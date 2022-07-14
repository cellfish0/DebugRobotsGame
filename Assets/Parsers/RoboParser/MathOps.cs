using System;

namespace Assets.Parsers.RoboParser
{
    public class MathOps
    {
        private string nodeName;
        private int line;

        public MathOps(string nodeName, int line)
        {
            this.nodeName = nodeName;
            this.line = line;
        }
        public object Add(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l + r;
            }

            if (left is float lf && right is float rf)
            {
                return lf + rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt + rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat + rInt;
            }

            if (left is string || right is string)
            {
                return $"{left}{right}";
            }

            //throw new RoboError(nodeName, line, $"Cannot add values of types {left?.GetType()} and {right?.GetType()}");
            
            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "add"
            };
            throw new MathError(nodeName, line, a);
        }

        public object Subtract(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l - r;
            }

            if (left is float lf && right is float rf)
            {
                return lf - rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt - rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat - rInt;
            }

            if (left is string || right is string)
            {

                string result = "";

                int len = right.ToString().Length;
                string stringL = left.ToString();
                string stringR = right.ToString();

                string rest = "";


                if (stringL.Length < stringR.Length)
                {
                    len = stringL.Length;
                    rest = stringR.Substring(stringL.Length - 1);
                }
                else if (stringL.Length > stringR.Length)
                {
                    len = stringR.Length;
                    rest = stringL.Substring(stringR.Length - 1);
                }

                for (int i = 0; i < len; i++)
                {
                    int index = Math.Abs((int)stringL[i] - (int)stringR[i]);
                    index += char.IsLetter(stringL[i]) || char.IsLetter(stringR[i]) ? 64 : 0;

                    index += char.IsNumber(stringL[i]) || char.IsNumber(stringR[i]) ? 48 : 0;
                    char index1 = (char)(index);
                    result += index1;
                }
                result += rest;
                return result;
            }

            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "subtract"
            };
            throw new MathError(nodeName, line, a);
        }

        internal object Multiply(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l * r;
            }

            if (left is float lf && right is float rf)
            {
                return lf * rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt * rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat * rInt;
            }

            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "multiply"
            };
            throw new MathError(nodeName, line, a);

        }

        internal object Divide(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l / r;
            }

            if (left is float lf && right is float rf)
            {
                return lf / rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt / rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat / rInt;
            }

            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "divide"
            };
            throw new MathError(nodeName, line, a);

        }

        internal object Remainder(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l % r;
            }

            if (left is float lf && right is float rf)
            {
                return lf % rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt % rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat % rInt;
            }

            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "divide"
            };
            throw new MathError(nodeName, line, a);

        }

        internal bool IsEqual(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l == r;
            }

            if (left is float lf && right is float rf)
            {
                return lf == rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt == rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat == rInt;
            }

            if (left is string sl && right is string sr)
            {
                return sl == sr;
            }

            if (left is bool bl && right is bool rb)
            {
                return bl == rb;
            }

            return left.Equals(right);
            //throw new RoboError(nodeName, line, $"Cannot compare values of types {left?.GetType()} and {right?.GetType()}");
        }

        internal bool MoreThan(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l > r;
            }

            if (left is float lf && right is float rf)
            {
                return lf > rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt > rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat > rInt;
            }

            if (left is string sl && right is string sr)
            {
                return sl.Length > sr.Length;
            }
            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "compare"
            };
            throw new MathError(nodeName, line, a);
        }
        internal bool LessThan(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l < r;
            }

            if (left is float lf && right is float rf)
            {
                return lf < rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt < rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat < rInt;
            }

            if (left is string sl && right is string sr)
            {
                return sl.Length < sr.Length;
            }
            
            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "compare"
            };
            throw new MathError(nodeName, line, a);
        }
        internal bool MoreOrEq(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l <= r;
            }

            if (left is float lf && right is float rf)
            {
                return lf <= rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt <= rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat <= rInt;
            }

            if (left is string sl && right is string sr)
            {
                return sl.Length <= sr.Length;
            }
            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "compare"
            };
            throw new MathError(nodeName, line, a);
        }
        internal bool LessOrEq(object left, object right)
        {
            if (left is int l && right is int r)
            {
                return l <= r;
            }

            if (left is float lf && right is float rf)
            {
                return lf <= rf;
            }

            if (left is int lInt && right is float rFloat)
            {
                return lInt <= rFloat;
            }

            if (left is float lFloat && right is int rInt)
            {
                return lFloat <= rInt;
            }

            if (left is string sl && right is string sr)
            {
                return sl.Length <= sr.Length;
            }
            MathError.Args a = new MathError.Args
            {
                left = left,
                right = right,
                opName = "compare"
            };
            throw new MathError(nodeName, line, a);
        }
    }
}
