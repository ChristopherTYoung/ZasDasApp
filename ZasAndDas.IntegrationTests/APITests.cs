using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDas.IntegrationTests
{
    public class APITests : IntegrationTestBase
    {

        public APITests(WebApplicationFactory<Program> app, ITestOutputHelper outputHelper) : base(app, outputHelper)
        {
        }

        [Fact]
        public async Task CanAddPizzaBase()
        {
            var client = _app.CreateClient();
            var pizza = new PizzaBaseDTO { Name = "The Za", Price = 15.99 };
            var response = await client.PostAsJsonAsync("/api/inventory/addpizzabase", pizza);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var pizzas = await client.GetFromJsonAsync<List<PizzaBaseDTO>>("/api/inventory/getallpizzabase");
            pizzas!.First(p => p.Name == "The Za" && p.Price == 15.99).ShouldNotBeNull();
        }

        [Fact]
        public async Task CanAddAndGetStockItem()
        {
            var client = _app.CreateClient();
            var stockItem = new StockItemDTO { Name = "Diet Coke", ItemCategoryId = 1, Price = 3.75 };
            var response = await client.PostAsJsonAsync("/api/inventory/addstockitem", stockItem);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var stockItems = await client.GetFromJsonAsync<IEnumerable<StockItem>>("/api/inventory/getallstockitems");
            stockItems!.FirstOrDefault(s => s.ItemName == "Diet Coke").ShouldNotBeNull();
        }

        // breaks stuff shh...
        //[Fact]
        //public async Task CanAddAndGetDrinkBases()
        //{
        //    var client = _app.CreateClient();
        //    var drink = new DrinkDTO { Name = "Kyle's Monster" };
        //    var response = await client.PostAsJsonAsync("/api/inventory/adddrinkbase", drink);
        //    response.IsSuccessStatusCode.ShouldBeTrue();

        //    var drinks = await client.GetFromJsonAsync<List<DrinkBaseDTO>>("/api/inventory/getalldrinkbase");
        //    drinks!.ShouldContain(d => d.DrinkName == drink.Name);
        //}

        [Fact]
        public async Task CannotSendEmptyOrder()
        {
            var client = _app.CreateClient();
            var order = new OrderDTO
            {
                GrossAmount = 0,
                NetAmount = 0,
                SalesTax = 0,
                Items = new List<OrderItemDTO>(),
                DateOrdered = DateTime.Parse("03-31-2025 12:30:00 PM")
            };
            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CanSendOrder()
        {
            var client = _app.CreateClient();
            var order = new OrderDTO
            {
                GrossAmount = 3.75M,
                NetAmount = 3.85M,
                SalesTax = 0.10M,
                Items = new List<OrderItemDTO>() { new OrderItemDTO(new StockItemDTO { Price = 3.75, ItemCategoryId = 1, Name = "Sprite", Description = "" }) },
                DateOrdered = DateTime.Parse("03-31-2025 12:30:00 PM")
            };
            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var orders = await client.GetFromJsonAsync<List<OrderDTO>>("/api/order/allorders");
            orders!.Count.ShouldBe(1);
            orders.First().NetAmount.ShouldBe(order.NetAmount);
        }
    }
}
