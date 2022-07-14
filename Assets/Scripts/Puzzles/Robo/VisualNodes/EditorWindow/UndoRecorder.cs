using System.Collections;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Util;
using ConsoleApp2.Test;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow
{
    public class UndoRecorder : RoboBaseBehaviour
    {
        private const int UndoCapacity = 10;
        [SerializeField] private TMP_InputField inputField;
        private Stack<UndoableAction> UndoableActions = new Stack<UndoableAction>(UndoCapacity);
        private CommonTokenStream prevStream;
        private string prevText;
        private int prevCursorPos;
        private Draggable draggable;

        public void Init(string text)
        {
            Controls.Robo.Undo.performed += Undo_performed;
            draggable = GetComponent<Draggable>();

            prevText = text;
            prevCursorPos = inputField.stringPosition;

            inputField.onValueChanged.AddListener(TryAddAction);
        }

        void OnDestroy()
        {
            Controls.Robo.Undo.performed -= Undo_performed;
        }

        private void Undo_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            //Debug.Log("undo");
            if (UndoableActions.Count == 0)
            {
                return;
            }

            if (DraggableManager.IsFocused(draggable))
            {
                Undo();
            }
        }

        public void TryAddAction(string text)
        {
            var fileContents = text;

            var commonTokenStream = GetStream(fileContents);

            if (!AreStreamsSame(prevStream, commonTokenStream))
            {
                var undoableAction = new UndoableAction(prevText, prevCursorPos, text, inputField.stringPosition);
                UndoableActions.Push(undoableAction);
                
            }

            prevText = text;
            prevCursorPos = inputField.stringPosition;
            prevStream = commonTokenStream;
        }

        private CommonTokenStream GetStream(string fileContents)
        {
            AntlrInputStream inputStream = new AntlrInputStream(fileContents);

            RoboLexer speakLexer = new RoboLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            return commonTokenStream;
        }

        public void Undo()
        {
            var last = UndoableActions.Pop();

            if (UndoableActions.Count >= 1)
            {
                UndoableAction peek = UndoableActions.Peek();
                prevText = peek.before;
                prevCursorPos = peek.cursorPosBefore;
            }
            else
            {
                prevText = last.before;
                prevCursorPos = last.cursorPosBefore;
            }

            inputField.SetTextWithoutNotify(last.before);
            inputField.stringPosition = last.cursorPosBefore;
            
        }

        private bool AreStreamsSame(CommonTokenStream stream1, CommonTokenStream stream2)
        {
            if (stream1 == null && stream2 != null)
            {
                return false;
            }
            if (stream2 == null && stream1 != null)
            {
                return false;
            }
            if (stream2 == null && stream1 == null)
            {
                return true;
            }



            WalkTree(stream1);
            WalkTree(stream2);

            IList<IToken> token1S = stream1.GetTokens();
            IList<IToken> token2S = stream2.GetTokens();

            if (token2S.Count != token1S.Count)
            {
                //Debug.Log("count");
                return false;
            }


            for (int i = 0; i < token1S.Count; i++)
            {
                var token1 = token1S[i];
                foreach (var token2 in token2S)
                {
                    if (token1.TokenIndex == -1 || token2.TokenIndex == -1)
                    {
                        continue;
                    }


                    if (token1.TokenIndex == token2.TokenIndex && token1.Type != token2.Type)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void WalkTree(CommonTokenStream stream1)
        {
            var walker = new ParseTreeWalker();
            RoboParser roboParser = new RoboParser(stream1);
            walker.Walk(new RoboBaseListener(), roboParser.program());
        }
    }

    public class UndoableAction
    {
        public int cursorPosBefore;
        public int cursorPosAfter;
        public string before;
        public string after;

        public UndoableAction(string before, int cursorPosBefore, string after, int cursorPosAfter)
        {
            this.cursorPosBefore = cursorPosBefore;
            this.cursorPosAfter = cursorPosAfter;

            this.before = before;
            this.after = after;
        }
        
    }
}
