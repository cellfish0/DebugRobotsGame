using Assets.Scripts.Puzzles.Robo.Util;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Nodes
{
    public abstract class NodeBase : ScriptableObject, INode
    {
        public NodeType type;
        public abstract string FuncName { get; }
        public abstract string FuncReturns { get; }
        public abstract string Text { get; }

        public abstract ScriptAsString RoboScript { get; set; }

        public virtual string FormatFuncName()
        {
            return FuncName;
        }

        public virtual string FormatReturn()
        {
            return FuncReturns;
        }
    }
}

