using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AS3_A;
using AS4_A;

namespace AS4_B
{
    public class BooleanPostfixEvaluator
    {
        public static bool EvaluatePostfixExpression(string postfixExpression)
        {
            var boolStack = new StackList<bool>();
            var numberStack = new StackList<double>();
            var split = postfixExpression.Split(' ');
            foreach (var str in split)
            {
                if(str.Length == 0) continue;
                if (BooleanInfixToPostfixConverter.BooleanOperators.TryGetValue(str, out int value))
                {
                    var result = false;
                    if (value is not (3 or 2))
                    {
                        if (value == 5)
                        {
                            var x = boolStack.Pop();
                            result = Calculate(x, str);
                        }
                        else
                        {
                            if (boolStack.Count < 2) throw new InvalidOperationException("Invalid Postfix Notation");
                            var x = boolStack.Pop();
                            var y = boolStack.Pop();
                            result = Calculate(y, x, str);
                        }
                        boolStack.Push(result);
                    }
                    else
                    {
                        if (numberStack.Count < 2) throw new InvalidOperationException("Invalid Postfix Notation");
                        var x = numberStack.Pop();
                        var y = numberStack.Pop();
                        result = Calculate(y, x, str);
                        boolStack.Push(result);
                    }
                }
                else
                    numberStack.Push(double.Parse(str));
            }
            return boolStack.Pop();
        }

        private static bool Calculate(double y, double x, string operation)
        {
            return operation switch
            {
                ">" => y > x,
                "<" => y < x,
                ">=" => y >= x,
                "<=" => y <= x,
                "==" => Math.Abs(y - x) == 0,
                _ => throw new InvalidOperationException("Operation not recognized")
            };
        }
        private static bool Calculate(bool y, bool x, string operation)
        {
            return operation switch
            {
                "&&" => y && x,
                "||" => y || x,
                _ => throw new InvalidOperationException("Operation not recognized")
            };
        }
        private static bool Calculate(bool x, string trigonometricOperation)
        {
            return trigonometricOperation switch
            {
                "!" => !x,
                _ => throw new InvalidOperationException("Operation not recognized")
            };
        }
    }
}
