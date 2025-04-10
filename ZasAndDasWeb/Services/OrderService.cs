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
                    await context.Pizzas.AddAsync(itemDTO.Pizza!);
                    await context.SaveChangesAsync();
                    var pizza = context.Pizzas.First(p => p == itemDTO.Pizza);
                    await pizza.SaveToppingsToDatabase(context);
                    item.PizzaId = pizza.Id;
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

        public async Task AddOrderItemToDatabase(OrderItemDTO orderItem)
        {
            var item = await AddItemToDatabase(orderItem);
            await context.OrderItems.AddAsync(item);
        }

        public async Task AddOrderToDatabase(OrderDTO orderDTO)
        {
            await context.PizzaOrders.AddAsync(orderDTO);
            await context.SaveChangesAsync();
            var orderId = context.PizzaOrders.First(o => o == orderDTO).Id;
            foreach (var item in orderDTO.Items)
            {
                item.OrderId = orderId;
                await AddOrderItemToDatabase(item);
            }
        }
    }
}
