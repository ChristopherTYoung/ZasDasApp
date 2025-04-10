using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Square;
using Square.Payments;

namespace ZasUndDas.Shared
{
    public class SquareClientService
    {
        public SquareClientService(string devToken)
        {
            client = new SquareClient(devToken);
        }
        SquareClient client;

        public async Task CreateAndProcessPayment(string nonce, long amount, int orderId)
        {
            string SaleKey = Guid.NewGuid().ToString();
            await client.Payments.CreateAsync(new CreatePaymentRequest()
            {
                SourceId = nonce,
                IdempotencyKey = SaleKey,
                AmountMoney =
                new()
                {
                    Amount = amount,
                    Currency = Currency.Usd
                },
                ReferenceId = orderId.ToString()
            });
        }
    }
}
