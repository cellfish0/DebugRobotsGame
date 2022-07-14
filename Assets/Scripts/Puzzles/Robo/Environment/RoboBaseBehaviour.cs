using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.Console;
using Assets.Scripts.Puzzles.Robo.Util;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class RoboBaseBehaviour : MonoBehaviour
    {
        private RoboComponentHolder componentHolder;
        protected SocketConnector SocketConnector => componentHolder.SocketConnector;
        protected Controls Controls => componentHolder.controls;
        protected DataBaseLoader DataBaseLoader => componentHolder.DataBaseLoader;
        protected TestSubjects TestSubjects => componentHolder.TestSubjects;
        protected DraggableManager DraggableManager => componentHolder.DraggableManager;
        protected ProgramStarter ProgramStarter => componentHolder.ProgramStarter;
        protected RoboSyntaxChecker RoboSyntaxChecker => componentHolder.RoboSyntaxChecker;
        protected RoboConsole RoboConsole => componentHolder.RoboConsole;
        protected CommandHandler CommandHandler => componentHolder.CommandHandler;
        protected GlobalIdentifiers GlobalIdentifiers => componentHolder.GlobalIdentifiers;
        private bool initialized;

        private void Awake()
        {
            componentHolder = RoboScenePersistentObject.Instance.componentHolder;
            
            if(componentHolder.IsInitialized)
            {
                BaseInit();
            }
            else
            {
                componentHolder.Initialized.AddListener(BaseInit);
            }
            
        }
        protected virtual void BaseInit()
        {
           if(initialized) return;
           
            //SocketConnector = componentHolder.SocketConnector;
            //Controls = componentHolder.controls;
            //DataBaseLoader = componentHolder.DataBaseLoader;
            //TestSubjects = componentHolder.TestSubjects;
            //DraggableManager = componentHolder.DraggableManager;
            //ProgramStarter = componentHolder.ProgramStarter;
            //RoboSyntaxChecker = componentHolder.RoboSyntaxChecker;
            //RoboConsole = componentHolder.RoboConsole;
            //CommandHandler = componentHolder.CommandHandler;
           
            initialized = true;
        }


    }
}

