
using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Nodes;
using Assets.Scripts.Puzzles.Robo.VisualNodes;
using UnityEngine;

namespace Assets.Parsers.RoboParser
{
    public class GameStateManager : RoboBaseBehaviour   
    {
        [SerializeField] private DiffChecker diffChecker;
        [SerializeField] private ProgramStarter programStarter;
        public void TrySubmitProgram()
        {
            var result = programStarter.TrySubmit();
            float difference = -1;
            if (result.GetMessage() == ProgramSubmitResult.Message.Success)
            {
                var nodes = programStarter.Program.VisualNodes;
                
                foreach (var node in nodes)
                {
                    
                    if (node.Node.Text == null || node.GetText() == null)
                    {
                        //Debug.Log($"no text for node {node.Node}");
                        continue;
                    }
                    difference += diffChecker.CheckDifferences(node.Node.Text, node.GetText());
                }
                RoboConsole.DrawHorizontalLine();
                RoboConsole.WriteLine($"Good job! You solved the puzzle correctly:)\n Your difference index was {difference}");
            }
            else if (result.GetMessage() == ProgramSubmitResult.Message.CompilerErrors)
            {
                RoboConsole.DrawHorizontalLine();
                RoboConsole.WriteLine("COMPILER ERRORS??? LMAO....");
            }
            else if (result.GetMessage() == ProgramSubmitResult.Message.JobNotDone)
            {
                RoboConsole.DrawHorizontalLine();
                RoboConsole.WriteLine("THE JOB IS NOT DONE... LMAO....");
            }
            else if (result.GetMessage() == ProgramSubmitResult.Message.HumanDeadAndJobNotDone)
            {
                RoboConsole.DrawHorizontalLine();
                RoboConsole.WriteLine("THE HUMAN DIED AND... THE JOB IS NOT DONE??");
            }
            else if (result.GetMessage() == ProgramSubmitResult.Message.HumanDead)
            {
                RoboConsole.DrawHorizontalLine();
                RoboConsole.WriteLine("Nice! The human's dead though");
            }

        }

        
    }
}
