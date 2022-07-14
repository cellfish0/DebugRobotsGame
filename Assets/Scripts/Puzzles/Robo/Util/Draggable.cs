using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Puzzles.Robo.Util
{
    public class Draggable : MonoBehaviour
    {
        public UnityEvent<Draggable> Focused;
        public UnityEvent<Draggable> DraggingEvent;
        private Canvas canvas;
        private RectTransform canvasTransform;
        private Vector3 offset;
        private void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            canvas = GetComponentInParent<Canvas>();
            canvasTransform = canvas.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(BaseEventData data)
        {
            transform.SetAsLastSibling();
            PointerEventData pointerData = (PointerEventData)data;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasTransform,
                pointerData.position,
                canvas.worldCamera,
                out Vector2 position
            );
            offset = transform.position - canvas.transform.TransformPoint(position);

            Focused.Invoke(this);
        }

        public void DragHandler(BaseEventData data)
        {

            PointerEventData pointerData = (PointerEventData)data;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasTransform,
                pointerData.position,
                canvas.worldCamera,
                out Vector2 position
            );

            transform.position = offset + canvas.transform.TransformPoint(position);
            DraggingEvent.Invoke(this);
        }
    }
}
