using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    

    public abstract class ComponentHolderBase : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent Initialized;
        public bool IsInitialized { get; protected set; }

        public abstract void Init();
        public void Initialize()
        {
            IsInitialized = true;
            Initialized.Invoke();
        }
    }
}
