namespace Api.Calculations;

using NodaTime;

public class Calculation
{
    public Guid Id { get; set; }
    public string Operation { get; set; } = string.Empty;
    public List<double> Operands { get; set; } = [];
    public double Result { get; set; }
    public Instant CreatedAt { get; set; }
}