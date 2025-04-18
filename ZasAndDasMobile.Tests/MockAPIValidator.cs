using System.Collections.Generic;
using System.Linq;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasWeb.Services
{
    public class MockAPIKeyValidationService : IAPIKeyValidationService
    {
        private readonly List<CustomerDTO> customers = new()
        {
            new CustomerDTO
            {
                Id = 1,
                CustomerName = "Alice Johnson",
                Email = "alice@example.com",
                Phone = "123-456-7890",
                ApiKey = "valid-api-key-123",
                HashedPass = "password123"
            },
            new CustomerDTO
            {
                Id = 2,
                CustomerName = "Bob Smith",
                Email = "bob@example.com",
                Phone = "987-654-3210",
                ApiKey = "test-key-456",
                HashedPass = "securepass"
            }
        };

        public CustomerDTO? GetCustomer(string? key)
        {
            if (string.IsNullOrWhiteSpace(key)) return null;
            return customers.FirstOrDefault(c => c.ApiKey == key);
        }

        public string CreateAccount(CreateRequest request)
        {
            return request.Email + "test";
        }

        public string Authenticate(AuthRequest request)
        {
            return request.Email + "test";
        }
    }
}
