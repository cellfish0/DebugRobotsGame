using Assets.Scripts._3D.Misc.Room;
using UnityEngine;

namespace Assets.Scripts._3D.Misc
{
    public class QualityChanger : RoomBaseBehaviour
    {
        private int index;

        public void Init()
        {
            BaseInit();
            FpsControls.Misc.ChangeQuality.performed += ChangeQuality;
        }

        private void OnDestroy()
        {
            FpsControls.Misc.ChangeQuality.performed -= ChangeQuality;
        }

        private void ChangeQuality(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (index >= QualitySettings.names.Length)
            {
                index = 0;
            }
            QualitySettings.SetQualityLevel(index);
            index++;
        }
    }
}