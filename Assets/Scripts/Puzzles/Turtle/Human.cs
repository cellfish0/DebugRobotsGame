using UnityEngine;

namespace Assets.Scripts.Puzzles.Turtle
{
    [CreateAssetMenu(menuName = "Turtle/Tiles/Human")]
    public class Human : PowerableTile
    {
        public override void PowerUp()
        {
            base.PowerUp();
            Debug.Log("You lost!");
        }
    }
}