using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
    public class FpsRayCastProvider :  MonoBehaviour, IRayProvider
    {

        [SerializeField] private Camera sourceCamera;
        
        public Ray CreateRay()
        {
            return sourceCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        }
    }
}