using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Nodes;
using Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public abstract class VisualNodeBase : RoboBaseBehaviour
    {
        protected EditorWindowMenu EditorWindowPrefab;
        [SerializeField] private NodeBase Template;

        public NodeType Type => Template.type;
        private NodeBase NodeTemplate { get => Template; }
        public NodeBase Node { get; protected set; }

        public abstract List<InSocket> Ins { get; }
        public abstract List<OutSocket> Outs { get; }



        public virtual void Init()
        {
            RoboComponentHolder componentHolder = RoboScenePersistentObject.Instance.componentHolder as RoboComponentHolder;
            EditorWindowPrefab = componentHolder.EditorWindowPrefab;
            Node = Instantiate(NodeTemplate);
        }

        public abstract string GetText();
        public abstract RoboSimpleVisitor Execute(TestSubject subject);
        public abstract HashSet<VisualNodeBase> GetNext();
    }
}




