using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Environment;
using Assets.Scripts.Puzzles.Robo.VisualNodes;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;



using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Util
{
    [ExecuteAlways]
    public class LinkSaver : MonoBehaviour
    {

        [SerializeField] private ProgramStarter programStarter;
        [SerializeField] private LinkSaverSO saver;
        bool initialized = false;

        private void Awake()
        {
            if (!initialized)
            {
                EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
                initialized = true;
            }

        }

        private void OnDestroy()
        {
            EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
            initialized =false;
        }
        private void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
        {
            
            if (obj == PlayModeStateChange.ExitingPlayMode)
            {
                SaveLinks();
            }
            if (obj == PlayModeStateChange.EnteredEditMode)
            {
                SetLinks();
            }
        }

        private void SaveLinks()
        {
            //Debug.Log("Saved");
            LinkHolder linkHolder = programStarter.Program.LinkHolder;
            EditorUtility.SetDirty(linkHolder);
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
            saver.links = new List<Link>(linkHolder.Links);

        }

        private void SetLinks()
        {

            LinkHolder linkHolder = programStarter.Program.LinkHolder;
            //Debug.Log(saver); 
            linkHolder.Links = saver.links;

        }

    }
}
#endif