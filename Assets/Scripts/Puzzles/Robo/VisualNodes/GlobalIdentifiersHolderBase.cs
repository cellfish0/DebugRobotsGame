using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.Environment;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public abstract class GlobalIdentifiersHolderBase : MonoBehaviour
    {
        public GlobalIdentifiers Identifiers { get; private set; }

        public virtual void Init()
        {
            Identifiers = new GlobalIdentifiers
            {
                ["WriteLine"] = new RoboFunction(new ArgFilter(typeof(object)), WriteLine),
                ["HeatUp"] = new RoboFunction(new ArgFilter(typeof(TestSubject), typeof(float)), HeatUp),
                ["CoolDown"] = new RoboFunction(new ArgFilter(typeof(TestSubject), typeof(float)), CoolDown)
            };
        }

        private object WriteLine(object[] args)
        {
            RoboComponentHolder componentHolder = RoboScenePersistentObject.Instance.componentHolder as RoboComponentHolder;
            componentHolder.CommandHandler.AddCommand(new WriteLine(args));
            return null;
        }

        private object HeatUp(object[] args)
        {
            TestSubject subject = args[0] as TestSubject;
            float value = Convert.ToSingle(args[1]);

            subject.HeatUp(value);
            return null;
        }

        private object CoolDown(object[] args)
        {
            TestSubject subject = args[0] as TestSubject;
            float value = Convert.ToSingle(args[1]);

            subject.CoolDown(value);
            return null;
        }
    }
}