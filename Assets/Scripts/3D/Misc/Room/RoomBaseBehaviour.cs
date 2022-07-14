using UnityEngine;

namespace Assets.Scripts._3D.Misc.Room
{
    public class RoomBaseBehaviour : MonoBehaviour
    {
        protected RoomComponentHolder componentHolder;
        protected FPSControls FpsControls => componentHolder.FpsControls;
        protected Camera PlayerCamera => componentHolder.PlayerCamera;


        public virtual void BaseInit()
        {
            componentHolder = RoomScenePersistentObject.Instance.componentHolder;
        }
    }
}
