using System;
using Antlr4.Runtime;
using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.Util;
using Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow;
using ConsoleApp2.Test;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public abstract class EditableVisualNode : VisualNodeBase, IEditableNode
    {
        private EditorWindowMenu editorWindow;
        private CodeEditor codeEditor;
        [SerializeField] protected ScriptAsString RoboScript;
        [SerializeField] private bool locked;
        [SerializeField] private TMP_Text FuncLabel;
        [SerializeField] private Image lockedImage;
        public object Result { get; private set; }

        public override void Init()
        {
            base.Init();
            Node.RoboScript = RoboScript;
            FuncLabel.text = Node.FuncName;
            lockedImage.gameObject.SetActive(locked);
            
            CreateEditorWindow();
        }

        public override RoboSimpleVisitor Execute(TestSubject subject)
        {
            var fileContents = GetText();

            AntlrInputStream inputStream = new AntlrInputStream(fileContents);
            RoboLexer speakLexer = new RoboLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            RoboParser roboParser = new RoboParser(commonTokenStream);


            roboParser.RemoveErrorListeners();
            roboParser.AddErrorListener(new RoboErrorListener(Node.FuncName));


            var roboContext = roboParser.program();
            RoboSimpleVisitor visitor = new RoboSimpleVisitor(Node.FuncName);

            try
            {
                Result = visitor.Visit(roboContext);
                return visitor;
            }
            catch (Exception)
            {
                throw;
                return null;
            }
        }

        public override string GetText()
        {
            return codeEditor.GetText();
        }

        public void ResetText()
        {
            codeEditor.SetText(Node.Text);
        }

        private void CreateEditorWindow()
        {
            editorWindow = Instantiate(EditorWindowPrefab, GetComponentInParent<VisualProgram>().WindowRoot);
            editorWindow.Hide();

            Draggable draggable = editorWindow.GetComponent<Draggable>();

            draggable.Init();
            draggable.Focused.AddListener(DraggableManager.SetCurrentFocused);

            codeEditor = editorWindow.GetComponent<CodeEditor>();
            codeEditor.Init(Node.Text, Node.FormatFuncName(), Node.FormatReturn(), locked);

            editorWindow.ResetText.onClick.AddListener(ResetText);

            UndoRecorder undoRecorder = editorWindow.GetComponent<UndoRecorder>();
            undoRecorder.Init(Node.Text);
        }



        public void ShowWindow()
        {
            //if(locked) return;
            

            if (editorWindow == null)
            {
                CreateEditorWindow();
            }

            editorWindow.Show();
            
        }
    }
}



