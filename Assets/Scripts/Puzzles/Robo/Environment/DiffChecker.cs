using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ConsoleApp2.Test;
using UnityEngine;

namespace Assets.Scripts.Puzzles.Robo.Environment
{
    public class DiffChecker : MonoBehaviour
    {
 
        public float CheckDifferences(string before, string after)
        {
            //Debug.Log(before);
            //Debug.Log(after);
            var stream1 = GetStream(before);
            var stream2 = GetStream(after);
            WalkTree(stream1);
            WalkTree(stream2);
            
            Dictionary<int, List<int>> tokenPositions1 = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> tokenPositions2 = new Dictionary<int, List<int>>();

            IVocabulary vocabulary = GetVocabulary(before, after);

            foreach (var token in stream1.GetTokens())
            {
                InitDictionary(token, vocabulary, tokenPositions1);
            }

            foreach (var token in stream2.GetTokens())
            {
                InitDictionary(token, vocabulary, tokenPositions2);
            }

            int missingTokens = 0;
            int extraTokens = 0;

            int tokenCountDifference = 0;
            int tokenStartIndexDifference = 0;

            foreach (var key1 in tokenPositions1.Keys)
            {
                //Debug.Log(key1.Text);
                if(!tokenPositions2.ContainsKey(key1))
                {
                    missingTokens++;
                }
                else
                {
                    var dif = tokenPositions1[key1].Count - tokenPositions2[key1].Count;
                    tokenCountDifference += dif;
                    int len;

                    if (dif <= 0)
                    {
                        for (var index = 0; index < tokenPositions1[key1].Count; index++)
                        {
                            
                            tokenStartIndexDifference += Mathf.Abs(tokenPositions1[key1][index] - tokenPositions2[key1][index]);
                        }
                    }
                    
                    else
                    {
                        for (var index = 0; index < tokenPositions2[key1].Count; index++)
                        {
                            tokenStartIndexDifference += Mathf.Abs(tokenPositions1[key1][index] - tokenPositions2[key1][index]);
                        }
                    }
                    
                }
            }

            foreach (var key2 in tokenPositions2.Keys)
            {
                //Debug.Log(key2.Text);
                if (!tokenPositions1.ContainsKey(key2))
                {
                   extraTokens++;
                }
            }
            
            /*
            Debug.Log(MissingTokens);
            Debug.Log(ExtraTokens);
            Debug.Log(TokenCountDifference);
            Debug.Log(TokenStartIndexDifference);
            */

            return missingTokens + extraTokens + Mathf.Abs(tokenCountDifference * 0.25f) + tokenStartIndexDifference * 0.01f; 
        }

        private static void InitDictionary(IToken token, IVocabulary vocabulary, Dictionary<int, List<int>> tokenPositions)
        {
            if (token.Type == -1 || vocabulary.GetSymbolicName(token.Type) == "NEWLINE" || token.Channel == 1) return;
            if (!tokenPositions.ContainsKey(token.Type)) tokenPositions[token.Type] = new List<int>();
            tokenPositions[token.Type].Add(token.TokenIndex);
        }

        private IVocabulary GetVocabulary(string before, string after)
        {
            AntlrInputStream inputStream = new AntlrInputStream(after + "\n " + before);

            RoboLexer speakLexer = new RoboLexer(inputStream);
            return speakLexer.Vocabulary;
        }

        private CommonTokenStream GetStream(string fileContents)
        {
            AntlrInputStream inputStream = new AntlrInputStream(fileContents);

            RoboLexer speakLexer = new RoboLexer(inputStream);
            
            var commonTokenStream = new CommonTokenStream(speakLexer);
            return commonTokenStream;
        }

        private static void WalkTree(CommonTokenStream stream)
        {
            var walker = new ParseTreeWalker();
            RoboParser roboParser = new RoboParser(stream);
            walker.Walk(new RoboBaseListener(), roboParser.program());
            
        }


    }
}
