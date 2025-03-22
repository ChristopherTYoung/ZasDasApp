using Square;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.Services
{
    public class PizzaService
    {
        List<Pizza> pizzas = new List<Pizza>();
        public List<Pizza> GetAllPizzas()
        {
            return pizzas.ToList();
        }
        public PizzaService AddPizza(Pizza pizza)
        {
            pizzas.Add(pizza);
            return this;
        }

        internal static PizzaService TestPizzas()
        {
            return (new PizzaService()).AddPizza(new Pizza() { Name="Pizza1", Description="This is a pizza", Price=4.99 })
                                       .AddPizza(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddPizza(new Pizza() { Name = "Pizza3", Description = "This is not a pizza", Price = 6.99 });
        }
    }
}
