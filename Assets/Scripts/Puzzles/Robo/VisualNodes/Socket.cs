using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public abstract class Socket : RoboBaseBehaviour
    {
        private RectTransform RectTransform;
        private Draggable draggable;
        public Vector3 Position { get => transform.position; }
        public VisualNodeBase Parent { get; private set; }
        public UnityEvent<Vector3> Moved;
        

        public virtual void Init(VisualNodeBase parent)
        {
            base.BaseInit();
            Parent = parent;
            RectTransform = GetComponent<RectTransform>();
            draggable = Parent.GetComponent<Draggable>();
            draggable.DraggingEvent.AddListener(OnMove);
        }

        private Vector3 GetPosition()
        {
            var v = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(v);
            return v[0];
        }

        private void OnMove(Draggable arg1)
        {
            Moved.Invoke(GetPosition());
        }
    }
}

