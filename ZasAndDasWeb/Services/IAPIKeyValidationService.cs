using Square.Bookings.CustomAttributeDefinitions;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasWeb.Services
{
    public interface IAPIKeyValidationService
    {
        bool IsValidAPIKey(string? key);
        string CreateAccount(CreateRequest request);
        string Authenticate(AuthRequest request);
    }
}