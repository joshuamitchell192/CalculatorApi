namespace Api;

public class Calculator
{
    public static double Add(List<double> operands)
    {
        return operands.Sum();
    }

    public static double Subtract(List<double> operands)
    {
        return operands.Aggregate((a, b) => a - b);
    }

    public static double Multiply(List<double> operands)
    {
        return operands.Aggregate((a, b) => a * b);
    }

    public static double Divide(List<double> operands)
    {
        return operands.Aggregate((a, b) => a / b);
    }

    public static double Calculate(string operation, List<double> operands)
    {
        var result = operation.ToLower() switch
        {
            "add" => Add(operands),
            "subtract" => Subtract(operands),
            "multiply" => Multiply(operands),
            "divide" => Divide(operands),
            _ => throw new InvalidOperationException("Unexpected operation"),
        };

        return result;
    }
}