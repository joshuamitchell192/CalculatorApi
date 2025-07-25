namespace Api
{
    public interface ICalculator
    {
        public double Add(List<double> operands);
        public double Subtract(List<double> operands);
        public double Multiply(List<double> operands);
        public double Divide(List<double> operands);
    }

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
    }
}
