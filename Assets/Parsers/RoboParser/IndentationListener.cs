using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using ConsoleApp2.Test;

namespace Assets.Parsers.RoboParser
{
    class IndentationListener : RoboBaseListener
    {
        private CommonTokenStream tokens;
        private TokenStreamRewriter rewriter;
        private int indentLevel;


        public IndentationListener(CommonTokenStream tokens)
        {
            this.tokens = tokens;
            rewriter = new TokenStreamRewriter(tokens);

        }



        public override void EnterBlock([NotNull] ConsoleApp2.Test.RoboParser.BlockContext context)
        {

            indentLevel = 1;
           
           
            if(context.OP_BRACKET() == null || context.CL_BRACKET() == null)
            {
                return;
            }

            IList<IToken> lTokenList = tokens.Get(context.OP_BRACKET().Symbol.TokenIndex, context.CL_BRACKET().Symbol.TokenIndex);
            if (lTokenList == null) return;


            IVocabulary vocabulary = new ConsoleApp2.Test.RoboParser(tokens).Vocabulary;


            for (int i = 0; i < lTokenList.Count; i++)
            {

                IToken token = lTokenList[i];

                string symbolicName = vocabulary.GetSymbolicName(token.Type);
                if (token.Channel == 1 && token.Column == 0 && symbolicName != null && symbolicName != "COMMENT")
                {
                    rewriter.Delete(token);
                }

            }

            for (int i = 1; i < lTokenList.Count - 3; i++)
            {

                


                IToken token = lTokenList[i];

                if (vocabulary.GetSymbolicName(token.Type) == "NEWLINE")
                {

                    rewriter.InsertAfter(token, GetIndent(indentLevel));


                }

            }
            //rewriter.InsertBefore(context.start, GetIndent(indentLevel - 1));
            //rewriter.InsertBefore(context.stop, GetIndent(indentLevel - 1));
            /*
            rewriter.InsertBefore(context.start, GetIndent(indentLevel - 1));
            int tokenIndex = 0;
            bool indentedInvalid = false;
            foreach (var l in context.line())
            {
                
                foreach(var v in l.NEWLINE())
                {
                    if(v.Symbol.TokenIndex == -1)
                    {
                        tokenIndex = -1;
                        break;
                    }
                }

                var t = l.NEWLINE();
                //tokenIndex = l.NEWLINE(l.NEWLINE().Length - 1).Symbol.TokenIndex;
                if (l.exception != null)
                {
                    rewriter.InsertBefore(l.start, GetIndent(indentLevel));
                    indentedInvalid = false;
                }
                else
                {
                    if(!indentedInvalid)
                    {
                        rewriter.InsertBefore(l.start, GetIndent(indentLevel));
                        indentedInvalid = true;
                    }
                }
                
            }
            rewriter.InsertBefore(context.stop, GetIndent(indentLevel - 1));
            */
        }
        private bool NoBrackets(string text)
        {
            int op = 0;
            foreach (char c in text)
            {
                if (c == '{')
                {
                    op++;
                }
                if (c == '}')
                {
                    op++;
                }
            }
            return op == 0;
        }
        private bool AllBracketsClosed(string text)
        {

            int op = 0;

            foreach (char c in text)
            {
                if (c == '{')
                {
                    op++;
                }
                if (c == '}')
                {
                    op--;
                }
            }

            return op == 0;
        }


        public string GetTranslatedText()
        {
            return rewriter.GetText();
        }

        public string GetIndent(int indentLevel)
        {
            string temp = "";
            for (int i = 0; i < indentLevel; i++)
            {
                temp += "\t";
            }
            return temp;
        }
    }
}
