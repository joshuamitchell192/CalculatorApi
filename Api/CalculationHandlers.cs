namespace Api.CalculationsApi;

public class CalculationRequestBody
{
    public string Operation { get; set; } = string.Empty;
    public List<double> Operands { get; set; } = [];
}

public class CalculationHandlers
{
    public static async Task<IResult> HandleAddCalculation(HttpContext ctx, CalculationRequestBody requestBody)
    {
        await Task.Delay(1);
        return Results.InternalServerError(new NotImplementedException());
    }
}