using System.Text;
using AS3_A;
using AS4_A;

namespace AS4_B
{
    public class BooleanInfixToPostfixConverter:InfixToPostfixConverter
    {
        public static Dictionary<string, int> BooleanOperators = new Dictionary<string, int>()
        {
            {"&&", 1},
            {"||", 1},
            {"==", 2},
            {"!=", 2},
            {"<=", 3},
            {"<", 3},
            {">", 3},
            {">=", 3},
            {"!", 5},
        };

        public static string ConvertBooleanToPostfix(string infixNotation)
        {
            var stack = new StackList<string>();
            var sb = new StringBuilder();
            var numberString = new StringBuilder();
            var operatorString = new StringBuilder();
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
                        stack.Pop();
                    else
                        throw new InvalidOperationException("Delimiters do not match");
                }
                //Does operation logic
                else if (!NumbersAndDividingSymbols.Contains(character) && character != ' ')
                {
                    operatorString.Append(character);

                    if((operatorString.ToString() == ">" 
                        || operatorString.ToString() == "<")
                       && infixNotation[index + 1] == '=')
                        continue;
                    if (IsBooleanOperator(operatorString.ToString()))
                    {
                        DoOperation(stack, operatorString.ToString(), sb);
                        operatorString.Clear();
                    }
                }
            }
            //add the remaining number in (if any)
            sb.Append($"{numberString} ");
            foreach (var remaining in stack)
            {
                if (Delimiters.ContainsKey(remaining[0])) throw new InvalidOperationException("Delimiters do not match");
                sb.Append($"{remaining} ");
            }
            return sb.ToString();
        }
        private static void DoOperation(IStack<string> stack, string characterString, StringBuilder sb)
        {
            if (stack.Count == 0) stack.Push(characterString);
            else
            {
                while (!BooleanPrecedence(stack.Peek(), characterString)
                       && !(Delimiters.ContainsKey(stack.Peek()[0]) || Delimiters.ContainsValue(stack.Peek()[0])))
                {
                    sb.Append($"{stack.Pop()} ");
                    if (stack.Count == 0) break;
                }

                stack.Push(characterString);
            }
        }

        public static bool IsBooleanOperator(string s)
        {
            return BooleanOperators.ContainsKey(s);
        }

        public static bool BooleanPrecedence(string operator1, string operator2)
        {
            BooleanOperators.TryGetValue(operator1, out var operator1Precedence);
            BooleanOperators.TryGetValue(operator2, out var operator2Precedence);
            return operator1Precedence < operator2Precedence;
        }
    }
}