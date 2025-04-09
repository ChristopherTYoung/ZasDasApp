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
            var Pizzas = new MenuItemService();
        }
        [Fact]
        public void PizzaReturn()
        {
            var Pizzas = new MenuItemService();
            Pizzas.GetAllPizzas().ShouldBe(new List<PizzaBaseDTO>());
        }
        [Theory]
        [InlineData("Jeff")]
        public void PizzaReturnSomething(string pizza)
        {
            var Pizzas = new MenuItemService();
            PizzaBaseDTO pipza = new PizzaBaseDTO() { Name = pizza };
            Pizzas.AddItemToMenu(pipza);
            Pizzas.GetAllPizzas().ShouldBe(new List<PizzaBaseDTO>() { pipza });
        }
        [Fact]
        public void PizzaReturnsListCopy()
        {
            var Pizzas = new MenuItemService();
            PizzaBaseDTO pipza = new PizzaBaseDTO() { Name = "pizza" };
            Pizzas.AddItemToMenu(pipza);
            var list = Pizzas.GetAllPizzas();
            list.Add(new PizzaBaseDTO());
            Pizzas.GetAllPizzas().ShouldBe(new List<PizzaBaseDTO>() { pipza });

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
            DrinkDTO drink = new DrinkDTO() { Name = "Kyle's Monseter" };
            drinks.AddItemToMenu(drink);
            drinks.GetAllDrinks().Count.ShouldBe(1);
        }
        [Fact]
        public void EmptyCart()
        {
            var cart = new CartService();
            cart.GetCartItems().ShouldBe(new List<ICheckoutItem>());

        }
        [Fact]
        public void AddToCart()
        {
            var cart = new CartService();
            var pizza = new PizzaDTO(new PizzaBaseDTO() { Price = 10.99 });
            cart.AddToCart(pizza);
            cart.GetCartItems().ShouldContain(pizza);
        }
        // ? 
        [Fact]
        public void AddToCartDoesEquivalent()
        {
            var cart = new CartService();
            cart.AddToCart(new PizzaDTO(new PizzaBaseDTO()));
            cart.GetCartItems().ShouldNotBe(new() { new PizzaDTO(new PizzaBaseDTO() { Name = "jeff" }) });
        }

        [Fact]
        public void RemoveFromCart()
        {
            var cart = new CartService();
            cart.AddToCart(new PizzaDTO(new PizzaBaseDTO()) { Id = 1 });
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
            cart.AddToCart(new PizzaDTO(new PizzaBaseDTO() { Price = 4.99 }));
            cart.CalculateTotal().ShouldBe(4.99);
        }

        [Theory]
        [InlineData(5, 6, 11)]
        [InlineData(5.001, 6, 11)]
        [InlineData(5.006, 6, 11.01)]
        public void CalculatePrice_WithTwoItems(double price1, double price2, double result)
        {
            var cart = new CartService();
            cart.AddToCart(new PizzaDTO(new PizzaBaseDTO() { Price = price1 }));
            cart.AddToCart(new PizzaDTO(new PizzaBaseDTO() { Price = price2 }));
            cart.CalculateTotal().ShouldBe(result);
        }

        [Fact]
        public void PriceCannotBeNegative()
        {
            var cart = new CartService();
            cart.AddToCart(new PizzaDTO(new PizzaBaseDTO() { Price = 5 }));
            Should.Throw<InvalidOperationException>(() => cart.AddToCart(new PizzaDTO(new PizzaBaseDTO() { Price = -6 })));
        }


    }
}