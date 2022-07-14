using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Assets.Parsers.RoboParser.Subjects;
using Assets.Scripts.Puzzles.Robo.Environment;
using ConsoleApp2.Test;

namespace Assets.Parsers.RoboParser
{
    public class RoboSimpleVisitor : RoboBaseVisitor<object>
    {
        private readonly BooleanHandler _booleanHandler;
        //private readonly MathOps _mathOps;
        private const int MAX_WHILE_REPEATS = 500;

        private string FuncName;

        public GlobalIdentifiers GlobalIdentifiers;
        public LocalIdentifiers LocalIdentifiers { get; } = new LocalIdentifiers();

        public BooleanHandler BooleanHandler => _booleanHandler;
        //public TestSubject Subject { get; }

        public RoboSimpleVisitor(string funcName)
        {
            GlobalIdentifiers = RoboScenePersistentObject.Instance.componentHolder.GlobalIdentifiers;

            this.FuncName = funcName;
            _booleanHandler = new BooleanHandler();
        }


        public override object VisitAssignment([NotNull] ConsoleApp2.Test.RoboParser.AssignmentContext context)
        {


            var varName = context.identifierOrMember().parent().GetText();
            var value = Visit(context.expression());

            IdentifiersBase Identifiers = LocalIdentifiers;
            if (context.keyword() != null && context.keyword().GLOBAL_KEYWORD() != null)
            {
                if (IdExists(varName))
                {
                    throw new IdentifierWithKeywordExistsError(FuncName, context.Start.Line, varName, context.keyword().GetText());
                }
                Identifiers = GlobalIdentifiers;
            }

            if (GlobalIdentifiers.ContainsKey(varName))
            {
                Identifiers = GlobalIdentifiers;
            }
            /*
            if (Identifiers.ContainsKey(varName) && IsFunction(varName))
            {
                throw new Exception($"The name {varName} is already used by a function");
            }
            */
            if (context.identifierOrMember().member(0) == null)
            {
                Identifiers[varName] = value;
            }
            else
            {
                SetLastMember(context, varName, value);
            }

            return null;
        }

        private bool IsFunction(string varName)
        {
            //careful
            return GlobalIdentifiers[varName] is RoboFunction;
        }

        public override object VisitConstant([NotNull] ConsoleApp2.Test.RoboParser.ConstantContext context)
        {

            var i = context.INTEGER();
            if (i != null)
            {
                var text = i.GetText();
                var deb = text.Length > 9 ? int.MaxValue : int.Parse(text);

                return deb;
            }

            var s = context.STRING();
            if (s != null)
            {
                string sText = s.GetText();
                return sText.Substring(1, sText.Length - 2);
            }

            var f = context.FLOAT();
            if (f != null)
            {
                var text = f.GetText();
                char separator = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator[0];

                var constant = text.Substring(0, text.IndexOf(separator)).Length > 6 ? float.MaxValue : float.Parse(text, CultureInfo.InvariantCulture);

                return constant;
            }

            var b = context.BOOL();
            if (b != null)
            {
                return b.GetText() == "true";
            }

            if (context.NULL() != null)
            {
                return null;
            }

            throw new NotImplementedException();
        }

        public override object VisitIdentifierExpression([NotNull] ConsoleApp2.Test.RoboParser.IdentifierExpressionContext context)
        {
            var parentName = context.identifierOrMember().parent().GetText();
            var varName = parentName;

            if (!IdExists(parentName))
            {
                throw new VariableUndefinedError(FuncName, context.Start.Line, parentName);
            }

            if (context.identifierOrMember().member(0) != null)
            {
                return GetLastMemberValue(context, parentName, out varName);
            }

            return GetIdValue(varName);
        }

        public object GetIdValue(string varName)
        {
            if (GlobalIdentifiers.ContainsKey(varName))
            {
                return GlobalIdentifiers[varName];
            }

            if (LocalIdentifiers.ContainsKey(varName))
            {
                return LocalIdentifiers[varName];
            }

            throw new Exception($"Variable {varName} undefined");
        }

        public bool IdExists(string Name)
        {
            return LocalIdentifiers.ContainsKey(Name) || GlobalIdentifiers.ContainsKey(Name);
        }

        public override object VisitNotExpression(ConsoleApp2.Test.RoboParser.NotExpressionContext context)
        {
            return !BooleanHandler.IsTrue(Visit(context.expression()));
        }

        private object GetLastMemberValue(ConsoleApp2.Test.RoboParser.IdentifierExpressionContext context, string parentName, out string varName)
        {
            int length = context.identifierOrMember().member().Length;
            ConsoleApp2.Test.RoboParser.MemberContext memberContext = context.identifierOrMember().member(length - 1);
            varName = memberContext.GetText();

            if (!IdExists(parentName))
            {
                throw new VariableUndefinedError(FuncName, context.Start.Line, parentName);
            }

            object parent = GetIdValue(parentName);
            object child = null;
            var members = context.identifierOrMember().member();

