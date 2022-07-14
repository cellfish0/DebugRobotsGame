using Assets.Scripts._3D.Misc;
using Assets.Scripts.Puzzles.Robo.Environment;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Turtle
{
    public class TurtleScenePersistentObject : ScenePersistentObjectBase
    {
        
        public TurtleComponentHolder componentHolder;

        public static TurtleScenePersistentObject Instance { get; private set; }

        private void Awake()
        {
            Init();
        }

        protected override void Init()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            componentHolder.Init();
        }

        protected override void OnDispose()
        {
            Instance = null;
        }
    }
}
