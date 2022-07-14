using System;
using System.Collections.Generic;
using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using UnityEngine.Events;

namespace Assets.Parsers.RoboParser
{
    public class RoboErrorListener : BaseErrorListener
    {
        public string FuncName { get; private set; }

        public RoboErrorListener(string funcName)
        {
            FuncName = funcName;
        }

        public List<RoboError> Errors { get; private set; } = new List<RoboError>();

        public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            string message;
            if (e != null && offendingSymbol != null)
                message = $"Unexpected symbol: \"{offendingSymbol.Text}\", was expecting {e.GetExpectedTokens()}";
            else
                message = msg;
            
            RoboError error = new RoboError(message);
            //throw error;
            Errors.Add(error);
        }
    }



    public class RoboError : Exception
    {
        public int Line { get; set; }
        public string RoboMessage { get; set; }
        public string FuncName;

        public RoboError() { }

        public RoboError(string message)
        {
            RoboMessage = message;
        }

        protected RoboError(string nodeName, int line)
        {
            Line = line;
            FuncName = nodeName;
        }

        public virtual string PrintMessage()
        {
            return $"In {FuncName}, line {Line.ToString()}: " + RoboMessage;
        }

    }


    public class NoLineRoboError : RoboError
    {
        public NoLineRoboError(string needVars, string funcName)
        {
            RoboMessage = $"Node has to contain variable named \"{needVars}\"";
            FuncName = funcName;
        }

        public override string PrintMessage()
        {
            return $"In {FuncName} " + RoboMessage;
        }
    }


    public class VariableUndefinedError : RoboError
    {
        public VariableUndefinedError(string nodeName, int line, string varName) : base(nodeName, line)
        {
            RoboMessage = $"Variable \"{varName}\" is undefined";
        }
    }

    public class FunctionUndefinedError : RoboError
    {
        public FunctionUndefinedError(string nodeName, int line, string varName) : base(nodeName, line)
        {
            RoboMessage = $"Function \"{varName}\" is undefined";
        }
    }

    public class BlockDefinitionError : RoboError
    {
        public BlockDefinitionError(string nodeName, int line, string varName) : base(nodeName, line)
        {
            RoboMessage = "You can't define variables inside a block" +
                          $"\n Please, define \"{varName}\" before the block and then assign it";
        }
    }

    public class IdentifierWithKeywordExistsError : RoboError
    {
        public IdentifierWithKeywordExistsError(string nodeName, int line, string varName, string keyword) : base(nodeName, line)
        {
            RoboMessage = $"You can only use the keyword \"{keyword}\" when defining a variable. However, this already exists:\"{varName}\"";
        }
    }

    public class CantReadError : RoboError
    {
        public CantReadError(string nodeName, int line, string varName) : base(nodeName, line)
        {
            RoboMessage = $"\"{varName}\" cannot be read directly";
        }
    }

    public class CantWriteError : RoboError
    {
        public CantWriteError(string nodeName, int line, string varName) : base(nodeName, line)
        {
            RoboMessage = $"\"{varName}\" cannot be set directly";
        }
    }

    public class ArgumentsNumberError : RoboError
    {
        public struct Args
        {
            public int neededNumber;
            public int number;
            public string funcName;
        }

        public ArgumentsNumberError(string nodeName, int line, Args args) : base(nodeName, line)
        {
            RoboMessage = $"Function \"{args.funcName}\" must be called with {args.neededNumber} argument(s). You were trying to call it with {args.number} argument(s)";
        }
    }

    public class ArgumentTypeError : RoboError
    {
        public struct Args
        {
            public string funcName;
            public Type needed;
            public Type passed;
        }

        public ArgumentTypeError(string nodeName, int line, Args args) : base(nodeName, line)
        {
            RoboMessage = $"You are trying to pass an argument of type {args.passed} to  \"{args.funcName}\", when it expects an argument of type {args.needed}";
        }
    }

    public class AssignmentTypeError : RoboError
    {
        public struct Args
        {
            public string varName;
            public object value;
        }
        public AssignmentTypeError(string nodeName, int line, Args args) : base(nodeName, line)
        {
           RoboMessage = $"Can't assign the value <color=red>{args.value}</color=red> of type {args.value.GetType()} to {args.varName}";
        }
    }

    public class MathError : RoboError
    {
        public struct Args
        {
            public string opName;
            public object left;
            public object right;
        }
        
        public MathError(string nodeName, int line, Args args) : base(nodeName, line)
        {
           RoboMessage = $"Cannot {args.opName} values of types {args.left?.GetType().Name} and {args.right?.GetType().Name}";
        }
    }

    public class NegativeValueError : RoboError
    {
        public NegativeValueError(string nodeName, int line, string funcName) : base(nodeName, line)
        {
            RoboMessage = $"Can't have negative values when using {funcName}!";
        }
    }

    public class NoError : RoboError
    {
    }

}