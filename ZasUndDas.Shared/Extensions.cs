using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZasUndDas.Shared.Data;

namespace ZasUndDas.Shared
{
    public static class Extensions
    {
        public static async Task<Pizza> ToPizza(this PizzaDTO pizzaDTO, PostgresContext context)
        {
            List<PizzaAddin> addins = new List<PizzaAddin>();
            Pizza pizza = new Pizza();
            foreach (var ingredient in pizzaDTO.Ingredients)
            {
                pizza.PizzaAddins.Add(new PizzaAddin() { _Pizza = pizza, Addin = context.PAddins.First(p => p.Id == ingredient) });
            }
            pizza.BaseId = context.PizzaBases.FirstOrDefault(p => p.PizzaName == pizzaDTO.Name)?.Id ?? throw new InvalidCastException();
            return pizza;
        }

        public static OrderDTO ToOrderDTO(this PizzaOrder order, PostgresContext context)
        {
            OrderDTO orderDTO = new OrderDTO()
            {
                DateOrdered = order.DateOrdered,
                GrossAmt = order.GrossAmount,
                NetAmt = order.NetAmount,
                SalesTax = order.SalesTax,
                Items = order.OrderItems.Where(i => i.OrderId == order.Id).Select(i => i.ToItemDTO(context)).ToList()
            };

            return new OrderDTO();
        }

        public static OrderItemDTO ToItemDTO(this OrderItem item, PostgresContext context)
        {
            if (item.StockItem == null)
                throw new Exception("not a stock item");
            return new OrderItemDTO(new StockItemDTO(item.StockItem));
        }
    }
}
