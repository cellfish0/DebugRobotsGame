using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow
{
    public class LineCount : MonoBehaviour
    {
        private TMP_Text ugui;
        [SerializeField] private TMP_InputField inputField;
        private int prevLines = 0;

        private void Awake()
        {
            ugui = GetComponent<TMP_Text>();
        }
        public void UpdateLines(string arg)
        {
            int numLines = inputField.text.Split('\n').Length;

            if (prevLines != numLines)
                UpdateAllLines(numLines);
            prevLines = numLines;
        }

        private void UpdateAllLines(int numLines)
        {
            ugui.text = "";
            for (int i = 1; i <= numLines; i++)
            {
                ugui.text +=  i.ToString() + "\n";
            }
        }
    }
}