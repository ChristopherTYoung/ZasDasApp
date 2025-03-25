using ZasAndDasMobile.Models;
using ZasAndDasMobile.ViewModels;
using ZasAndDasMobile.Services;
using ZasUndDas.Shared;
using Shouldly;
namespace ZasAndDasMobile.Tests

{
    public class MainPageTests
    {
        [Fact]
        public void Pizza()
        {
            var Pizzas = new MenuItemService();
        }
        [Fact]
        public void PizzaReturn()
        {
            var Pizzas = new MenuItemService();
            Pizzas.GetAllPizzas().ShouldBe(new List<Pizza>());
        }
        [Theory]
        [InlineData("Jeff")]
        public void PizzaReturnSomething(string pizza)
        {
            var Pizzas = new MenuItemService();
            Pizza pipza = new Pizza() { Name = pizza };
            Pizzas.AddItemToMenu(pipza);
            Pizzas.GetAllPizzas().ShouldBe(new List<Pizza>() { pipza });
        }
        [Fact]
        public void PizzaReturnsListCopy()
        {
            var Pizzas = new MenuItemService();
            Pizza pipza = new Pizza() { Name = "pizza" };
            Pizzas.AddItemToMenu(pipza);
            var list = Pizzas.GetAllPizzas();
            list.Add(new Pizza());
            Pizzas.GetAllPizzas().ShouldBe(new List<Pizza>() { pipza });

        }

        [Fact]
        public void DrinksReturnEmptyList()
        {
            var drinks = new MenuItemService();
            drinks.GetAllDrinks().ShouldBeEmpty();
        }

        [Fact]
        public void AddDrinkToList()
        {
            var drinks = new MenuItemService();
            Drink drink = new Drink() { Name = "Kyle's Monseter" };
            drinks.AddItemToMenu(drink);
            drinks.GetAllDrinks().Count.ShouldBe(1);
        }
        [Fact]
        public void EmptyCart()
        {
            var cart = new CartService();
            cart.GetCartItems().ShouldBe(new List<IStoreItem>());

        }
        [Fact]
        public void AddToCart()
        {
            var cart = new CartService();
            cart.AddToCart(new Pizza());
            cart.GetCartItems().ShouldBe(new() { new Pizza() });
        }
        [Fact]
        public void AddToCartDoesEquivalent()
        {
            var cart = new CartService();
            cart.AddToCart(new Pizza());
            cart.GetCartItems().ShouldNotBe(new() { new Pizza() { Name="jeff"} });
        }

        [Fact]
        public void RemoveFromCart()
        {
            var cart = new CartService();
            cart.AddToCart(new Pizza() { Id = 1 });
            cart.GetCartItems().Count.ShouldBe(1);
            cart.RemoveItem(1);
            cart.GetCartItems().Count.ShouldBe(0);
        }

        [Fact]
        public void CalculatePrice_WithEmptyCart()
        {
            var cart = new CartService();
            cart.CalculateTotal().ShouldBe(0);
        }

        [Fact]
        public void CalculatePrice_WithOneItem()
        {
            var cart = new CartService();
            cart.AddToCart(new Pizza { Id = 1, Price = 4.99 });
            cart.CalculateTotal().ShouldBe(4.99);
        }

        [Fact]
        public void CalculatePrice_WithTwoItems()
        {
            var cart = new CartService();
            cart.AddToCart(new Pizza { Id = 1, Price = 5 });
            cart.AddToCart(new Pizza { Id = 1, Price = 6 });
            cart.CalculateTotal().ShouldBe(11);
        }
    }
}

