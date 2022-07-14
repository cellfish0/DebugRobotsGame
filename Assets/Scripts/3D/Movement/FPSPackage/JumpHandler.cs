using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    [RequireComponent(typeof(CharacterController))]
    public class JumpHandler : MonoBehaviour
    {
        public event Action BeginJump;
        public event Action<Vector3> Jumping;
        public event Action FinishedJump;

        [SerializeField] private AnimationCurve RawJumpCurve;
        [SerializeField] private float JumpHeight;
        [SerializeField] private float jumpDuration;
        private IPlayerInput input;
        private CharacterController characterController;
        private AnimationCurve normalizedJumpCurve;
        private IEnumerator jumpCoroutine;

        public void InterruptJump()
        {
            if(jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
                jumpCoroutine = null;
                FinishedJump?.Invoke();
            }
        }
        public void Init(IPlayerInput PlayerInput)
        {
            characterController = GetComponent<CharacterController>();
            input = PlayerInput;
            normalizedJumpCurve = NormalizeCurve(RawJumpCurve);
            input.Jump += Jump;
        }

        public void Dispose()
        {
            input.Jump -= Jump;
        }
        public void Jump()
        {
            if (characterController.isGrounded)
            {
                BeginJump?.Invoke();
                jumpCoroutine = JumpCoroutine(normalizedJumpCurve);
                StartCoroutine(jumpCoroutine);
            }
        }

        private IEnumerator JumpCoroutine(AnimationCurve curve)
        {
            float t = Time.fixedDeltaTime / jumpDuration;
            while (t < 1)
            {
                float height = (curve.Evaluate(t) - curve.Evaluate(t - (Time.fixedDeltaTime / jumpDuration))) * JumpHeight;
                Vector3 velocity = new Vector3(0, height, 0);
                Jumping?.Invoke(velocity);

                t += Time.fixedDeltaTime / jumpDuration;
                yield return new WaitForFixedUpdate();
            }

            FinishedJump?.Invoke();
        }

        private AnimationCurve NormalizeCurve(AnimationCurve curve)
        {
            curve = SetStartToZero(curve);
            Keyframe[] keys = curve.keys;

            float horizontalRatio = 1 / curve.keys[curve.length - 1].time;
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].time *= horizontalRatio;

            }

            float verticalRatio = GetVerticalRatio(curve);
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].value *= verticalRatio;
                keys[i].inTangent /= horizontalRatio;
                keys[i].outTangent /= horizontalRatio;
            }

            AnimationCurve normalized = new AnimationCurve(keys);
            return normalized;
        }

        private float GetVerticalRatio(AnimationCurve curve)
        {
            float maxValue = GetMaxValue(curve);
            return 1 / maxValue;
        }

        private float GetMaxValue(AnimationCurve curve)
        {
            Keyframe[] keys = curve.keys;

            float duration = keys[keys.Length - 1].time;
            float maxValue = Mathf.NegativeInfinity;

            float sliceFrequency = 0.01f;
            sliceFrequency *= duration;

            for (float i = 0; i < duration; i += sliceFrequency)
            {
                float value = curve.Evaluate(i);
                if (value > maxValue) maxValue = value;
            }
            return maxValue;
        }

        private AnimationCurve SetStartToZero(AnimationCurve curve)
        {
            Keyframe[] keys = curve.keys;

            if (keys[0].value != 0)
            {
                float verticalOffset = -keys[0].value;

                for (int i = 0; i < keys.Length; i++)
                {
                    keys[i].value += verticalOffset;
                }
            }

            if (keys[0].time != 0)
            {
                float horizontalOffset = -keys[0].time;

                for (int i = 0; i < keys.Length; i++)
                {
                    keys[i].time += horizontalOffset;
                }
            }

            return new AnimationCurve(keys);
        }
    }
}
