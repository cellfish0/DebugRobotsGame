using System;
using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.Util;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Nodes
{
    [CreateAssetMenu]
    public class StartNode : NodeBase
    {
    
        public List<TestSubject> testSubjects;

        public override string FuncName => null;

        public override string FuncReturns => null;

        public override string Text => null;
        public override ScriptAsString RoboScript { get => null; set => RoboScript = null; }
    }
}

