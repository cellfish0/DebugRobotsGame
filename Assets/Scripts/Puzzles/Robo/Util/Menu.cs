using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Util
{
    public class Menu : MonoBehaviour, IMenu
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
