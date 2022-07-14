using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class IfVisualNode : EditableVisualNode
    {
        [SerializeField] private InSocket In;
        [SerializeField] private OutSocket OutTrue;
        [SerializeField] private OutSocket OutFalse;

        private bool IsTrue;


        public override List<InSocket> Ins => new List<InSocket>() {In };
        public override List<OutSocket> Outs => new List<OutSocket>() { OutTrue, OutFalse };

        public override RoboSimpleVisitor Execute(TestSubject subject)
        {
        
            var visitor = base.Execute(subject);
            IsTrue = visitor.BooleanHandler.IsTrue(visitor.GetIdValue("IsTrue"));

            return visitor;
        }

        public override HashSet<VisualNodeBase> GetNext()
        {
            if (IsTrue)
            {
                return GetBySocket(OutTrue);
            }

            else
            {
                return GetBySocket(OutFalse);
            }
        }

        private HashSet<VisualNodeBase> GetBySocket(OutSocket Out)
        {
            HashSet<InSocket> linked = Out.LinkedTo;
            HashSet<VisualNodeBase> next = new HashSet<VisualNodeBase>();
            foreach (var socket in linked)
            {
                next.Add(socket.Parent);
            }
            return next;
        }

        public override void Init()
        {
            base.Init();
            In.Init(this);
            OutTrue.Init(this);
            OutFalse.Init(this);    
        }
    }
}



