using UnityEngine;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    [RequireComponent(typeof(CharacterController))]
    public class GravityHandler : MonoBehaviour
    {
    
        [SerializeField] private Vector3 gravity;
        [SerializeField] private float stickingForce;

        [SerializeField] private CharacterController characterController;
        private Vector3 verticalVelocity;
        private bool isSticking;
 
    
        public void Init(CharacterController CharacterController)
        {
            characterController = CharacterController;
        }

        public Vector3 GetVerticalVelocity()
        {
            if (!characterController.isGrounded)
            {
                if(isSticking)
                {
                    isSticking = false;
                    verticalVelocity = Vector3.zero;
                }
                verticalVelocity += gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            }
            else
            {
                isSticking = true;
                verticalVelocity = gravity.normalized * stickingForce;
            }
            return verticalVelocity;
        }
    }
}
