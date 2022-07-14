using System;
using System.IO;
using Antlr4.Runtime;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Util;
using ConsoleApp2.Test;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Nodes
{
    public abstract class BasicNodeBase : NodeBase
    {

        [SerializeField] private string args;
        [SerializeField] private string returnIdentifier;

        public override ScriptAsString RoboScript { get; set; }
        public override string Text { get => GetText(); }

        private string GetText()
        {

            return RoboScript.text;
        }

        public override string FuncName { get => RoboScript.Name; }
        public override string FuncReturns { get => returnIdentifier; }

        public override string FormatReturn()
        {
            return "return " + FuncReturns + "\n}";
        }

        public override string FormatFuncName()
        {
            return "<color=blue>func</color> " + FuncName + "(" + args + ")" + "\n{";
        }
    }



    public enum NodeType
    {
        Basic,
        If,
        Start
    }
}