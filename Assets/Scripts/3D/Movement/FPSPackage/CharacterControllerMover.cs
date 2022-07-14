using UnityEngine;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMover : MonoBehaviour, IMoveable
    {
        [Header("Ground")]
        [SerializeField] private float groundAcceleration = 3f;
        [SerializeField] private float groundMaxVelocity = 10f;
        [SerializeField] private float friction = 1f;
        [Header("Air")]
        [SerializeField] private float airAcceleration = 3f;
        [SerializeField] private float airMaxVelocity = 10f;
        [Space]
        [SerializeField] private CharacterController characterController;

        private IInput input;
        private JumpHandler jumpHandler;
    
        private GravityHandler gravityHandler;

        private Vector3 horizontalVelocity;
        private Vector3 verticalVelocity;
    

        private bool isJumping;

        public void Move(Vector3 movement)
        {
            characterController.Move(movement);
        }

        public void Init(IInput Input, GravityHandler GravityHandler, JumpHandler JumpHandler)
        {
            input = Input;
            gravityHandler = GravityHandler;
            jumpHandler = JumpHandler;

            input.MovementEvent += OnHorizontalMovement;
            jumpHandler.Jumping += OnJump;
            jumpHandler.FinishedJump += FinishedJump;
        }

        public void Dispose()
        {
            input.MovementEvent -= OnHorizontalMovement;
            jumpHandler.Jumping -= OnJump;
            jumpHandler.FinishedJump -= FinishedJump;
        }

        private void FinishedJump()
        {
            isJumping = false;
        }


        private void OnHorizontalMovement(Vector3 movement)
        {
            movement = transform.TransformDirection(movement);


            if (characterController.isGrounded)
            {
                horizontalVelocity = ApplyFriction(horizontalVelocity);
                horizontalVelocity = AccelerateHorizontal(movement, horizontalVelocity, groundAcceleration, groundMaxVelocity);
            }
            else
            {
                horizontalVelocity = AccelerateHorizontal(movement, horizontalVelocity, airAcceleration, airMaxVelocity);
            }
        }

        private Vector3 ApplyFriction(Vector3 previousVelocity)
        {
            float speed = previousVelocity.magnitude;
            if (speed != 0)
            {
                float drop = speed * friction * Time.deltaTime;
                previousVelocity *= Mathf.Max(speed - drop, 0) / speed;
            }
            return previousVelocity;
        }

        private Vector3 AccelerateHorizontal(Vector3 desiredVelocity, Vector3 previousVelocity, float acceleration, float maxVelocity)
        {
            float projectedVelocity = Vector3.Dot(previousVelocity, desiredVelocity);
            float acceleratedVelocity = acceleration * Time.deltaTime;

            if (projectedVelocity + acceleratedVelocity > maxVelocity * Time.deltaTime)
            {
                acceleratedVelocity = maxVelocity * Time.deltaTime - projectedVelocity;
            }
            //Debug.Log(projectedVelocity + acceleratedVelocity);

            return previousVelocity + desiredVelocity.normalized * acceleratedVelocity;
        }

        private void OnJump(Vector3 velocity)
        {
            isJumping = true;
            verticalVelocity = velocity;
        }

        private void Update()
        {
            if (!isJumping)
            {
                verticalVelocity = gravityHandler.GetVerticalVelocity();
            }

            Vector3 movement = verticalVelocity;
            movement += horizontalVelocity;

            Move(movement);
        }
    }
}
