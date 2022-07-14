using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Puzzles.Robo.Util;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JSONCreator))]
public class JSONCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Generate JSON"))
        {
            JSONCreator jsonCreator = (JSONCreator)target;
            jsonCreator.Generate();
        }
    }
}
