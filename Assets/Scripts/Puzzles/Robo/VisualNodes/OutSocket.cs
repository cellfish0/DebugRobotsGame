using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class OutSocket : Socket
    {

        [HideInInspector]
        public UnityEvent<OutSocket> BeganDrag;
        [HideInInspector]
        public UnityEvent<OutSocket> EndedDrag;
        public HashSet<InSocket> LinkedTo = new HashSet<InSocket>();
        [SerializeField] private List<InSocket> LinkedToList = new List<InSocket>();

        public override void Init(VisualNodeBase parent)
        {
            base.Init(parent);
            LinkedTo = new HashSet<InSocket>(LinkedToList);

            BeganDrag.AddListener(SocketConnector.SetDraggedSocket);
            EndedDrag.AddListener(SocketConnector.TryConnect);
        }

        public void OnBeginDrag()
        {
            
            BeganDrag.Invoke(this);
        }

        public void OnEndDrag()
        {
            
            EndedDrag.Invoke(this);
        
        }

        public bool AddLink(InSocket to)
        {
            bool success = LinkedTo.Add(to);
            LinkedToList = new List<InSocket>(LinkedTo);
            return success;
        }
    }
}
