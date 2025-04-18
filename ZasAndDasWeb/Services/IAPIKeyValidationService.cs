using Square.Bookings.CustomAttributeDefinitions;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasWeb.Services
{
    public interface IAPIKeyValidationService
    {
        public Task<CustomerDTO> GetCustomer(string? key);
        Task<string> CreateAccount(CreateRequest request);
        string Authenticate(AuthRequest request);
    }
}