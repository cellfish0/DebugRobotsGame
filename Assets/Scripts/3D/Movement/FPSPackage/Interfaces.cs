using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts._3D.Movement.FPSPackage
{
    public interface IWeapon
    {
        public event Action PrimaryAttack;
        public event Action SecondaryAttack;

        public void OnPrimaryAttack();
        public void OnSecondaryAttack();
    }
    [Serializable]
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void OnPrimaryAttack();
        public abstract void OnSecondaryAttack();
    
    }
    public interface IKillable
    {
        public event Action<IKillable> Killed;
        public void OnKill();
    }

    public interface IDamageable
    {
        public event Action<float> Damaged;
        public float Health { get; }
        public void Damage(float value);
    }

    public interface IInput
    {
        public event Action<Vector3> MovementEvent;
        public event Action PrimaryAttack;
        public event Action SecondaryAttack;
        public event Action<int> Switch;
    }

    public interface IPlayerInput : IInput
    {
        public event Action<Vector2> MouseRotationEvent;
        public event Action Jump;
        public void OnPlanarMovement(InputAction.CallbackContext ctx);
        public void OnMouseRotationX(InputAction.CallbackContext ctx);
        public void OnMouseRotationY(InputAction.CallbackContext ctx);
        public void OnJump(InputAction.CallbackContext ctx);

    }


    public interface IMoveable
    {
        public void Move(Vector3 movement);
    }
}