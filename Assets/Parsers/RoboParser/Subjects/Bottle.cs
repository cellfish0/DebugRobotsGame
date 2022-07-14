using UnityEngine;

namespace Assets.Parsers.RoboParser.Subjects
{
    [CreateAssetMenu(menuName = "TestSubjects/Bottle")]
    public class Bottle : TestSubject
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
