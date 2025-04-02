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
            var pizza = new PizzaBaseDTO { Name = "The Za", BasePriceId = 1 };
            var response = await client.PostAsJsonAsync("/api/inventory/addpizzabase", pizza);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var pizzas = await client.GetFromJsonAsync<List<PizzaBase>>("/api/inventory/getallpizzabase");
            pizzas!.First(p => p.PizzaName == "The Za" && p.BasePriceId == 1).ShouldNotBeNull();
        }

        [Fact]
        public async Task CanAddAndGetStockItem()
        {
            var client = _app.CreateClient();
            var stockItem = new StockItemDTO { Name = "Diet Coke", ItemCategoryId = 1, BasePriceId = 2 };
            var response = await client.PostAsJsonAsync("/api/inventory/addstockitem", stockItem);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var stockItems = await client.GetFromJsonAsync<IEnumerable<StockItem>>("/api/inventory/getallstockitems");
            stockItems!.FirstOrDefault(s => s.ItemName == "Diet Coke").ShouldNotBeNull();
        }

        [Fact]
        public async Task CanConvertPizzaDTO()
        {
            var options = new DbContextOptionsBuilder<PostgresContext>();
            //.UseInMemoryDatabase(databaseName: "zasanddas");
            options.UseNpgsql(_dbContainer.GetConnectionString());
            var context = new PostgresContext(options.Options);

            var pizzaDTO = new PizzaDTO
            {
                Name = "Test",
                Price = 17.50,
                Size = ItemSize.large,
                Ingredients = new List<int> { 1 }
            };

            var pizza = await pizzaDTO.ToPizza(context);
            pizza.ShouldBeOfType<Pizza>();
            pizza.PizzaAddins.Count.ShouldBe(1);
            pizza.PizzaAddins.First().Addin.AddinName.ShouldBe("pepperoni");
            pizza.BaseId.ShouldBe(1);
        }

        [Fact]
        public async Task CannotSendEmptyOrder()
        {
            var client = _app.CreateClient();
            var order = new OrderDTO
            {
                GrossAmt = 0,
                NetAmt = 0,
                SalesTax = 0,
                Items = new List<OrderItemDTO>(),
                DateOrdered = DateTime.Parse("03-31-2025 12:30:00 PM")
            };
            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CanConvertItemToItemDTO()
        {
            var options = new DbContextOptionsBuilder<PostgresContext>();
            //.UseInMemoryDatabase(databaseName: "zasanddas");
            options.UseNpgsql(_dbContainer.GetConnectionString());
            var context = new PostgresContext(options.Options);

            var item = new OrderItem
            {
                StockItem = new StockItem { ItemName = "Coke", BasePriceId = 3 }
            };

            var itemDTO = item.ToItemDTO(context);
            itemDTO.Item.ShouldBe(ItemType.Stock);
        }

        [Fact]
        public void CanConvertOrderToOrderDTO()
        {
            var options = new DbContextOptionsBuilder<PostgresContext>();
            //.UseInMemoryDatabase(databaseName: "zasanddas");
            options.UseNpgsql(_dbContainer.GetConnectionString());
            var context = new PostgresContext(options.Options);

            var order = new PizzaOrder
            {
            };

            var orderDTO = order.ToOrderDTO(context);
            orderDTO.ShouldBeOfType<OrderDTO>();
        }

        [Fact]
        public async Task CanSendOrder()
        {
            var client = _app.CreateClient();
            var order = new OrderDTO
            {
                GrossAmt = 0,
                NetAmt = 0,
                SalesTax = 0,
                Items = new List<OrderItemDTO>() { new OrderItemDTO(new StockItemDTO { BasePriceId = 3, ItemCategoryId = 1, Name = "Sprite", Description = "" }) },
                DateOrdered = DateTime.Parse("03-31-2025 12:30:00 PM")
            };
            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var orders = await client.GetFromJsonAsync<List<OrderDTO>>("/api/order/allorders");
            orders.ShouldContain(order);
        }
    }
}
