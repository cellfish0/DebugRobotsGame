using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Util
{
    [CreateAssetMenu]
    public class TestSubjects : ScriptableObject
    {
        public List<TestSubject> testSubjects = new List<TestSubject>();
        public List<int> Serialize(List<TestSubject> subjects)
        {
            List<int> temp = new List<int>();
            foreach (var sub in subjects)
            {
                for (int i = 0; i < testSubjects.Count; i++)
                {
                    TestSubject sub2 = testSubjects[i];
                    if (sub.GetType() == sub2.GetType())
                    {
                        temp.Add(i);
                    }
                }
            }
            return temp;
        }

        public List<TestSubject> Deserialize(List<int> indeces)
        {
            List<TestSubject> temp = new List<TestSubject>();
            foreach (var index in indeces)
            {
                temp.Add(testSubjects[index]);
            }
            return temp;
        }
    }
}

