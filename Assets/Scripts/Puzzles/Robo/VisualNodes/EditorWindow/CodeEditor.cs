using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Util;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow
{
    public class CodeEditor : RoboBaseBehaviour
    {
        [SerializeField] private TMP_Text funcName;
        [SerializeField] private TMP_Text funcReturns;

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private AutoFormatter autoFormatter;
        private Draggable draggable;

        public void Init(string code, string funcName, string funcReturns, bool locked)
        {
            inputField.interactable = !locked;

            draggable = GetComponent<Draggable>();

            this.funcName.text = funcName;
            this.funcReturns.text = funcReturns;

            Controls.Robo.Format.performed += Format;
            inputField.textComponent.enableWordWrapping = false;
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(FormatColor);

            SetText(code);
        }

        public void SetText(string code)
        {
            inputField.text = code;
        }

        public string GetText()
        {
            return inputField.text;
        }

        public void OnDestroy()
        {
            Controls.Robo.Format.performed -= Format;
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(FormatColor);
        }

        public void Format(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (DraggableManager.IsFocused(draggable))
                Format();
        }

        public void Format()
        {
            Debug.Log("Starting Formatting..");

            inputField.SetTextWithoutNotify(autoFormatter.FormatIndents(inputField.text));
        }

        private void FormatColor(object value)
        {
            if (value as TMP_Text == inputField.textComponent)
                autoFormatter.FormatColor(inputField.textComponent);
        }
    }
}
