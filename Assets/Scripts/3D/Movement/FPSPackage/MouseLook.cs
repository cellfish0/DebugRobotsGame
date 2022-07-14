using UnityEngine;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool smooth = true;
        [Space]
        [SerializeField] private float sensitivity = 1f;
        [SerializeField] private float smoothing = 3f;
        [Space]
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [Space]
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [Space]
        [SerializeField] private float maxLagTime = 0.1f;
        float lagTimer = 0;
        private Vector2 lastRealInput;

        private float absoluteRotationX = 0;
        private float absoluteRotationY = 0;

        float smoothRotationX = 0;
        float smoothRotationY = 0;

        private Quaternion originalRotation;
        private Quaternion originalParentRotation;
        private IPlayerInput playerInput;
    
        public void Init(IPlayerInput PlayerInput)
        {
            originalParentRotation = transform.localRotation;
            playerInput = PlayerInput;
            originalRotation = target.localRotation;
            //Cursor.lockState = CursorLockMode.Locked;

            playerInput.MouseRotationEvent += Rotate;
        }

        public void Dispose()
        {
            playerInput.MouseRotationEvent -= Rotate;
        }

        public void Rotate(Vector2 rawDelta)
        {

            //rawDelta = HandleLag(rawDelta);

            absoluteRotationX += rawDelta.x * sensitivity * Time.fixedDeltaTime;
            absoluteRotationY += rawDelta.y * sensitivity * Time.fixedDeltaTime;

            absoluteRotationX = ClampAngle(absoluteRotationX, minX, maxX);
            absoluteRotationY = ClampAngle(absoluteRotationY, minY, maxY);


            Quaternion xQuaternion;
            Quaternion yQuaternion;

            smoothRotationX = Mathf.LerpAngle(smoothRotationX, absoluteRotationX, smoothing);
            smoothRotationY = Mathf.LerpAngle(smoothRotationY, absoluteRotationY, smoothing);

            if (smooth)
            {
                xQuaternion = Quaternion.AngleAxis(smoothRotationX, Vector3.up);
                yQuaternion = Quaternion.AngleAxis(smoothRotationY, -Vector3.right);
            }
            else
            {
                xQuaternion = Quaternion.AngleAxis(absoluteRotationX, Vector3.up);
                yQuaternion = Quaternion.AngleAxis(absoluteRotationY, -Vector3.right);
            }

            target.localRotation = originalRotation * yQuaternion;
            transform.localRotation = originalParentRotation * xQuaternion;
        }

    

        private Vector2 HandleLag(Vector3 delta)
        {
            lagTimer += Time.fixedDeltaTime;

            if (Mathf.Approximately(0, delta.x) && Mathf.Approximately(0, delta.y) && lagTimer < maxLagTime)
            {
                return lastRealInput;
            }
            else
            {
                lagTimer = 0;
                lastRealInput = delta;
                return lastRealInput;
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            //Debug.Log(angle);
            if (angle < -360)
                angle += 360F;
            if (angle > 360)
                angle -= 360F;

            return Mathf.Clamp(angle, min, max);
        }
    }
}
