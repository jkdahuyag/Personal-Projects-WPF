using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AS3_A;
using Microsoft.VisualBasic.CompilerServices;
using NUnit.Framework;
using FluentAssertions;

namespace AS4_A
{
    public class PostfixEvaluator
    {
        public static double EvaluatePostfixExpression(string postfixExpression)
        {
            var stack = new StackList<double>();
            var split = postfixExpression.Split(' ');
            foreach (var str in split)
            {
                if (InfixToPostfixConverter.IsOperator(str))
                {
                    var result = 0d;
                    if (InfixToPostfixConverter.TrigonometricFunctions.Contains(str))
                    {
                        var x = stack.Pop();
                        result = Calculate(x, str);
                    }
                    else
                    {
                        if (stack.Count < 2) throw new InvalidOperationException("Invalid Postfix Notation");
                        var x = stack.Pop();
                        var y = stack.Pop();
                        result = Calculate(y, x, str);
                    }
                    stack.Push(result);

                }
                else
                {
                    if (str.Length == 0) continue;
                    stack.Push(double.Parse(str));
                }
            }
            return stack.Pop();
        }

        private static double Calculate(double y, double x, string operation)
        {
            return operation switch
            {
                "+" => y + x,
                "-" => y - x,
                "*" => y * x,
                "/" => y / x,
                "%" => y % x,
                "^" => Math.Pow(y, x),
                _ => throw new InvalidOperationException("Operation not recognized")
            };
        }
        private static double Calculate(double x, string trigonometricOperation)
        {
            return trigonometricOperation switch
            {
                "sin" => Math.Sin(x),
                "cos" => Math.Cos(x),
                "tan" => Math.Tan(x),
                "csc" => 1 / Math.Sin(x),
                "sec" => 1 / Math.Cos(x),
                "cot" => 1 / Math.Tan(x),
                _ => throw new InvalidOperationException("Operation not recognized")
            };
        }

    }
}
