namespace Api.RouteModules;

using Carter;
using Handlers;

public class CalculationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder routes)
    {
        routes.MapPost("/calculations", CalculationHandlers.HandleAddCalculation);
        routes.MapPut("/calculations/{id:guid}", CalculationHandlers.HandleUpdateCalculation);
        routes.MapGet("/calculations", CalculationHandlers.HandleGetAllCalculations);
        routes.MapGet("/calculations/{id:guid}", CalculationHandlers.HandleGetCalculation);
        routes.MapDelete("/calculations/{id:guid}", CalculationHandlers.HandleDeleteCalculation);
    }
}