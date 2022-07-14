using UnityEngine;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    [RequireComponent(typeof(JumpHandler))]
    public class JumpInterrupionHandler : MonoBehaviour
    {
        [SerializeField] private float minHeadHeight;
        [SerializeField] private float maxHeadHeight;
        private JumpHandler jumpHandler;
        private CharacterController characterController;
        [SerializeField] private bool isJumping;
    
        public void Init(CharacterController CharacterController, JumpHandler JumpHandler)
        {
            characterController = CharacterController;
            jumpHandler = JumpHandler;
        
            jumpHandler.BeginJump += OnJumpStart;
            jumpHandler.FinishedJump += OnJumpEnd;
        }

        public void Dispose()
        {
            jumpHandler.BeginJump -= OnJumpStart;
            jumpHandler.FinishedJump -= OnJumpEnd;
        }

        private void OnJumpStart()
        {
            isJumping = true;
        }

        private void OnJumpEnd()
        {
            isJumping = false;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!isJumping) return;
            float currentHeadHeightMax = characterController.transform.position.y + characterController.center.y + maxHeadHeight;
            float currentHeadHeightMin = characterController.transform.position.y + characterController.center.y + minHeadHeight;
            if (hit.point.y <= currentHeadHeightMax && hit.point.y >= currentHeadHeightMin)
            {
                InterruptJump();
            }
        }
        private void InterruptJump()
        {
            jumpHandler.InterruptJump();
            OnJumpEnd();
        }
    }
}
