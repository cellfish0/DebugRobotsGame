using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Console;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    //access this the same way you accessed the Console!
    public class CommandHandler : RoboBaseBehaviour
    {
        public  bool QuietMode { get; set; }

        private List<Command> commands = new List<Command>();

        public void AddCommand(Command command)
        {
            commands.Add(command);
            if (!QuietMode)
            {
                command.Execute();
            }
        }
    }

    public abstract class Command
    {
        public  abstract void Execute();
    }

    public class WriteLine : Command
    {
        public object[] args;


        public WriteLine(object[] args)
        {
            this.args = args;
        }

        public override void Execute()
        {
            foreach (var o in args)
            {
                RoboScenePersistentObject.Instance.componentHolder.RoboConsole.WriteLine(o);
            }
        
        }
    }
}