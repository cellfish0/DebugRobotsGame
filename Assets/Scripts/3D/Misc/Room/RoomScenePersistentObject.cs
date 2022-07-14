namespace Assets.Scripts._3D.Misc.Room
{
    public class RoomScenePersistentObject : ScenePersistentObjectBase
    {
        public RoomComponentHolder componentHolder;
        public static RoomScenePersistentObject Instance { get; private set; }

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