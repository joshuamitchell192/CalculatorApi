using Microsoft.EntityFrameworkCore;

namespace Api.Calculations;

public interface ICalculationsService
{
    Task<bool> AddCalculation(Calculation calculation);
    Task<List<Calculation>> GetAllCalculations();
}

public class CalculationService(AppDbContext db) : ICalculationsService
{
    public async Task<bool> AddCalculation(Calculation calculation)
    {
        await db.AddAsync(calculation);
        int entitiesSaved = await db.SaveChangesAsync();

        return entitiesSaved > 0;
    }

    public async Task<List<Calculation>> GetAllCalculations()
    {
        return await db.Calculations.ToListAsync();
    }
}