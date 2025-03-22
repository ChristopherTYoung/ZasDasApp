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
            var Pizzas = new PizzaService();
        }
        [Fact]
        public void PizzaReturn()
        {
            var Pizzas = new PizzaService();
            Pizzas.GetAllPizzas().ShouldBe(new List<Pizza>());
        }
        [Theory]
        [InlineData("Jeff")]
        public void PizzaReturnSomething(string pizza)
        {
            var Pizzas = new PizzaService();
            Pizza pipza = new Pizza() { Name = pizza };
            Pizzas.AddPizza(pipza);
            Pizzas.GetAllPizzas().ShouldBe(new List<Pizza>() { pipza });
        }
        [Fact]
        public void PizzaReturnsListCopy()
        {
            var Pizzas = new PizzaService();
            Pizza pipza = new Pizza() { Name = "pizza" };
            Pizzas.AddPizza(pipza);
            var list = Pizzas.GetAllPizzas();
            list.Add(new());
            Pizzas.GetAllPizzas().ShouldBe(new List<Pizza>() { pipza });

        }
    }
}

