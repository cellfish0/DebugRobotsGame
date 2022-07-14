using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.VisualNodes;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Util
{
    [CreateAssetMenu]
    public class LinkSaverSO : ScriptableObject
    {
        [HideInInspector]
        public List<Link> links;
    }
}
