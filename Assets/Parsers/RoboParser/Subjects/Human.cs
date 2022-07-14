using UnityEngine;

namespace Assets.Parsers.RoboParser.Subjects
{
    [CreateAssetMenu(menuName = "TestSubjects/Human")]
    public class Human : TestSubject
    {
        public Human()
        {
            TemperatureC = 36.6f;
        }


        public override bool IsHuman() => true;

        public override bool IsDead()
        {
            if(TemperatureC < 24f || TemperatureC > 45f)
            {
                return true;
            }
            return false;
        }  
    }

    public class PlaceholderSubject : TestSubject
    {
        public override bool IsDead()
        {
            return false;
        }

        public override bool IsHuman()
        {
            return false;
        }
    }
}
