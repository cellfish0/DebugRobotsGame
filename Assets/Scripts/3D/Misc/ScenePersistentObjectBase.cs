using UnityEngine;

namespace Assets.Scripts._3D.Misc
{
    public abstract class ScenePersistentObjectBase : MonoBehaviour
    {
        protected abstract void Init();

        protected abstract void OnDispose();

        private void OnDestroy()
        {
            OnDispose();
        }
    }
}