            foreach (ConsoleApp2.Test.RoboParser.MemberContext member in members)
            {
                var memberName = member.GetText();
                System.Reflection.PropertyInfo propertyInfo = parent.GetType().GetProperty(memberName, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    throw new VariableUndefinedError(FuncName, context.Start.Line, memberName);
                }

                HandleAccessors(propertyInfo, memberContext.Start.Line, memberName);

                child = propertyInfo.GetValue(parent);
                parent = child;
            }

            return child;
        }

        private void HandleAccessors(PropertyInfo propertyInfo, int line, string functionName, bool read = true)
        {

            var methodInfos = propertyInfo.GetAccessors(true);
            // Debug.Log(methodInfos.Length);
            for (int j = 0; j < methodInfos.Length; j++)
            {
                if (read)
                {
                    if (methodInfos[j].ReturnType != typeof(void) && !methodInfos[j].IsPublic)
                    {
                        throw new CantReadError(FuncName, line, functionName);
                    }
                }
                else
                {

                    if (methodInfos[j].ReturnType == typeof(void) && !methodInfos[j].IsPublic)
                    {
                        throw new CantWriteError(FuncName, line, functionName);
                    }

                }

            }
        }

        private object SetLastMember(ConsoleApp2.Test.RoboParser.AssignmentContext context, string parentName, object value)
        {

            int length = context.identifierOrMember().member().Length;
            ConsoleApp2.Test.RoboParser.MemberContext memberContext = context.identifierOrMember().member(length - 1);
            var varName = memberContext.GetText();

            if (!IdExists(parentName))
            {
                throw new VariableUndefinedError(FuncName, context.Start.Line, parentName);
            }

            object parent = GetIdValue(parentName);
            object child = null;
            var members = context.identifierOrMember().member();

            for (int i = 0; i < members.Length - 1; i++)
            {

                ConsoleApp2.Test.RoboParser.MemberContext member = members[i];
                string name = member.GetText();
                var propertyInfo = parent.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new VariableUndefinedError(FuncName, context.Start.Line, name);
                }

                child = propertyInfo.GetValue(parent);
                parent = child;
            }


            var memberInfo = parent.GetType().GetProperty(varName);
            if (parent == null || memberInfo == null)
            {
                throw new VariableUndefinedError(FuncName, context.Start.Line, varName);
            }
            HandleAccessors(memberInfo, memberContext.Start.Line, varName, false);

            var t = memberInfo.GetValue(parent).GetType();
            try
            {
                memberInfo.SetValue(parent, value);
            }
            catch
            {
                AssignmentTypeError.Args a = new AssignmentTypeError.Args()
                {
                    value = value,
                    varName = varName
                };
                throw new AssignmentTypeError(FuncName, context.Start.Line, a);
            }

            return null;
        }

        private object InvokeLastMethod(ConsoleApp2.Test.RoboParser.FunctionCallContext context, string parentName, object[] args)
        {

            int length = context.identifierOrMember().member().Length;
            ConsoleApp2.Test.RoboParser.MemberContext memberContext = context.identifierOrMember().member(length - 1);
            var varName = memberContext.GetText();

            if (!IdExists(parentName))
            {
                throw new VariableUndefinedError(FuncName, context.Start.Line, varName);
            }

            object parent = GetIdValue(parentName);
            object child = null;
            var members = context.identifierOrMember().member();

            for (int i = 0; i < members.Length - 1; i++)
            {
                ConsoleApp2.Test.RoboParser.MemberContext member = members[i];
                var functionName = member.GetText();
                var propertyInfo = parent.GetType().GetProperty(functionName, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    throw new FunctionUndefinedError(FuncName, context.Start.Line, functionName);
                }

                HandleAccessors(propertyInfo, memberContext.Start.Line, functionName);
                child = propertyInfo.GetValue(parent);
                parent = child;
            }

            MethodInfo methodInfo = parent.GetType().GetMethod(varName, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            if (methodInfo == null)
            {
                throw new FunctionUndefinedError(FuncName, context.Start.Line, varName);
            }

            ParameterInfo[] pars = methodInfo.GetParameters();
            var types = (from par in pars
                        select par.ParameterType).ToArray();

            ArgFilter filter = new ArgFilter(types);

            var error = GetError(context, filter, args, varName);

            if (!(error is NoError))
            {
                throw error;
            }


            var result = methodInfo.Invoke(parent, args);

            return result;
        }

        public override object VisitAdditiveExpression([NotNull] ConsoleApp2.Test.RoboParser.AdditiveExpressionContext context)
        {
            MathOps _mathOps = new MathOps(FuncName, context.Start.Line);
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.addOp().GetText();



            switch (op)
            {
                case "+":
                    object result = _mathOps.Add(left, right);
                    return result;
                case "-":
                    return _mathOps.Subtract(left, right);
            }

            throw new NotSupportedException();

        }
        public override object VisitMultiplicativeExpression([NotNull] ConsoleApp2.Test.RoboParser.MultiplicativeExpressionContext context)
        {
            MathOps _mathOps = new MathOps(FuncName, context.Start.Line);
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.multOp().GetText();


            switch (op)
            {
                case "*":
                    object result = _mathOps.Multiply(left, right);
                    return result;
                case "/":
                    return _mathOps.Divide(left, right);
                case "%":
                    return _mathOps.Remainder(left, right);
            }

            throw new NotSupportedException();
        }
        public override object VisitComparisonExpression([NotNull] ConsoleApp2.Test.RoboParser.ComparisonExpressionContext context)
        {
            MathOps _mathOps = new MathOps(FuncName, context.Start.Line);
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.compareOp().GetText();

            switch (op)
            {
                case "==":
                    object result = _mathOps.IsEqual(left, right);
                    return result;
                case ">":
                    return _mathOps.MoreThan(left, right);
                case "<":
                    return _mathOps.LessThan(left, right);

                case ">=":
                    return _mathOps.MoreOrEq(left, right);
                case "<=":
                    return _mathOps.LessOrEq(left, right);
            }

            throw new NotSupportedException();
        }

