using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net.Http.Json;
using Xunit.Abstractions;
using ZasAndDasWeb.Data;

namespace ZasAndDas.IntegrationTests
{
    public class APITests : IntegrationTestBase
    {

        public APITests(WebApplicationFactory<Program> app, ITestOutputHelper outputHelper) : base(app, outputHelper)
        {
        }

        [Fact]
        public async Task CanAddABasePrice()
        {
            var client = _app.CreateClient();
            var price = new PricePerItem { Price = 6.99M };
            await client.PostAsJsonAsync("/api/inventory/addbaseprice", price);

            var prices = await client.GetFromJsonAsync<List<PricePerItem>>("/api/inventory/getallprices");
            prices.ShouldContain(price);
        }

        [Fact]
        public async Task CanAddPizzaBase()
        {
            var client = _app.CreateClient();
            var pizza = new PizzaBase { PizzaName = "The Za", BasePriceId = 1 };
            await client.PostAsJsonAsync("/api/inventory/addpizzabase", pizza);

            var pizzas = await client.GetFromJsonAsync<List<PizzaBase>>("/api/inventory/getallpizza");
            pizzas.ShouldContain(pizza);
        }
    }
}
