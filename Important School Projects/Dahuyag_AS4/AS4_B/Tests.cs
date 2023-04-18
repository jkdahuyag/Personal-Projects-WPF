using AS4_A;
using AS4_B;
using NUnit.Framework;

public class Tests
{
    [Test]
    public void VisualConfirmationTest()
    {
        var postfixExpression1 = BooleanInfixToPostfixConverter.ConvertBooleanToPostfix("(2==2) || !((4 > 2) && ((1 >= 0) && (3.01 > 40.22)))");
        Console.WriteLine(postfixExpression1);
        var result1 = BooleanPostfixEvaluator.EvaluatePostfixExpression(postfixExpression1);
        Console.WriteLine(result1);


        var postfixExpression = BooleanInfixToPostfixConverter.ConvertBooleanToPostfix("(5 > 8) || (7.01 > 10)");
        Console.WriteLine(postfixExpression);
        var result = BooleanPostfixEvaluator.EvaluatePostfixExpression(postfixExpression);
        Console.WriteLine(result);
    }
}