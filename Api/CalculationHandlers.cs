using Api.Calculations;
using Carter.ModelBinding;
using FluentValidation;

namespace Api;

public class CalculationRequestBody
{
    public string Operation { get; init; } = string.Empty;
    public List<double> Operands { get; init; } = [];
}

public class CalculationRequestBodyValidator : AbstractValidator<CalculationRequestBody>
{
    private readonly HashSet<string> _validOperators =
        new(["add", "subtract", "multiply", "divide"], StringComparer.OrdinalIgnoreCase);

    public CalculationRequestBodyValidator()
    {
        RuleFor(request => request.Operands).NotEmpty();
        RuleFor(request => request.Operands).Must(o => o.Count >= 2).WithMessage("Must have at least 2 operands.");
        RuleFor(request => request.Operation).Must(_validOperators.Contains).WithMessage("Invalid Operator.");
        When(request => request.Operation.Equals("divide", StringComparison.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(request => request.Operands).Must(o => !o.Skip(1).Contains(0))
                    .WithMessage("Division by zero is not allowed.");
            });
    }
}

public static class CalculationHandlers
{
    public static async Task<IResult> HandleAddCalculation(HttpContext ctx, CalculationRequestBody requestBody,
        ICalculationsService calculationService)
    {
        // Validate Request
        var validationResult = ctx.Request.Validate(requestBody);
        if (!validationResult.IsValid)
        {
            return Results.UnprocessableEntity(validationResult.GetFormattedErrors());
        }

        // Calculate Result
        var result = requestBody.Operation.ToLower() switch
        {
            "add" => Calculator.Add(requestBody.Operands),
            "subtract" => Calculator.Subtract(requestBody.Operands),
            "multiply" => Calculator.Multiply(requestBody.Operands),
            "divide" => Calculator.Divide(requestBody.Operands),
            _ => throw new InvalidOperationException("Unexpected operation in request body")
        };

        var calculation = new Calculation
        {
            Id = Guid.NewGuid(),
            Operation = requestBody.Operation,
            Operands = requestBody.Operands,
            Result = result,
            CreatedAt = NodaTime.SystemClock.Instance.GetCurrentInstant()
        };

        bool saved = await calculationService.AddCalculation(calculation);

        return saved
            ? Results.Created($"calculations/{calculation.Id}", new { calculation.Result })
            : Results.InternalServerError("Failed to save calculation.");
    }
    
    public static async Task<IResult> HandleGetAllCalculations(HttpContext ctx, ICalculationsService calculationService)
    {
        var calculations = await calculationService.GetAllCalculations();
        var calculationDtoList =
            calculations.Select<Calculation, CalculationDto>(calc => new CalculationDto(calc));

        return Results.Ok(calculationDtoList);
    }

    public static async Task<IResult> HandleGetCalculation(HttpContext ctx, Guid id, ICalculationsService calculationService)
    {
        var calculation = await calculationService.GetCalculation(id);
        if (calculation == null)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(calculation);
    }
}