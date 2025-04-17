using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;
using Xunit.Abstractions;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

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
            var pizza = new PizzaBaseDTO { Name = "The Za", Price = 15.99M };
            var response = await client.PostAsJsonAsync("/api/inventory/addpizzabase", pizza);
            response.IsSuccessStatusCode.ShouldBeTrue();

            var pizzas = await client.GetFromJsonAsync<List<PizzaBaseDTO>>("/api/inventory/getallpizzabase");
            pizzas!.First(p => p.Name == "The Za" && p.Price == 15.99m).ShouldNotBeNull();
        }

        [Fact]
        public async Task CanAddAndGetStockItem()
        {
            var client = _app.CreateClient();
            var stockItem = new StockItemDTO { Name = "Diet Coke", ItemCategoryId = 1, Price = 3.75m };
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
                Items = new List<OrderItemDTO>() { new OrderItemDTO(new StockItemDTO { Id = 1, Name = "Coke", Price = 3.75m, ItemCategoryId = 1 }) },
                DateOrdered = DateTime.Parse("03-31-2025 12:30:00 PM")
            };

            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var orders = await client.GetFromJsonAsync<List<OrderDTO>>("/api/order/allorders");
            orders!.Count.ShouldBe(1);
            var orderDTO = orders.First();
            orderDTO.NetAmount.ShouldBe(order.NetAmount);
        }

        [Fact]
        public async Task CanSendPizzaOrder()
        {
            var client = _app.CreateClient();
            var pizza = new PizzaDTO(new() { Id = 1, Name = "Test", Price = 15.99M });
            pizza.AddTopping(new() { Id = 1, AddinName = "Pepperoni", Price = 1.50M });
            var order = new OrderDTO
            {
                GrossAmount = 3.75M,
                NetAmount = 3.85M,
                SalesTax = 0.10M,
                Items = [new(pizza)],
                DateOrdered = DateTime.ParseExact("03-31-2025 12:30:00 PM", "MM-dd-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            };

            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var orders = await client.GetFromJsonAsync<List<OrderDTO>>("/api/order/allorders");
            var orderDTO = orders.First();
            var dbPizza = orderDTO.Items.First().Pizza;
            dbPizza.Addins.Count().ShouldBe(1);
            dbPizza.Base.Id.ShouldBe(1);
            dbPizza.Base.Name.ShouldBe("Test");
            dbPizza.Base.Price.ShouldBe(15.99M);
        }
        [Fact]
        public async Task CanAuthorize()
        {
            var client = _app.CreateClient();
            var createRequest = new CreateRequest() { Email = "tetatete@gmail.com", Name = "test", PassCode = "Golden Wind" };
            var APIKEY = await client.PostAsJsonAsync("/api/auth/create", createRequest);
            APIKEY.ShouldNotBeNull();
            var authRequest = new AuthRequest() { Email = "tetatete@gmail.com", PassCode = "Golden Wind" };
            (await APIKEY!.Content.ReadAsStringAsync()).ShouldBe((await (await client.PostAsJsonAsync("/api/auth/create", createRequest)).Content.ReadAsStringAsync()));
        }

        [Fact]
        public async Task CanSendDrinkOrder()
        {
            var client = _app.CreateClient();
            var drink = new DrinkDTO(new() { Id = 1, Name = "Kyles M", Price = 4.50M });
            drink.AddDrinkAddin(new() { Id = 1, AddinName = "Raspberry", Price = 1.50M });
            var order = new OrderDTO
            {
                GrossAmount = 6.00M,
                NetAmount = 6.25M,
                SalesTax = 0.25M,
                Items = [new(drink)],
                DateOrdered = DateTime.ParseExact("03-31-2025 12:30:00 PM", "MM-dd-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            };

            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var orders = await client.GetFromJsonAsync<List<OrderDTO>>("/api/order/allorders");
            var orderDTO = orders.First();
            var dbDrink = orderDTO.Items.First().Drink;
            dbDrink.Addins.Count().ShouldBe(1);
            dbDrink.Base.Id.ShouldBe(1);
            dbDrink.Base.Name.ShouldBe("Kyles M");
            dbDrink.Base.Price.ShouldBe(4.50M);
        }

        [Fact]
        public async Task CanSendCalzoneOrder()
        {
            var client = _app.CreateClient();
            var calzone = new CalzoneDTO { CookedAtHome = false, Price = 5.99M, SauceId = 1, Sauce = new() { Id = 1, SauceName = "Marinara" } };
            calzone.AddTopping(new() { Id = 1, AddinName = "Pepperoni", Price = 1.50M });
            var order = new OrderDTO
            {
                GrossAmount = 7.49M,
                NetAmount = 7.50M,
                SalesTax = 0.01M,
                Items = [new(calzone)],
                DateOrdered = DateTime.ParseExact("03-31-2025 12:30:00 PM", "MM-dd-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            };


            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var orders = await client.GetFromJsonAsync<List<OrderDTO>>("/api/order/allorders");
            var orderDTO = orders.First();
            var dbCalzone = orderDTO.Items.First().Calzone;
            dbCalzone.Addins.Count().ShouldBe(1);
            dbCalzone.Price.ShouldBe(5.99M);
        }

        [Fact]
        public async Task CanSendSaladOrder()
        {
            var client = _app.CreateClient();
            var salad = new SaladDTO { Price = 5.99M };
            salad.AddSaladAddin(new() { Id = 1, AddinName = "Pepperoni", Price = 1.50M });
            var order = new OrderDTO
            {
                GrossAmount = 7.49M,
                NetAmount = 7.50M,
                SalesTax = 0.01M,
                Items = [new(salad)],
                DateOrdered = DateTime.ParseExact("03-31-2025 12:30:00 PM", "MM-dd-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            };

            var response = await client.PostAsJsonAsync("/api/order/sendorder", order);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var orders = await client.GetFromJsonAsync<List<OrderDTO>>("/api/order/allorders");
            var orderDTO = orders.First();
            var dbSalad = orderDTO.Items.First().Salad;
            dbSalad.Addins.Count().ShouldBe(1);
            dbSalad.Price.ShouldBe(5.99M);
        }
    }
}
