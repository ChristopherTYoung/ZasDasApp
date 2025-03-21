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
        public void AddPizza(Pizza pizza)
        {
            pizzas.Add(pizza);
        }
    }
}
