using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Console;
using Assets.Scripts.Puzzles.Robo.Util;
using Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class RoboComponentHolder : ComponentHolderBase
    {
        [SerializeField] private bool editing;

        public Camera MainCam;
        public RoboConsole RoboConsole;
        public SocketConnector SocketConnector;
        public EditorWindowMenu EditorWindowPrefab;
        public Controls controls;
        public TestSubjects TestSubjects;
        public DataBaseLoader DataBaseLoader;
        public ProgramStarter ProgramStarter;
        public DraggableManager DraggableManager;
        public ProgramLoader ProgramLoader;
        public RoboSyntaxChecker RoboSyntaxChecker;
        public CommandHandler CommandHandler;
        public GlobalIdentifiers GlobalIdentifiers => ProgramStarter.Program.IdentifiersHolder.Identifiers;


        public override void Init()
        {
            Application.targetFrameRate = 60;
            controls = new Controls();
            controls.Enable();
            //RoboScripts = JsonConvert.DeserializeAnonymousType(RoboScriptsJSON.text, RoboScripts);


            Addressables.InitializeAsync().Completed += OnAddressablesInit;
        }

        private void OnAddressablesInit(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator> obj)
        {
            DataBaseLoader.Complete.AddListener(OnAllInit);
            DataBaseLoader.Init();
        }



        public void OnAllInit()
        {
            

            if (editing)
            {
                ProgramStarter.Init();
            }
            else
            {
                ProgramLoader.Init();
            }

            Initialize();
        }

        
    }
}
