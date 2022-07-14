using Assets.Scripts._3D.Misc;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class RoboScenePersistentObject : ScenePersistentObjectBase
    {

        public RoboComponentHolder componentHolder;
        public static RoboScenePersistentObject Instance { get; private set; }

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
