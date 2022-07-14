using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Console
{
    public class RoboMessageLine : ConsoleLine
    {
        private TMP_Text text;

        public void Init(string message)
        {
            text = GetComponent<TMP_Text>();
            text.text = message;
        }
    }
}
