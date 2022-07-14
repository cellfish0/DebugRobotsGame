using TMPro;
using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
    public class DisplayHintResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private TMP_Text hintText;

        private void Awake()
        {
            hintText.gameObject.SetActive(false);
        }

        public void OnDeselect(IRoomSelectable selection)
        {
            StopDisplaying();
        }

        public void OnSelect(IRoomSelectable selection)
        {
            string hint = selection.GetHint();
            DisplayHint(hint);
        }

        private void DisplayHint(string hint)
        {
            hintText.gameObject.SetActive(true);
            hintText.text = hint;
        }

        private void StopDisplaying()
        {
            hintText.gameObject.SetActive(false);
        }
    }
}