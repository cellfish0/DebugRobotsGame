using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow
{
    public class EditorWindowMenu : RoboBaseBehaviour, IMenu
    {
        [SerializeField] private Button resetText;
        [SerializeField] private Draggable draggable;
        public Button ResetText { get => resetText; }
        public Draggable Draggable { get => draggable; }

        public void Show()
        {
            DraggableManager.SetCurrentFocused(Draggable);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            DraggableManager.TryRemoveFocused(Draggable);
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            if (gameObject.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}
