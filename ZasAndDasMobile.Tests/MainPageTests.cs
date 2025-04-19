using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;
using Shouldly;
namespace ZasAndDasMobile.Tests

{
    public class MainPageTests
    {
        [Fact]
        public void Pizza()
        {
            var Pizzas = new MenuItemService(new MockAPIService());
        }
        [Fact]
        public async Task PizzaReturn()
        {
            var api = new MockAPIService();
            var Pizzas = new MenuItemService(api);
            (await Pizzas.GetAllPizzas()).ShouldBeEquivalentTo(await api.GetPizzas());
        }

        [Fact]
        public async Task DrinksReturnEmptyList()
        {
            var Pizzas = new MenuItemService(new MockAPIService());

            (await Pizzas.GetAllDrinks()).Count().ShouldBe(3);
        }


        [Fact]
        public void EmptyCart()
        {
            var cart = new CartService();
            cart.GetCartItems.ShouldBe(new List<CheckoutItemVM>());

        }
        [Fact]
        public void AddToCart()
        {
            var cart = new CartService();
            var pizza = new CheckoutItemVM(new PizzaDTO(new PizzaBaseDTO() { Price = 10.99m }));
            ((PizzaDTO)pizza.item).PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 3.00m };
            cart.AddToCart(pizza.item);
            cart.GetCartItems.FirstOrDefault(p => p.item == pizza.item).ShouldNotBeNull();
        }
        // ? 
        [Fact]
        public void AddToCartDoesEquivalent()
        {
            var cart = new CartService();
            var pizza = new PizzaDTO(new PizzaBaseDTO());
            pizza.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 3.00m };
            cart.AddToCart(pizza);
            cart.GetCartItems.ShouldNotBe(new() { new(new PizzaDTO(new PizzaBaseDTO() { Name = "jeff" })) });
        }

        [Fact]
        public void RemoveFromCart()
        {
            var cart = new CartService();
            var pizza = new PizzaDTO(new PizzaBaseDTO()) { Id = 1 };
            pizza.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 3.00m };
            cart.AddToCart(pizza);
            cart.GetCartItems.Count.ShouldBe(1);
            cart.RemoveItem(1);
            cart.GetCartItems.Count.ShouldBe(0);
        }

        [Fact]
        public void CalculatePrice_WithEmptyCart()
        {
            var cart = new CartService();
            cart.CalculateSubTotal().ShouldBe(0);
        }

        [Fact]
        public void CalculatePrice_WithOneItem()
        {
            var cart = new CartService();
            var pizza = new PizzaDTO(new PizzaBaseDTO() { Price = 4.99m });
            pizza.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 0.00m };
            cart.AddToCart(pizza);
            cart.CalculateSubTotal().ShouldBe(4.99m);
        }

        [Theory]
        [InlineData(5, 6, 11)]
        [InlineData(5.001, 6, 11)]
        [InlineData(5.006, 6, 11.01)]
        public void CalculatePrice_WithTwoItems(decimal price1, decimal price2, decimal result)
        {
            var cart = new CartService();
            var pizza1 = new PizzaDTO(new PizzaBaseDTO() { Price = price1 });
            var pizza2 = new PizzaDTO(new PizzaBaseDTO() { Price = price2 });
            pizza1.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 0.00m };
            pizza2.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 0.00m };
            cart.AddToCart(pizza1);
            cart.AddToCart(pizza2);
            cart.CalculateSubTotal().ShouldBe(result);
        }

        [Fact]
        public void PriceCannotBeNegative()
        {
            var cart = new CartService();
            var pizza = new PizzaDTO(new PizzaBaseDTO() { Price = 5 });
            var errorPizza = new PizzaDTO(new PizzaBaseDTO() { Price = -6 });
            pizza.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 0.00m };
            errorPizza.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 0.00m };
            cart.AddToCart(pizza);
            Should.Throw<InvalidOperationException>(() => cart.AddToCart(errorPizza));
        }


    }
}