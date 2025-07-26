namespace ApiTests;

using Api;

public class CalculatorTests
{
    [Theory]
    [InlineData(new[] { 1.0, 2.0 }, 3.0)]
    [InlineData(new[] { -1.0, -2.0 }, -3.0)]
    [InlineData(new[] { 0.0, 0.0 }, 0.0)]
    public void TestAdd(double[] operands, double expected)
    {
        Assert.Equal(expected, Calculator.Add(operands.ToList()));
    }

    [Theory]
    [InlineData(new[] { 1.0, 2.0 }, -1.0)]
    [InlineData(new[] { -1.0, -2.0 }, 1.0)]
    [InlineData(new[] { 0.0, 0.0 }, 0.0)]
    public void TestSubtract(double[] operands, double expected)
    {
        Assert.Equal(expected, Calculator.Subtract(operands.ToList()));
    }

    [Theory]
    [InlineData(new[] { 1.0, 2.0 }, 2.0)]
    [InlineData(new[] { -1.0, -2.0 }, 2.0)]
    [InlineData(new[] { 0.0, 0.0 }, 0.0)]
    public void TestMultiply(double[] operands, double expected)
    {
        Assert.Equal(expected, Calculator.Multiply(operands.ToList()));
    }

    [Theory]
    [InlineData(new[] { 1.0, 2.0 }, 0.5)]
    [InlineData(new[] { -1.0, -2.0 }, 0.5)]
    public void TestDivide(double[] operands, double expected)
    {
        Assert.Equal(expected, Calculator.Divide(operands.ToList()));
    }
}