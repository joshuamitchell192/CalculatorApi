namespace Api.Calculations;

public class CalculationDto
{
    public string Id { get; set; }
    public string Operation { get; set; }
    public List<double> Operands { get; set; }
    public double Result { get; set; }
    public string CreatedAt { get; set; }

    public CalculationDto(Calculation calculation)
    {
        Id = calculation.Id.ToString();
        Operation = calculation.Operation;
        Operands = calculation.Operands;
        Result = calculation.Result;
        CreatedAt = calculation.CreatedAt.ToString();
    }
}