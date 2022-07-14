using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.Nodes;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class StartVisualNode : VisualNodeBase
    {
        [SerializeField] private OutSocket Out;
        [SerializeField] private TMP_Dropdown dropdown;
        private List<TestSubject> subjects;
        private int index = 0;
        public TestSubject Subject { get => GetSub(); }

        public override List<InSocket> Ins => null;

        public override List<OutSocket> Outs => new List<OutSocket>() {Out };

        private TestSubject GetSub()
        {
            return subjects[index];
        }

        public override void Init()
        {
            base.Init();

            Out.Init(this);
            ResetSubjects();
            InitDropdown();
        
        }

        public void ResetSubjects()
        {
            StartNode startNode = Node as StartNode;
            subjects = startNode.testSubjects;
        }

        private void InitDropdown()
        {
            dropdown.ClearOptions();
            List<string> subjectNames = new List<string>();
            foreach (var sub in subjects)
            {
                subjectNames.Add(sub.Name);
            }

            dropdown.AddOptions(subjectNames);
        }

        public void ChooseSubject(int index)
        {
            this.index = index;
        }

        public override HashSet<VisualNodeBase> GetNext()
        {
            HashSet<InSocket> linked = Out.LinkedTo;
            HashSet<VisualNodeBase> next = new HashSet<VisualNodeBase>();
            foreach (var socket in linked)
            {
                next.Add(socket.Parent);
            }
            return next;
        }

        public override RoboSimpleVisitor Execute(TestSubject subject)
        {
            return null;
        }

        public override string GetText()
        {
            return null;
        }
    }
}


