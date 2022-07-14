using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Assets.Parsers.RoboParser;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public abstract class IdentifiersBase
    {
        protected Dictionary<string, object> Identifiers = new Dictionary<string, object>();

        public object this[string index]
        {
            get => GetIdentifier(index);
            set => Assign(index, value);
        }

        public bool ContainsKey(string key)
        {
            return Identifiers.ContainsKey(key);
        }

        public object GetIdentifier(string key)
        {

            return Identifiers[key];
        }

        public void Assign(string key, object value)
        {

            Identifiers[key] = value;
        }
    }

    public class GlobalIdentifiers : IdentifiersBase
    {
        public GlobalIdentifiers()
        {
        }

        public void UpdateSubject(TestSubject subject)
        {
            Assign("TestSubject", subject);
        }

    }

    public class ArgumentsCheckResult
    {
        public enum CheckResult
        {
            Success,
            WrongType,
            WrongCount
        }

        public CheckResult Result = CheckResult.Success;
        public int offendingArg = -1;

        public ArgumentsCheckResult(CheckResult result)
        {
            Result = result;
        }
        public ArgumentsCheckResult(CheckResult result, int offendingArg)
        {
            Result = result;
            this.offendingArg = offendingArg;
        }
    }

    public abstract class ArgFilterBase
    {

        protected ArgFilterBase(params Type[] argTypes)
        {
            ArgTypes = argTypes.ToList();
        }

        public List<Type> ArgTypes { get; protected set; }

        public abstract ArgumentsCheckResult CheckArgument(object[] args);

    }

    public class ArgFilter : ArgFilterBase
    {
        public int Count => ArgTypes.Count;

        public ArgFilter(params Type[] argTypes) : base(argTypes)
        {
        }


        public override ArgumentsCheckResult CheckArgument(object[] args)
        {
            if (args.Length != Count)
            {
                return new ArgumentsCheckResult(ArgumentsCheckResult.CheckResult.WrongCount);
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].GetType() != ArgTypes[i])
                {
                    new ArgumentsCheckResult(ArgumentsCheckResult.CheckResult.WrongType, i);
                }
            }

            return new ArgumentsCheckResult(ArgumentsCheckResult.CheckResult.Success);
        }
    }

    public class NoArgsFilter : ArgFilterBase
    {

        public override ArgumentsCheckResult CheckArgument(object[] args)
        {
            if (args != null && args.Length > 0)
            {
                return new ArgumentsCheckResult(ArgumentsCheckResult.CheckResult.WrongCount);
            }

            return new ArgumentsCheckResult(ArgumentsCheckResult.CheckResult.Success);
        }
    }


    public class RoboFunction
    {
        public ArgFilterBase ArgFilter { get; }
        private Func<object[], object> Function;

       

        public RoboFunction(ArgFilterBase argFilter, Func<object[], object> function)
        {
            this.ArgFilter = argFilter;
            Function = function;
        }

        public ArgumentsCheckResult GetArgumentError(object[] args)
        {
            return ArgFilter.CheckArgument(args);
        }

        public object Invoke(object[] args)
        {
            return Function.Invoke(args);
        }
    }

}
