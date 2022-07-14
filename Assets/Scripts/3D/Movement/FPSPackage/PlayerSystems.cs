using UnityEngine;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    public class PlayerSystems : MonoBehaviour
    {
        [SerializeField] private CharacterController CharacterController;
        [SerializeField] private PlayerInputHandler PlayerInputHandler;
    
        //[SerializeField] private GenericHealth PlayerHealth;
        [SerializeField] private GravityHandler GravityHandler;
        [SerializeField] private CharacterControllerMover CharacterControllerMover;
        [SerializeField] private JumpHandler JumpHandler;
        [SerializeField] private JumpInterrupionHandler JumpInterrupionHandler;
        [SerializeField] private MouseLook MouseLook;
        //[SerializeField] private WeaponSwitcher WeaponSwitcher;
        //[SerializeField] private LegsAnimator LegsAnimator;

        private void OnEnable()
        {
            PlayerInputHandler.Init(CharacterController);
        
            CharacterControllerMover.Init(PlayerInputHandler, GravityHandler, JumpHandler);
            GravityHandler.Init(CharacterController);
            JumpHandler.Init(PlayerInputHandler);
            JumpInterrupionHandler.Init(CharacterController, JumpHandler);
            MouseLook.Init(PlayerInputHandler);
            //PlayerHealth.Init();
            //WeaponSwitcher.Init(PlayerInputHandler);
            //LegsAnimator.Init(PlayerInputHandler);
        }

        private void OnDisable()
        {
            CharacterControllerMover.Dispose();
            JumpHandler.Dispose();
            JumpInterrupionHandler.Dispose();
            MouseLook.Dispose();
            //WeaponSwitcher.Dispose();
            //LegsAnimator.Dispose();
        }
    }
}
