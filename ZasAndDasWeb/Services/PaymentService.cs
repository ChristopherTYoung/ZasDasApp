using Azure.Core;
using Square;
using Square.Payments;
namespace ZasAndDasWeb.Services
{
    public class PaymentService(SquareClient client)
    {
        public async Task<CreatePaymentResponse> SendPayment(string nonce, decimal amount)
        {
            var response = await client.Payments.CreateAsync(
                new CreatePaymentRequest
                {
                    AmountMoney = new Money
                    {
                        Amount = (long)amount * 100,
                        Currency = Currency.Usd
                    },
                    IdempotencyKey = Guid.NewGuid().ToString(),
                    SourceId = nonce
                }
            );
            return response;
        }
    }
}
