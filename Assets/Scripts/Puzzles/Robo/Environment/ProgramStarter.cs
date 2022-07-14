using Assets.Parsers.RoboParser;
using Assets.Scripts.Puzzles.Robo.VisualNodes;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class ProgramStarter : RoboBaseBehaviour
    {
        [SerializeField] private VisualProgram _program;

        public VisualProgram Program { get => _program; }

        public void Init()
        {
            Program.Init();
        }

        public void Init(VisualProgram program)
        {
            _program = program;
            Program.Init();
        }

        public void StartProgram()
        {
            Program.ExecuteProgram();
        }

        public void Quit()
        {
            Destroy(Program.gameObject);
            _program = null;
        }

        public ProgramSubmitResult TrySubmit()
        {
            return _program.TrySubmitProgram();
        }
    }
}
