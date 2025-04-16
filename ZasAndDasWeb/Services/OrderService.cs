using ZasUndDas.Shared.Data;
using ZasUndDas.Shared;

namespace ZasAndDasWeb.Services
{
    public class OrderService(PostgresContext context)
    {
        public async Task<OrderItem> AddItemToDatabase(OrderItemDTO itemDTO)
        {
            var item = new OrderItem()
            {
                OrderId = itemDTO.OrderId,
                Quantity = itemDTO.Quantity
            };
            switch (itemDTO.Item)
            {
                case ItemType.Pizza:
                    item.PizzaId = await AddPizzaToDatabase(itemDTO);
                    break;
                case ItemType.Drink:
                    break;
                case ItemType.Calzone:
                    break;
                case ItemType.CheeseBread:
                    break;
                case ItemType.Salad:
                    break;
                case ItemType.Stock:
                    item.StockItemId = itemDTO.StockItem!.Id;
                    break;
                default:
                    break;
            }
            return item;
        }

        private async Task<int> AddPizzaToDatabase(OrderItemDTO itemDTO)
        {
            await context.Pizzas.AddAsync(itemDTO.Pizza!.ToPizza());
            await context.SaveChangesAsync();
            var pizza = context.Pizzas.OrderBy(p => p.Id).Last();
            itemDTO.Pizza.Id = pizza.Id;
            await itemDTO.Pizza.SaveToppingsToDatabase(context);
            return pizza.Id;
        }

        public async Task AddOrderItemToDatabase(OrderItemDTO orderItem)
        {
            var item = await AddItemToDatabase(orderItem);
            await context.OrderItems.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task AddOrderToDatabase(OrderDTO orderDTO)
        {
            var order = new PizzaOrder
            {
                GrossAmount = orderDTO.GrossAmount,
                NetAmount = orderDTO.NetAmount,
                DiscountAmount = orderDTO.DiscountAmount,
                SalesTax = orderDTO.SalesTax,
                DateOrdered = orderDTO.DateOrdered,
            };
            await context.PizzaOrders.AddAsync(order);
            await context.SaveChangesAsync();
            var orderId = context.PizzaOrders.First(o => o == order).Id;
            foreach (var item in orderDTO.Items)
            {
                item.OrderId = orderId;
                await AddOrderItemToDatabase(item);
            }
        }
    }
}
