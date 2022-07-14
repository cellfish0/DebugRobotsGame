using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using ConsoleApp2.Test;
using ConsoleApp2;

namespace Assets.Parsers.RoboParser
{
    public class ColorListener : RoboBaseListener
    {

        private const string IF_COLOR = "#120ED6";
        private const string WHILE_COLOR = "#120ED6";
        private const string CONSTANT_COLOR = "#790CA3";
        private const string COMMENT_COLOR = "#C18240";
        private const string NEGATION_COLOR = "#D13A24";
        private const string KEYWORD_COLOR = "#C60DB2";
        private CommonTokenStream tokens;
        
        IVocabulary vocabulary;
        private List<ColoredToken> coloredTokens = new List<ColoredToken>();


        public ColorListener(CommonTokenStream tokens)
        {
            this.tokens = tokens;
            vocabulary = new ConsoleApp2.Test.RoboParser(tokens).Vocabulary;

        }

        public override void EnterProgram([NotNull] ConsoleApp2.Test.RoboParser.ProgramContext context)
        {
            IList<IToken> lTokenList = tokens.GetTokens();
            foreach (var token in lTokenList)
            {
                string symbolicName = vocabulary.GetSymbolicName(token.Type);

                if (symbolicName == "IF")
                {
                    SetColor(token, IF_COLOR);
                }

                if (symbolicName == "ELSE")
                {
                    SetColor(token, IF_COLOR);
                }

                if (symbolicName == "NEGATION_OPERATOR")
                {
                    SetColor(token, NEGATION_COLOR);
                }

                if (symbolicName == "WHILE")
                {
                    SetColor(token, WHILE_COLOR);
                }

                if (symbolicName == "INTEGER" ||
                    symbolicName == "FLOAT" ||
                    symbolicName == "STRING" ||
                    symbolicName == "BOOL")
                {
                    SetColor(token, CONSTANT_COLOR);
                }

                if (symbolicName == "COMMENT")
                {
                    SetColor(token, COMMENT_COLOR);
                }

                if (symbolicName == "GLOBAL_KEYWORD")
                {
                    SetColor(token, KEYWORD_COLOR);
                }
            }
        }
        
        private void SetColor(IToken token, string color)
        {
            ColoredToken coloredToken = new ColoredToken(token.StartIndex, token.StopIndex, color);
            coloredTokens.Add(coloredToken);
        }



        public List<ColoredToken> GetColoredTokens()
        {
            return coloredTokens;
        }
    }

    public class ColoredToken
    {
        public ColoredToken(int start, int stop, string color)
        {
            Start = start;
            Stop = stop;
            Color = color;
        }

        public int Start { get; private set; }
        public int Stop { get; private set; }
        public string Color { get; private set; }


    }
}
