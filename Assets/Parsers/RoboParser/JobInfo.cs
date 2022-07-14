using Assets.Scripts.Puzzles.Robo.Environment;
using UnityEngine;

namespace Assets.Parsers.RoboParser
{
    public abstract class JobInfo : RoboBaseBehaviour
    {
        public abstract bool MeetsCondition(TestSubject subject);
        //public abstract bool MeetsConcreteCondition(TestSubject subject);
    }
}