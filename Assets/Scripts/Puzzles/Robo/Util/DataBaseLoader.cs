using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.Nodes;
using Assets.Scripts.Puzzles.Robo.VisualNodes;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Puzzles.Robo.Util
{



  
    public class DataBaseLoader : MonoBehaviour
    {
        [HideInInspector] public UnityEvent Complete;
        public bool Initialized { get; private set; } = false;

        [SerializeField] private List<NodeBase> nodeTemplates = new List<NodeBase>();
        [SerializeField] private List<ScriptAsString> nodeScripts = new List<ScriptAsString>();
        [SerializeField] private List<GameObject> programPrefabs = new List<GameObject>();
        private bool TemplatesReady;
        private bool ScriptsReady;
        private List<IInitializable> initializables = new List<IInitializable>();

        public List<NodeBase> NodeTemplates { get => nodeTemplates; set => nodeTemplates = value; }

        public List<GameObject> ProgramPrefabs => programPrefabs;

        public void Init()
        {
            Initialized = false;

            CreateLoader("NodeTemplates", nodeTemplates);
            CreateLoader("NodeScripts", nodeScripts);
            CreateLoader("Programs", programPrefabs);
        }

        private void CreateLoader<T>(string label, IList<T> collection)
        {
            var loader = new AddressableListLoader<T>();
            loader.Complete += CheckAllComplete;
            loader.LoadAssets(label, collection);
            initializables.Add(loader);
        }



        private void CheckAllComplete()
        {

            foreach (var initializable in initializables)
            {

                if (initializable ==null || !initializable.Initialized)
                {
                    return;
                }
            }

            foreach (var initializable in initializables)
            {
                if (initializable == null || !initializable.Initialized)
                {
                    initializable.Complete -= CheckAllComplete;
                }
            }
            //Debug.Log(programPrefabs);
            Complete.Invoke();
        }



        public string GetNodeScript(string Name)
        {
            foreach (var node in nodeScripts)
            {
                if (node.Name == Name)
                    return node.text;
            }
            return null;
        }

        public NodeBase GetTemplateByType(NodeType type)
        {
            foreach (var template in NodeTemplates)
            {
                if(template.type == type)
                {
                    return Object.Instantiate(template);
                }
            }
            throw new System.NotImplementedException();
        }

    
    }
}
