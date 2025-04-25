using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;
using Shouldly;
using ZasAndDasMobile.ViewModels;
using NSubstitute;
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
            var pizza = new CheckoutItemVM(new PizzaDTO(new PizzaBaseDTO() { Price = 10.99m }), 1);
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
            cart.GetCartItems.ShouldNotBe(new() { new(new PizzaDTO(new PizzaBaseDTO() { Name = "jeff" }), 1) });
        }

        [Fact]
        public void CanChangePizzaSize()
        {
            var cart = new CartService();
            var pizza = new PizzaDTO(new PizzaBaseDTO()) { Id = 1 };
            pizza.PizzaSize = new PizzaSize() { Id = 0, SizeName = "12\"", Price = 0.00m };
            pizza.ChangeSize(new PizzaSize { SizeName = "16\"", Price = 3.00m });
            pizza.PizzaSize.SizeName.ShouldBe("16\"");
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

        /* 
         Can add comment
         Can change size of pizza
         Can add drinks and pizza
         Can remove drink
         Can remove side
         Delete cart items
        */

        [Fact]
        public void CanAddDrinkToCart()
        {
            var cart = new CartService();
            var drink = new DrinkDTO(new DrinkBaseDTO() { Name = "Kyle Mon" });
            cart.AddToCart(drink);
            cart.GetItemCount.ShouldBe(1);
        }

        [Fact]
        public void CanRemoveDrinkFromCart()
        {
            var cart = new CartService();
            var drink = new DrinkDTO(new DrinkBaseDTO() { Name = "Kyle Mon" });
            cart.AddToCart(drink);
            cart.GetItemCount.ShouldBe(1);
            cart.RemoveItem(1);
            cart.GetItemCount.ShouldBe(0);
        }

        [Fact]
        public void CanAddCalzoneToCart()
        {
            var cart = new CartService();
            var calzone = new CalzoneDTO { CookedAtHome = true, Price = 5.99m };
            cart.AddToCart(calzone);
            cart.GetItemCount.ShouldBe(1);
        }

        [Fact]
        public void CanRemoveCalzoneFromCart()
        {
            var cart = new CartService();
            var calzone = new CalzoneDTO { CookedAtHome = true, Price = 5.99m };
            cart.AddToCart(calzone);
            cart.GetItemCount.ShouldBe(1);
            cart.RemoveItem(1);
            cart.GetItemCount.ShouldBe(0);
        }

        [Fact]
        public void CanAddAndRemoveWithMultipleItems()
        {
            var cart = new CartService();

            var calzone = new CalzoneDTO { CookedAtHome = true, Price = 5.99m };
            cart.AddToCart(calzone);
            cart.GetItemCount.ShouldBe(1);

            var drink = new DrinkDTO(new DrinkBaseDTO() { Name = "Kyle Mon" });
            cart.AddToCart(drink);
            cart.GetItemCount.ShouldBe(2);

            cart.RemoveItem(1);
            cart.GetItemCount.ShouldBe(1);
            cart.GetCartItems.ShouldContain(i => i.Id == 2);
        }

        [Fact]
        public void CannotRemoveNonexistentItem()
        {
            var cart = new CartService();
            Should.Throw<InvalidOperationException>(() => cart.RemoveItem(1));
        }

        [Fact]
        public void TotalCalculatedCorrectlyWithTip()
        {
            var cart = Substitute.For<CartService>();
            var cartVM = new CartViewModel(cart);

            var calzone = new CalzoneDTO { CookedAtHome = true, Price = 6.00m };
            cart.AddToCart(calzone);

            cartVM.SetTipAmountCommand.Execute("0.20m");

            cart.TipAmount.ShouldBe(6.00m * 0.20m);

            var drink = new DrinkDTO(new DrinkBaseDTO() { Name = "Kyle Mon", Price = 6.00m });
            cart.AddToCart(drink);

            cart.TipAmount.ShouldBe(12.00m * 0.20m);
            cart.RemoveItem(1);

            cart.TipAmount.ShouldBe(6.00m * 0.20m);
        }
    }
}