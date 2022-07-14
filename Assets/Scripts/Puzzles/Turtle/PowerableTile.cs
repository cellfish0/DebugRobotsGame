using UnityEngine;

namespace Assets.Scripts.Puzzles.Turtle
{
    [CreateAssetMenu(menuName = "Turtle/Tiles/Powerable")]
    public class PowerableTile : TurtleTileBase
    {
        public virtual void PowerUp()
        {
            //Powered = true;
        }

        //public bool Powered;
    }
}