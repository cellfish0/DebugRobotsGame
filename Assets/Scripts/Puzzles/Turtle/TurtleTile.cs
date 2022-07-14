using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Turtle
{
    [CreateAssetMenu(menuName = "Turtle/Tiles/Tile")]
    public class TurtleTile : TurtleTileBase
    {
        [SerializeField] private PowerableTile paintedVariant;
        [SerializeField] private bool paintable = true;

        public bool Paintable
        {
            get => paintable;
            set => paintable = value;
        }

        public PowerableTile Painted
        {
            get => paintedVariant;
            set => paintedVariant = value;
        }
    }
}

