using NUnit.Framework;

namespace AS4_A;

public class Tests
{
    [Test]
    public void VisualConfirmationTest()
    {
        var postfixExpression1 = InfixToPostfixConverter.ConvertToPostfix("7 + (2)cos(sin(30 - 10 * 2) * 20 + 1)");
        Console.WriteLine(postfixExpression1);
        var result1 = PostfixEvaluator.EvaluatePostfixExpression(postfixExpression1);
        Console.WriteLine(result1);


        var postfixExpression = InfixToPostfixConverter.ConvertToPostfix("((6.00) (2+2)) + {3 +1- 2 +2 -1- (2 + 3)4 * 8} / 7 + (2)cos(sin(30 - 10 * 2) * 20 + 1)");
        Console.WriteLine(postfixExpression);
        var result = PostfixEvaluator.EvaluatePostfixExpression(postfixExpression);
        Console.WriteLine(result);

        var postfixExpression2 = InfixToPostfixConverter.ConvertToPostfix("sin(5^2 + 10)^2 * 10 * cos 100^(1/2) * 10 ");
        Console.WriteLine(postfixExpression2);
        var result2 = PostfixEvaluator.EvaluatePostfixExpression(postfixExpression2);
        Console.WriteLine(result2);

    }
}