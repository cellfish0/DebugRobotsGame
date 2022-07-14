
using UnityEditor.UIElements;

namespace Assets.Scripts.Puzzles.Turtle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public enum PenColor
    {
        Red,
        Blue
    }

    public class TurtleMover : MonoBehaviour
    {
        [SerializeField] private PowerManager powerManager;
        [SerializeField] private Tilemap mainMap;
        [SerializeField] private Tilemap turtleMap;
        private TurtleTile turtle;
        [SerializeField] private TurtleTile turtlePainting;
        [SerializeField] private TurtleTile turtleClean;
        private Vector3Int turtlePosition;
        private Vector3Int prevPosition;
        private bool moved;
        private PenColor penColor;
        private bool penDown;

        void Start()
        {
            turtle = turtleClean;

            MoveRight(2);
            PenDown();
            MoveUp(3);
            MoveLeft(2);
            turtleMap.SetTile(turtlePosition, null);
            powerManager.PowerUp();
        }

        public void MoveUp(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                TryMoveTo(turtlePosition + Vector3Int.up);
            }
        }

        public void MoveDown(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                TryMoveTo(turtlePosition + Vector3Int.down);
            }
        }

        public void MoveRight(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                TryMoveTo(turtlePosition + Vector3Int.right);
            }
        }

        public void MoveLeft(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                TryMoveTo(turtlePosition + Vector3Int.left);
            }
        }

        public void PenDown()
        {
            penDown = true;
            turtle = turtlePainting;


            TryPaint(turtlePosition);

        }

        public void PenUp()
        {
            penDown = false;
            turtle = turtleClean;
        }

        private bool TryMoveTo(Vector3Int toPosition)
        {

            if (toPosition.x >= mainMap.cellBounds.xMax ||
                toPosition.x < mainMap.cellBounds.xMin ||
                toPosition.y >= mainMap.cellBounds.yMax ||
                toPosition.y < mainMap.cellBounds.yMin)
            {
                if (!moved)
                    turtleMap.SetTile(turtlePosition, turtle);
                return false;
            }

            if (penDown)
            {
                TryPaint(toPosition);
            }

            prevPosition = turtlePosition;
            turtlePosition = toPosition;
            turtleMap.SetTile(prevPosition, null);
            turtleMap.SetTile(toPosition, turtle);

            moved = true;

            return true;
        }

        private void TryPaint(Vector3Int toPosition)
        {
            var turtleTile = mainMap.GetTile(toPosition) as TurtleTile;

            if (turtleTile != null && turtleTile.Paintable)
            {
                mainMap.SetTile(toPosition, turtleTile.Painted);
            }
        }
    }
}