        public override object VisitBoolExpression([NotNull] ConsoleApp2.Test.RoboParser.BoolExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.boolOp().GetText();

            switch (op)
            {
                case "and":
                    return BooleanHandler.IsTrue(left) && BooleanHandler.IsTrue(right);
                case "or":
                    return BooleanHandler.IsTrue(left) || BooleanHandler.IsTrue(right);
            }

            throw new NotImplementedException();
        }
        public override object VisitParenthesizedExpression([NotNull] ConsoleApp2.Test.RoboParser.ParenthesizedExpressionContext context)
        {
            return Visit(context.expression());
        }
        public override object VisitFunctionCall([NotNull] ConsoleApp2.Test.RoboParser.FunctionCallContext context)
        {
            ConsoleApp2.Test.RoboParser.ParentContext parentContext = context.identifierOrMember().parent();
            var funcName = parentContext.GetText();
            object result = null;
            object[] args = context.expression().Select(Visit).ToArray();

            if (context.identifierOrMember().member(0) != null)
            {
                result = InvokeLastMethod(context, funcName, args);
            }
            else
            {
                if (GlobalIdentifiers.ContainsKey(funcName) && IsFunction(funcName))
                {
                    var f = GlobalIdentifiers[funcName] as RoboFunction;

                    RoboError roboError = GetError(context, f.ArgFilter, args, funcName);

                    if (roboError is NoError)
                    {

                        try
                        {
                            result = f.Invoke(args);
                        }
                        catch (RoboError e)
                        {
                            e.FuncName = FuncName;
                            e.Line = context.Start.Line;
                            throw;
                        }
                        
                    }
                    else
                    {
                        throw roboError;
                    }
                }
                else
                {

                    throw new FunctionUndefinedError(FuncName, context.Start.Line, funcName);
                }
            }
            return result;
        }

        private RoboError GetError(ParserRuleContext context, ArgFilterBase f, object[] args, string funcName)
        {
            ArgumentsCheckResult checkResult = f.CheckArgument(args);
            RoboError roboError;

            if (checkResult.Result == ArgumentsCheckResult.CheckResult.Success)
            {
                roboError = new NoError();
            }

            else if (checkResult.Result == ArgumentsCheckResult.CheckResult.WrongType)
            {
                var a = new ArgumentTypeError.Args
                {
                    funcName = funcName,
                    needed = f.ArgTypes[checkResult.offendingArg],
                    passed = args[checkResult.offendingArg].GetType()
                };
                roboError = new ArgumentTypeError(FuncName, context.start.Line, a);
            }
            else if (checkResult.Result == ArgumentsCheckResult.CheckResult.WrongCount)
            {
                var a = new ArgumentsNumberError.Args
                {
                    funcName = funcName,
                    neededNumber = f.ArgTypes.Count,
                    number = args.Length
                };
                roboError = new ArgumentsNumberError(FuncName, context.start.Line, a);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            return roboError;
        }
        public override object VisitWhileBlock([NotNull] ConsoleApp2.Test.RoboParser.WhileBlockContext context)
        {
            int safeCounter = 0;
            var t = context.GetText();
            while (BooleanHandler.IsTrue(Visit(context.expression())) && safeCounter < MAX_WHILE_REPEATS)
            {
                Visit(context.block());
                safeCounter++;
            }
            return null;
        }
        public override object VisitIfBlock([NotNull] ConsoleApp2.Test.RoboParser.IfBlockContext context)
        {
            if (BooleanHandler.IsTrue(Visit(context.expression())))
            {
                Visit(context.block());
            }
            else if (context.elseIfBlock() != null)
            {
                Visit(context.elseIfBlock());
            }
            return null;
        }

        public override object VisitBlock(ConsoleApp2.Test.RoboParser.BlockContext context)
        {

            var lineContexts = context.line();
            if (lineContexts != null)
            {
                foreach (var lineContext in lineContexts)
                {
                    if (lineContext.statement() == null) continue;

                    var assignmentContext = lineContext.statement().assignment();
                    if (lineContext.statement() != null && assignmentContext != null)
                    {
                        string varName = assignmentContext.identifierOrMember().parent().GetText();
                        if (!IdExists(varName))
                        {
                            throw new BlockDefinitionError(FuncName, context.Start.Line, varName);
                        }
                    }
                }
            }


            return base.VisitBlock(context);
        }
    }
}
