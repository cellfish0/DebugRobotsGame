using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class BasicVisualNode : EditableVisualNode
    {
       
        [SerializeField] private InSocket In;
        [SerializeField] private OutSocket Out;

        public override List<InSocket> Ins => new List<InSocket>() {In };

        public override List<OutSocket> Outs => new List<OutSocket>() {Out };

        public override void Init()
        {
            base.Init();
            In.Init(this);  
            Out.Init(this);
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


    }
}


