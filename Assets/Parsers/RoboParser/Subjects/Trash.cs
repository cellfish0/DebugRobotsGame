using UnityEngine;

namespace Assets.Parsers.RoboParser.Subjects
{
    [CreateAssetMenu(menuName = "TestSubjects/Trash")]
    public class Trash : TestSubject
    {
        public Trash()
        {
            TemperatureC = 20f;
        }

        
        public override bool IsHuman() => false;



        public override bool IsDead()
        {
            return false;
        }
    }
}
