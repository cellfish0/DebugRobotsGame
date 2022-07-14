using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Puzzles.Turtle
{
    public class PowerManager : MonoBehaviour
    {
        [SerializeField] private Tilemap map;
        private List<PowerSourceTile> powerSources;
        private Dictionary<Vector3Int, bool> powered = new Dictionary<Vector3Int, bool>();


        public void PowerUp()
        {
            foreach (var position in map.cellBounds.allPositionsWithin)
            {
                powered.Add(position, false);
            }

            foreach (var position in map.cellBounds.allPositionsWithin)
            {

                var powerSource = map.GetTile(position) as PowerSourceTile;
                if (powerSource != null)
                {
                    TryPowerAt(position);
                }
                

            }
        }

        private void TryPowerAdjacent(Vector3Int position)
        {
            //Debug.Log($"Adjacent to {position}: ");
            TryPowerAt(position + Vector3Int.up);
            TryPowerAt(position + Vector3Int.down);
            TryPowerAt(position + Vector3Int.right);
            TryPowerAt(position + Vector3Int.left);
        }

        private void TryPowerAt(Vector3Int position)
        {
            var temp = map.GetTile(position) as PowerableTile;
            
            if (temp != null)
            {
                //Debug.Log($"looking at {temp.name}");
                if (!powered.ContainsKey(position))
                {
                    //Debug.Log($"{position}");
                    return;
                }
                
                if (!powered[position])
                {
                    //Debug.Log($"power up {temp.name}");
                    temp.PowerUp();
                    powered[position] = true;
                    TryPowerAdjacent(position);
                }
                else
                {
                    //Debug.Log($"{temp.name} is powered");
                }
               
            }
        }
    }
}
