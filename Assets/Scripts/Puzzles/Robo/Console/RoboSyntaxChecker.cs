using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.VisualNodes;
using ConsoleApp2.Test;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Console
{
    public class RoboSyntaxChecker : RoboBaseBehaviour
    {
        private const string PredicateVariable = "IsTrue";

        public List<RoboError> CheckAll(List<VisualNodeBase> visualNodes)
        {
            CommandHandler.QuietMode = true;
            
            List<RoboError> temp = new List<RoboError>();
            foreach (VisualNodeBase node in visualNodes)
            {
                string needVars = null;
                if (node is IfVisualNode)
                {
                    needVars = PredicateVariable;
                }

                var r = Check(node.Node.FuncName, node.GetText(), needVars);
                if (r != null)
                {
                    temp.AddRange(r);
                }
            }

            CommandHandler.QuietMode =false;
            return temp;
        }


        public List<RoboError> Check(string FuncName, string text, string needVars)
        {
            

            if (text == null) return null;

            List<RoboError> temp = new List<RoboError>();

            AntlrInputStream inputStream = new AntlrInputStream(text);
            RoboLexer speakLexer = new RoboLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            RoboParser roboParser = new RoboParser(commonTokenStream);


            roboParser.RemoveErrorListeners();
            RoboErrorListener listener = new RoboErrorListener(FuncName);
            roboParser.AddErrorListener(listener);


            var roboContext = roboParser.program();
            RoboSimpleVisitor visitor = new RoboSimpleVisitor(FuncName);

            try
            {
                var result = visitor.Visit(roboContext);
            }
            catch (RoboError e)
            {
                temp.Add(e);
            }
            catch (Exception)
            {
                throw;
            }

            if(needVars != null && !visitor.IdExists(needVars))
            {
                temp.Add(new NoLineRoboError( needVars, FuncName));
            }

            if (listener.Errors != null)
            {
                temp.AddRange(listener.Errors);
            }

           
            return temp;
        }

        private void InitTree(string value, out CommonTokenStream commonTokenStream, out RoboParser.ProgramContext roboContext, out ParseTreeWalker walker)
        {
            var fileContents = value;

            AntlrInputStream inputStream = new AntlrInputStream(fileContents);

            RoboLexer speakLexer = new RoboLexer(inputStream);
            commonTokenStream = new CommonTokenStream(speakLexer);
            RoboParser roboParser = new RoboParser(commonTokenStream);
            roboContext = roboParser.program();
            walker = new ParseTreeWalker();
        }
    }
}
