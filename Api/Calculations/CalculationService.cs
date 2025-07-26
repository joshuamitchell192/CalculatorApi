namespace Api.Calculations;

using Microsoft.EntityFrameworkCore;
using Extensions;

public interface ICalculationsService
{
    Task<bool> AddCalculation(Calculation calculation);
    Task<bool> UpdateCalculation(Calculation calculation);
    Task<List<Calculation>> GetAllCalculations();
    Task<Calculation?> GetCalculation(Guid id);
    Task<bool> DeleteCalculation(Guid id);
}

public class CalculationService(AppDbContext db) : ICalculationsService
{
    public async Task<bool> AddCalculation(Calculation calculation)
    {
        await db.AddAsync(calculation);
        int entitiesSaved = await db.SaveChangesAsync();

        return entitiesSaved > 0;
    }

    public async Task<bool> UpdateCalculation(Calculation calculation)
    {
        db.Calculations.Update(calculation);
        int entitiesSaved = await db.SaveChangesAsync();
        return entitiesSaved > 0;
    }

    public async Task<List<Calculation>> GetAllCalculations()
    {
        return await db.Calculations.ToListAsync();
    }

    public async Task<Calculation?> GetCalculation(Guid id)
    {
        return await db.Calculations.FindAsync(id);
    }

    public async Task<bool> DeleteCalculation(Guid id)
    {
        var calculation = await db.Calculations.FindAsync(id);
        if (calculation == null)
        {
            throw new EntityNotFoundException($"Calculation not found. Id: {id}");
        }

        db.Calculations.Remove(calculation);

        int entitiesRemoved = await db.SaveChangesAsync();
        return entitiesRemoved > 0;
    }
}