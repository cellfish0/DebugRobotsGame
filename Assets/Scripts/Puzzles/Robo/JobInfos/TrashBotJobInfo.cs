using Assets.Parsers.RoboParser;
using Assets.Parsers.RoboParser.Subjects;

namespace Assets.Scripts.Puzzles.Robo.JobInfos
{
    public class TrashBotJobInfo : JobInfo
    {

        public override bool MeetsCondition(TestSubject subject)
        {
           
            return MeetsConcreteCondition((dynamic)subject);
        }

        private bool MeetsConcreteCondition(TestSubject subject)
        {
            return true;
        }

        private bool MeetsConcreteCondition(Trash subject)
        {
            
            return subject.TemperatureC >= 1100f;
        }
    }
}