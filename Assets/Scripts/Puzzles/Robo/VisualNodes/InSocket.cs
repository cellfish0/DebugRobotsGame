using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class InSocket : Socket
    {
        [HideInInspector]
        public UnityEvent<InSocket> OverThis;
        [HideInInspector]
        public UnityEvent<InSocket> PointerExited;

        public override void Init(VisualNodeBase parent)
        {
            base.Init(parent);
            OverThis.AddListener(SocketConnector.SetOverSocket);
            PointerExited.AddListener(SocketConnector.TryRemoveOverSocket);
        }

        public void OnCursorEnter()
        {
            OverThis.Invoke(this);
        
        }

        public void OnCursorExit()
        {
            PointerExited.Invoke(this);
        }
    }
}
