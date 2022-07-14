using UnityEngine;

namespace Assets.Scripts.Puzzles.Turtle
{
    [CreateAssetMenu(menuName = "Turtle/Tiles/Hash")]
    public class Hash : PowerableTile
    {
        public override void PowerUp()
        {
            base.PowerUp();
            Debug.Log("Hash powered!");
        }
    }
}