using System.Collections.Generic;
using Assets.Parsers.RoboParser;
using Assets.Parsers.RoboParser.Subjects;
using Assets.Scripts.Puzzles.Robo.Console;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Nodes;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes
{
    public class VisualProgram : RoboBaseBehaviour
    {
        private const int MAX_REPEATS = 100;

        public UnityEvent<List<RoboError>> ErrorsFound;
        
        [SerializeField] private Transform nodesRoot;
        [SerializeField] private Transform windowRoot;
        [SerializeField] private JobInfo JobInfo;
        [SerializeField] private GlobalIdentifiersHolderBase identifiersHolder;
        [SerializeField] private LinkHolder linkHolder;
        [SerializeField] private List<VisualNodeBase> visualNodes = new List<VisualNodeBase>();
        [SerializeField] private bool execute;
        int counter = 0;
        private TestSubject initialSubject;
        private TestSubject subject;

        private StartVisualNode startNode;

        public LinkHolder LinkHolder
        {
            get => linkHolder;
            private set => linkHolder = value;
        }

        public IEnumerable<VisualNodeBase> VisualNodes => visualNodes;

        public Transform WindowRoot => windowRoot;

        public GlobalIdentifiersHolderBase IdentifiersHolder
        {
            get => identifiersHolder;
            set => identifiersHolder = value;
        }

        public void Init()
        {
            identifiersHolder.Init();
            LinkHolder.Init();
            ErrorsFound.AddListener(RoboConsole.DisplayErrors);

            foreach (Transform child in nodesRoot)
            {
                if (!child.TryGetComponent(out VisualNodeBase visualNode)) continue;

                visualNode.Init();
                visualNodes.Add(visualNode);

                StartVisualNode startVisualNode = visualNode as StartVisualNode;
                if (startVisualNode != null)
                {
                    startNode = startVisualNode;
                }
            }

            SetupLinks();

        }

        private void SetupLinks()
        {
            foreach (Link link in LinkHolder.Links)
            {
                foreach (VisualNodeBase visualNode in visualNodes)
                {
                    foreach (OutSocket outSocket in visualNode.Outs)
                    {
                        if (link.From == outSocket)
                        {
                            //outSocket.AddLink(link.To);
                            SocketConnector.CreateLinkSilent(link.From, link.To);
                        }
                    }
                }
            }
        }

        public TestSubject UpdateSubject(TestSubject original)
        {
            this.subject = Instantiate(original);
            GlobalIdentifiers.Assign("TestSubject", this.subject);
            return this.subject;
        }

        private void Update()
        {
            if (execute)
            {
                ExecuteProgram();
                execute = false;
            }
        }


        public void ExecuteProgram()
        {
            CommandHandler.QuietMode = false;

            RegenerateSubjects();
            if (HasSyntaxErrors()) return;
            RegenerateSubjects();

            //Debug.Log(s.TemperatureC);
            if (HasParserErrors()) return;

            OutputResults();
        }

        private TestSubject RegenerateSubjects()
        {
            startNode.ResetSubjects();
            var s = UpdateSubject(startNode.Subject);
            ResetIdentifiers(s);
            return s;
        }

        private void ResetIdentifiers(TestSubject subject)
        {
            identifiersHolder.Init();
            identifiersHolder.Identifiers.UpdateSubject(subject);
        }

        private bool HasParserErrors()
        {
            try
            {
                
                ExecuteNode(startNode);
            }
            catch (RoboError e)
            {
                ErrorsFound.Invoke(new List<RoboError> { e });
                return true;
            }
           
            return false;
        }

        private bool HasSyntaxErrors()
        {
            var errors = RoboSyntaxChecker.CheckAll(visualNodes);
            if (errors.Count != 0)
            {
                ErrorsFound.Invoke(errors);
                return true;
            }
            

            return false;
        }


        public ProgramSubmitResult TrySubmitProgram()
        {
            CommandHandler.QuietMode = true;
           

            var testSubjects = (startNode.Node as StartNode)?.testSubjects;
            List<TestSubject> new_testSubjects = new List<TestSubject>();

            foreach (var testSubject in testSubjects)
            {
                startNode.ResetSubjects();
                var s = UpdateSubject(testSubject);
                ResetIdentifiers(s);

                var errors = RoboSyntaxChecker.CheckAll(visualNodes);
                if (errors.Count != 0)
                {
                    ErrorsFound.Invoke(errors);
                    return new CompilerErrors();
                }

                startNode.ResetSubjects();
                s = UpdateSubject(testSubject);
                ResetIdentifiers(s);

                try
                {
                    ExecuteNode(startNode);
                }
                catch (RoboError e)
                {
                    ErrorsFound.Invoke(new List<RoboError> { e });
                    return new CompilerErrors();
                }

                new_testSubjects.Add(subject);
            }

            

            return CheckJobSuccess(new_testSubjects);
        }

        private ProgramSubmitResult CheckJobSuccess(List<TestSubject> subjects)
        {
            ProgramSubmitResult result = new Success();
            foreach (var sub in subjects)
            {
                if (!JobInfo.MeetsCondition(sub))
                {
                    result = new JobNotDoneDecorator(result);
                }

                var human = sub as Human;
                if (human != null && human.IsDead())
                {
                    result = new HumanDeadDecorator(result);
                }
            }

            return result;
        }



    private void OutputResults()
        {
            RoboConsole.WriteLine("<color=yellow>======================");
            RoboConsole.WriteLine($"<color=yellow>Stats for {subject.Name}:");
            RoboConsole.WriteLine($"<color=yellow>Is Human: {subject.IsHuman()}");
            RoboConsole.WriteLine($"<color=yellow>Is Dead: {subject.IsDead()}");
            RoboConsole.WriteLine($"<color=yellow>Temperature (Celsius): {subject.TemperatureC}");
            RoboConsole.WriteLine($"<color=yellow>Temperature (Fahrenheit): {subject.TemperatureF}");
            RoboConsole.WriteLine("<color=yellow>======================");
            RoboConsole.WriteLine("<color=yellow>END OF ASSESSMENT");
            RoboConsole.WriteLine("<color=yellow>======================");
        }

        private void ExecuteNode(VisualNodeBase visualNode)
        {
            counter++;
            visualNode.Execute(subject);
            HashSet<VisualNodeBase> nextNodes = visualNode.GetNext();

            foreach (var node in nextNodes)
            {
                if (counter < MAX_REPEATS)
                    ExecuteNode(node);
            }
        }

    }


    public abstract class ProgramSubmitResult
    {

        public enum Message
        {
            None,
            Success,
            HumanDead,
            JobNotDone,
            HumanDeadAndJobNotDone,
            CompilerErrors
        }
        public abstract Message GetMessage();

    }

    public class CompilerErrors : ProgramSubmitResult
    {
        public override Message GetMessage()
        {
            return Message.CompilerErrors;
        }
    }

    public class Success : ProgramSubmitResult
    {
        public override Message GetMessage()
        {
            return Message.Success;
        }
    }

    public class JobNotDoneDecorator : ProgramSubmitResult
    {
        protected ProgramSubmitResult result;

        public JobNotDoneDecorator(ProgramSubmitResult result)
        {
            this.result = result;
        }

        public override Message GetMessage()
        {
            return Decorate((dynamic)result);
        }

        private Message Decorate(Success result)
        {
            return Message.JobNotDone;
        }

        private Message Decorate(JobNotDoneDecorator result)
        {
            return Message.JobNotDone;
        }
        private Message Decorate(HumanDeadDecorator result)
        {
            return Message.HumanDeadAndJobNotDone;
        }
    }

    public class HumanDeadDecorator : ProgramSubmitResult
    {
        protected ProgramSubmitResult result;

        public HumanDeadDecorator(ProgramSubmitResult result)
        {
            this.result = result;
        }

        public override Message GetMessage()
        {
            return Decorate((dynamic)result);
        }

        private Message Decorate(Success result)
        {
            return Message.HumanDead;
        }

        private Message Decorate(JobNotDoneDecorator result)
        {
            return Message.HumanDeadAndJobNotDone;
        }

        private Message Decorate(HumanDeadDecorator result)
        {
            return Message.HumanDead;
        }
    }
}
