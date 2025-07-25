namespace Api.Calculations;

public interface ICalculationsService
{
    Task<bool> AddCalculation(Calculation calculation);
}

public class CalculationService : ICalculationsService
{

    private readonly AppDbContext _db;

    public CalculationService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<bool> AddCalculation(Calculation calculation)
    {
        await _db.AddAsync(calculation);
        int entitiesSaved = await _db.SaveChangesAsync();

        return entitiesSaved > 0;
    }
}