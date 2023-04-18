using System.Globalization;
using System.Text;
using AS3_A;
using Dahuyag_Assessment_1;

namespace AS4_A
{
    public class InfixToPostfixConverter
    {
        public static string[] TrigonometricFunctions = { "sin", "cos", "tan", "csc", "sec", "cot" };
        public static Dictionary<string, int> OperatorsOrderedByPrecedence = new Dictionary<string, int>()
        {
            {"+", 4},
            {"-", 4},
            {"*", 7},
            {"/", 7},
            {"%", 7},
            {"sin", 8},
            {"cos", 8},
            {"tan", 8},
            {"csc", 8},
            {"sec", 8},
            {"cot", 8},
            {"^", 10}
        };
        public static string NumbersAndDividingSymbols = "0123456789.,";
        public static Dictionary<char, char> Delimiters = new Dictionary<char, char>()
        {
            {'(',')'},
            {'[',']'},
            {'{','}'}
        };
        public static string ConvertToPostfix(string infixNotation)
        {
            var stack = new StackList<string>();
            var sb = new StringBuilder();
            var numberString = new StringBuilder();
            var operatorString = new StringBuilder();
            var recentlyClosedDelimiter = false;
            for (var index = 0; index < infixNotation.Length; index++)
            {
                var character = infixNotation[index];
                //Multi-digit logic
                if (NumbersAndDividingSymbols.Contains(character)) numberString.Append($"{character}");
                else
                {
                    if (numberString.Length == 0) { }
                    else
                    {
                        sb.Append($"{numberString} ");
                        numberString.Clear();
                    }
                }
                //Checks for opening delimiter
                if (Delimiters.ContainsKey(character)) stack.Push(character.ToString());
                //Checks for closing delimiter
                else if (Delimiters.ContainsValue(character))
                {
                    char delimiter;
                    while (!Delimiters.TryGetValue(stack.Peek()[0], out delimiter))
                    {
                        sb.Append($"{stack.Pop()} ");
                        if (stack.Count == 0) break;
                    }

                    if (delimiter == character)
                    {
                        stack.Pop();
                        recentlyClosedDelimiter = true;
                    }
                    else
                        throw new InvalidOperationException("Delimiters do not match");
                }               
                //Does operation logic
                else if (!NumbersAndDividingSymbols.Contains(character) && character != ' ')
                {
                    operatorString.Append(character);

                    
                    if (IsOperator(operatorString.ToString()))
                    {
                        DoOperation(stack, operatorString.ToString(), sb);
                        operatorString.Clear();
                    }
                }

                //Application of implied multiplication
                if (recentlyClosedDelimiter 
                    && index != infixNotation.Length - 1
                    && infixNotation[index + 1] != ' ')
                {
                    var impliedOperator = "*";
                    if (!IsOperator(infixNotation[index + 1].ToString()) && !Delimiters.ContainsValue(infixNotation[index + 1]))
                        DoOperation(stack, impliedOperator, sb);

                    recentlyClosedDelimiter = false;
                }
            }
            //add the remaining number in (if any)
            sb.Append($"{numberString} ");
            foreach (var remaining in stack)
            {
                if (Delimiters.ContainsKey(remaining[0])) throw new InvalidOperationException("Delimiters do not match");
                sb.Append($"{remaining} " );
            }
            return sb.ToString();
        }

        private static void DoOperation(IStack<string> stack, string characterString, StringBuilder sb)
        {
            if (stack.Count == 0) stack.Push(characterString);
            else
            {
                while (!Precedence(stack.Peek(), characterString)
                       && !(Delimiters.ContainsKey(stack.Peek()[0]) || Delimiters.ContainsValue(stack.Peek()[0])))
                {
                    sb.Append($"{stack.Pop()} ");
                    if (stack.Count == 0) break;
                }

                stack.Push(characterString);
            }
        }

        public static bool IsOperator(string s)
        {
            return OperatorsOrderedByPrecedence.ContainsKey(s);
        }

        public static bool Precedence(string operator1, string operator2)
        {
            OperatorsOrderedByPrecedence.TryGetValue(operator1, out var operator1Precedence);
            OperatorsOrderedByPrecedence.TryGetValue(operator2, out var operator2Precedence);
            return operator1Precedence < operator2Precedence;
        }
    }
}