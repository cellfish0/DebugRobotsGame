using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    public class PlayerInputHandler : MonoBehaviour, IPlayerInput
    {
        private CharacterController controller;
    
        public event Action<Vector2> MouseRotationEvent;
        public event Action<Vector3> MovementEvent;
        public event Action Jump;

        public event Action PrimaryAttack;
        public event Action SecondaryAttack;

        public event Action<int> Switch;

        private Vector3 currentMovement;

        private float MouseRotationX;
        private float MouseRotationY;


        public void Init(CharacterController Controller)
        {
            controller = Controller;
            Application.targetFrameRate = 60;
        }

        public void OnSwitch(InputAction.CallbackContext ctx)
        {
            if(ctx.performed)
            {
                int increment = (int)ctx.ReadValue<float>() / 120;
                Switch?.Invoke(increment);
                Debug.Log(increment);
            }
        
        }

        public void OnPrimaryAttack(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                PrimaryAttack?.Invoke();
            }
        }
        public void OnSecondaryAttack(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                SecondaryAttack?.Invoke();
            }
        }

        public void OnMouseRotationX(InputAction.CallbackContext ctx)
        {
            MouseRotationX = ctx.ReadValue<float>();
        }

        public void OnMouseRotationY(InputAction.CallbackContext ctx)
        {
            MouseRotationY = ctx.ReadValue<float>();
        }

        public void OnPlanarMovement(InputAction.CallbackContext ctx)
        {
            Vector2 rawMovement = ctx.ReadValue<Vector2>();
            BeginMovement(rawMovement);
        }

        private void BeginMovement(Vector2 rawMovement)
        {
            if (rawMovement == Vector2.zero)
            {
                StopMovement();
                return;
            }
            currentMovement = new Vector3(rawMovement.x, 0, rawMovement.y);
            currentMovement *= Time.fixedDeltaTime;
        }

        private void StopMovement()
        {
            currentMovement = Vector3.zero;
        }

        private void Update()
        {
            MovementEvent?.Invoke(currentMovement);
            MouseRotationEvent?.Invoke(new Vector2(MouseRotationX, MouseRotationY));
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if(ctx.performed)
            {
                Jump?.Invoke();
            }
        }
    }
}
