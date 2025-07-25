namespace Api
{
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
