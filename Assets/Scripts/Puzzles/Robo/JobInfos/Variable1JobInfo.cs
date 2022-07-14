using Assets.Parsers.RoboParser;

namespace Assets.Scripts.Puzzles.Robo.JobInfos
{
    public class Variable1JobInfo : JobInfo
    {
    
        public override bool MeetsCondition(TestSubject subject)
        {
            return GlobalIdentifiers["a"] is string s && s == "Hello!";
        }
    }
}
