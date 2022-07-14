using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Turtle;
using UnityEngine;

namespace Assets.Scripts._3D.Misc.Room
{
    public class RoomComponentHolder : ComponentHolderBase
    {
        public FPSControls FpsControls;
        public QualityChanger QualityChanger;
        public Camera PlayerCamera;

        public override void Init()
        {
            Initialize();
            FpsControls = new FPSControls();
            FpsControls.Enable();
            QualityChanger.Init();

            
        }
    }
}
