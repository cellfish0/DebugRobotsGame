using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Puzzles.Robo.Console
{
    public class RoboConsole : MonoBehaviour
    {
        [SerializeField] private RoboErrorLine ErrorLinePrefab;
        [SerializeField] private RoboMessageLine MessageLinePrefab;
        [SerializeField] private Transform root;
        [SerializeField] private ScrollRect scrollRect;
        private List<ConsoleLine> lines = new List<ConsoleLine>();

        public void DisplayErrors(List<RoboError> errors)
        {
            foreach (RoboError error in errors)
            {
                DisplayError(error);
            }

            ScrollToBottom();
        }

        public void DisplayError(RoboError error)
        {
            var line = Instantiate(ErrorLinePrefab, root);
            line.Init(error.FuncName, error.Line, error.RoboMessage);
            LayoutRebuilder.ForceRebuildLayoutImmediate(line.transform as RectTransform);

            AddLine(line);
        }


        private void ScrollToBottom()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.transform as RectTransform);
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, -1);
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.transform as RectTransform);
        }

        public void DrawHorizontalLine(string color = null)
        {
            string msg = "======================";
            if (color != null)
            {
                msg = $"<color={color}>" + msg;
            }
            WriteLine(msg);
        }

        public void WriteLine(object message)
        {
            if(message == null) message = "<color=blue>null</color>";
            
            var line = Instantiate(MessageLinePrefab, root);
            line.Init(message.ToString());
            LayoutRebuilder.ForceRebuildLayoutImmediate(line.transform as RectTransform);

            AddLine(line);

            ScrollToBottom();
        }

        private void AddLine(ConsoleLine line)
        {
            lines.Add(line);
            if (lines.Count > 50)
            {
                
                Destroy(lines[0].gameObject);
                lines.RemoveAt(0);
            }
            
        }
    }
}
