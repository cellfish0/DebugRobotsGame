using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Util;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class DraggableManager : MonoBehaviour
    {
        private Draggable FocusedWindow;

        public void SetCurrentFocused(Draggable draggable)
        {
            FocusedWindow = draggable;
        }

        public void TryRemoveFocused(Draggable toRemove)
        {
            if (FocusedWindow == toRemove)
            {
                FocusedWindow = null;
            }
        }

        public bool IsFocused(Draggable draggable)
        {
            return draggable == FocusedWindow;
        }
    }
}
