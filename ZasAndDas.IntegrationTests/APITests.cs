using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net.Http.Json;
using Xunit.Abstractions;
using ZasUndDas.Shared.Data;

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
            var response = await client.PostAsJsonAsync("/api/inventory/addbaseprice", price);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var prices = await client.GetFromJsonAsync<List<PricePerItem>>("/api/inventory/getallprices");
            prices!.First(p => p.Price == 6.99M).ShouldNotBeNull();
        }

        [Fact]
        public async Task CanAddPizzaBase()
        {
            var client = _app.CreateClient();
            var pizza = new PizzaBaseDTO { PizzaName = "The Za", BasePriceId = 1 };
            var response = await client.PostAsJsonAsync("/api/inventory/addpizzabase", pizza);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var pizzas = await client.GetFromJsonAsync<List<PizzaBase>>("/api/inventory/getallpizzabase");
            pizzas!.First(p => p.PizzaName == "The Za" && p.BasePriceId == 1).ShouldNotBeNull();
        }

        [Fact]
        public async Task CanGetStockItem()
        {
            var client = _app.CreateClient();
            var stockItem = new StockItemDTO { ItemName = "Diet Coke", ItemCategoryId = 1, BasePriceId = 2 };
            var response = await client.PostAsJsonAsync("/api/inventory/addstockitem", stockItem);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var stockItems = await client.GetFromJsonAsync<IEnumerable<StockItem>>("/api/inventory/getallstockitems");
            stockItems!.FirstOrDefault(s => s.ItemName == "Diet Coke").ShouldNotBeNull();
        }
    }
}
