using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
    public interface IRoomSelectable
    {
        string GetHint();
        Vector3 Position { get; }
        Vector3 HitPosition { get; set; }
    }

    public interface IInteractable : IRoomSelectable
    {
        void Interact();
    }
}
