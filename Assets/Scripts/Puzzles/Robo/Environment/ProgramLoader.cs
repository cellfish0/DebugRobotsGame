using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Util;
using Assets.Scripts.Puzzles.Robo.VisualNodes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class ProgramLoader : RoboBaseBehaviour
    {

        private List<GameObject> programs;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private Menu menu;
        [SerializeField] private Transform root;

        public void Init()
        {
            BaseInit();
            programs = DataBaseLoader.ProgramPrefabs;
       

            for (var i = 0; i < programs.Count; i++)
            {
                var temp = Instantiate(buttonPrefab, grid.transform);
                temp.GetComponentInChildren<TMP_Text>().text = $"Program {(i + 1).ToString()}";
                temp.onClick.AddListener(menu.Hide);
                var i1 = i;
                temp.onClick.AddListener(() => LoadProgram(programs[i1]));
            }
        }

        public void LoadProgram(GameObject program)
        {
            var programClone = Instantiate(program, root);
            ProgramStarter.Init(programClone.GetComponent<VisualProgram>());
        }


    }
}
