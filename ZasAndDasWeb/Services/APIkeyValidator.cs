using ZasUndDas.Shared.Data;

namespace ZasAndDasWeb.Services;

public class APIKeyValidationService : IAPIKeyValidationService
{
    private readonly PostgresContext _context;
    public APIKeyValidationService(PostgresContext context)
    {
        _context = context;
    }
    public bool IsValidAPIKey(string? key)
    {
        return _context.Customers.Select(u => u.ApiKey).Contains(key);
    }
}