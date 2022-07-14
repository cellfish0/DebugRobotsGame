using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
    public class RayCastBasedSelector : MonoBehaviour, ISelector
    {
        [SerializeField] private float length;
        private IRoomSelectable selection;
        public void Check(Ray ray)
        {
            selection = null;
            if (!Physics.Raycast(ray, out var hit, length)) return;

            if (hit.transform.TryGetComponent(out IRoomSelectable potentialSelection))
            {
                selection = potentialSelection;
                selection.HitPosition = hit.point;
            }

        }

        public IRoomSelectable GetSelection()
        {
            return selection;
        }
    }
}