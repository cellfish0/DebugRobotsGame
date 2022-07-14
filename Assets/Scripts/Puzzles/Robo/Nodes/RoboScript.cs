using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Nodes
{
    [CreateAssetMenu]
    public class RoboScript : ScriptableObject
    {
        [SerializeField] private TextAsset script;

        public string GetText()
        {
            return script.text;
        }
    }
}
