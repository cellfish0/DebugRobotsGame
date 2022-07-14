using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Assets.Parsers.RoboParser;
using ConsoleApp2.Test;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.VisualNodes.EditorWindow
{
    public class AutoFormatter : MonoBehaviour
    {
        Color32[] newVertexColors;
        public string FormatIndents(string value)
        {
            CommonTokenStream commonTokenStream;
            RoboParser.ProgramContext roboContext;
            ParseTreeWalker walker;


            InitTree(value, out commonTokenStream, out roboContext, out walker);

            IndentationListener listener = new IndentationListener(commonTokenStream);
            walker.Walk(listener, roboContext);

            return listener.GetTranslatedText();
        }

        public void FormatColor(TMP_Text ugui)
        {
            var coloredTokens = GetColoredTokens(ugui);

            foreach (var token in coloredTokens)
            {
                var textInfo = ugui.textInfo;
            
                for (int i = token.Start; i <= token.Stop; i++)
                {
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                    newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                    textInfo.characterInfo[i].style = FontStyles.Bold;
                
                    if (textInfo.characterInfo[i].isVisible)
                    {
                        ColorUtility.TryParseHtmlString(token.Color, out Color c0);

                        newVertexColors[vertexIndex + 0] = c0;
                        newVertexColors[vertexIndex + 1] = c0;
                        newVertexColors[vertexIndex + 2] = c0;
                        newVertexColors[vertexIndex + 3] = c0;

                        // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                        ugui.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                        //Canvas.ForceUpdateCanvases();
                    }
                }
            }

        }

        private List<ColoredToken> GetColoredTokens(TMP_Text text)
        {
            CommonTokenStream commonTokenStream;
            RoboParser.ProgramContext roboContext;
            ParseTreeWalker walker;


            InitTree(text.text, out commonTokenStream, out roboContext, out walker);

            ColorListener listener = new ColorListener(commonTokenStream);
            walker.Walk(listener, roboContext);
            return listener.GetColoredTokens();
        }

        private static void InitTree(string value, out CommonTokenStream commonTokenStream, out RoboParser.ProgramContext roboContext, out ParseTreeWalker walker)
        {
            var fileContents = value;

            AntlrInputStream inputStream = new AntlrInputStream(fileContents);

            RoboLexer speakLexer = new RoboLexer(inputStream);
            commonTokenStream = new CommonTokenStream(speakLexer);

            RoboParser roboParser = new RoboParser(commonTokenStream);
            roboContext = roboParser.program();
            walker = new ParseTreeWalker();
        }
    }
}
