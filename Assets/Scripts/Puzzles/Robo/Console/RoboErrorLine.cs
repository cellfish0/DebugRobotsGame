using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Console
{
    public class RoboErrorLine : ConsoleLine
    {
        private TMP_Text text;
        private int line;
        private const string funcNameColor = "#8CB8E7";

        public void Init(string funcName, int line, string message)
        {
            text = GetComponent<TMP_Text>();
            text.text = $"Error in node <color={funcNameColor}>{funcName}</color> at {line.ToString()}: <color=yellow>{message}</color>";
            text.text += "\n======================";
        }
    }

    public abstract class ConsoleLine : MonoBehaviour { }

}
