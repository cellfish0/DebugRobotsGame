using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

//[CreateAssetMenu]
namespace Assets.Scripts.Puzzles.Robo.Util
{
    public class JSONCreator : ScriptableObject
    {
        [SerializeField] private List<ScriptAsString> scripts;
        [SerializeField] private string JSONLocation = "Assets/RoboScripts.JSON";
        public void Generate()
        {
            Dictionary<string, string> roboScripts = new Dictionary<string, string>();
            foreach (var script in scripts)
            {
                roboScripts.Add(script.name, script.text);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            string json = JsonConvert.SerializeObject(roboScripts, Formatting.Indented);
            File.WriteAllText(JSONLocation, json);
        }
    }
}



