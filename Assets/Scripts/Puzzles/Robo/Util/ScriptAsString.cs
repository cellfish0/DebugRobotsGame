using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Util
{
    [CreateAssetMenu]
    public class ScriptAsString : ScriptableObject
    {
        public string Name;
        [TextArea(10, 40)]
        public string text;
    }
}



