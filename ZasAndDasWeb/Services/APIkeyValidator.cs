using Microsoft.EntityFrameworkCore;
using Square;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasWeb.Services;

public class APIKeyValidationService : IAPIKeyValidationService
{
    private readonly PostgresContext _context;
    ILogger<APIKeyValidationService> logger;
    public APIKeyValidationService(PostgresContext context, ILogger<APIKeyValidationService> logger)
    {
        _context = context;
        this.logger = logger;
    }
    public string Authenticate(AuthRequest request)
    {
        var customer = _context.Customers.FirstOrDefault(p => p.Email == request.Email);
        if (customer != null && request.PassCode.GetHashCode().ToString() == customer.HashedPass)
            return customer.ApiKey;
        return "Unable to validate Account";
    }

    public string CreateAccount(CreateRequest request)
    {
        var APIkey = Guid.NewGuid().ToString();
        bool unique = true;
        var customer = new CustomerDTO() { ApiKey = APIkey, CustomerName = request.Name, Email = request.Email, HashedPass = request.PassCode.GetHashCode().ToString() };
        var attempts = 0;
        while (unique || attempts < 3)
        {
            try
            {
                unique = true;
                _context.Customers.Add(customer);
                _context.SaveChangesAsync();
                return APIkey;
            }
            catch (DbUpdateException ex)
            {
                if (ex != null)
                {
                    logger.LogInformation(ex.InnerException?.Message ?? "unknown exception");
                    APIkey = new Guid().ToString();
                    customer.ApiKey = APIkey;
                    attempts++;
                    if (attempts > 3)
                        return "failed to create account";
                    unique = false;
                }
            }
        }
        return "unknown error";
    }

    public CustomerDTO GetCustomer(string? key)
    {
        return _context.Customers.First(u => u.ApiKey == key);
    }

}