namespace Api.Calculations;

using Microsoft.EntityFrameworkCore;
using Extensions;

public interface ICalculationsService
{
    Task<Calculation?> AddCalculation(string operation, List<double> operands, double result);
    Task<bool> UpdateCalculation(Calculation calculation);
    Task<List<Calculation>> GetAllCalculations();
    Task<Calculation?> GetCalculation(Guid id);
    Task<bool> DeleteCalculation(Guid id);
}

public class CalculationService(AppDbContext db) : ICalculationsService
{
    /// <summary>
    /// Adds the new calculation entity to the database.
    /// </summary>
    /// <param name="operation">The operation string.</param>
    /// <param name="operands">List double values for the operands.</param>
    /// <param name="result">The result of the calculation operation tho the operands</param>
    /// <returns>The calculation entity that was saved the database, otherwise null</returns>
    public async Task<Calculation?> AddCalculation(string operation, List<double> operands, double result)
    {
        var calculation = new Calculation
        {
            Id = Guid.NewGuid(),
            Operation = operation.ToLower(),
            Operands = operands,
            Result = result,
            CreatedAt = NodaTime.SystemClock.Instance.GetCurrentInstant()
        };

        await db.AddAsync(calculation);
        int entitiesSaved = await db.SaveChangesAsync();

        return entitiesSaved > 0 ? calculation : null;
    }

    /// <summary>
    /// Updates the calculation operands, operation and result with the associated id.
    /// </summary>
    /// <param name="calculation">The calculation entity to be updated.</param>
    /// <returns>True if the entity was successfully updated, other false.</returns>
    public async Task<bool> UpdateCalculation(Calculation calculation)
    {
        db.Calculations.Update(calculation);
        int entitiesSaved = await db.SaveChangesAsync();
        return entitiesSaved > 0;
    }

    /// <summary>
    /// Retrieves all calculation entities
    /// </summary>
    /// <returns>List of all calculation entities.</returns>
    public async Task<List<Calculation>> GetAllCalculations()
    {
        return await db.Calculations.ToListAsync();
    }

    /// <summary>
    /// Retrieves the calculation entity from the database.
    /// </summary>
    /// <param name="id">The id of the calculation entity.</param>
    /// <returns>The calculation entity with the associated id</returns>
    public async Task<Calculation?> GetCalculation(Guid id)
    {
        return await db.Calculations.FindAsync(id);
    }

    /// <summary>
    /// Deletes the calculation entity.
    /// </summary>
    /// <param name="id">The id of the calculation entity.</param>
    /// <returns>True if the entity was successfully deleted, otherwise false.</returns>
    /// <exception cref="EntityNotFoundException">
    ///     Throws an EntityNotFoundException if the entity does not exist in the database.
    /// </exception>
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