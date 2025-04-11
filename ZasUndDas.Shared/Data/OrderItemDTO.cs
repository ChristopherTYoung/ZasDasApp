using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasUndDas.Shared.Data;

public partial class OrderItem
{
    public int Id { set; get; }

    public int OrderId { set; get; }

    public int? Quantity { set; get; }

    public int? StockItemId { set; get; }

    public int? PizzaId { set; get; }

    public int? DrinkId { set; get; }

    public int? CalzoneId { set; get; }

    public int? SaladId { set; get; }

    public int? CheeseBreadId { set; get; }
    public virtual CalzoneDTO? Calzone { set; get; }

    public async Task<OrderItemDTO> ToOrderItemDTO(PostgresContext context)
    {
        var orderItem = new OrderItemDTO
        {
            Id = this.Id,
            OrderId = this.OrderId,
            StockItem = await context.StockItems.FindAsync(StockItemId),
            Pizza = await context.Pizzas.FindAsync(PizzaId)
        };
        return orderItem;
    }
}
public class OrderItemDTO
{
    public int Id { set; get; }
    public int OrderId { get; set; }
    //public DrinkDTO {set; get;}
    public StockItemDTO? StockItem { set; get; }
    public PizzaDTO? Pizza { set; get; }
    public ItemType? Item { set; get; }
    public int Quantity { set; get; }
    public OrderItemDTO()
    {
    }

    public OrderItemDTO(ICheckoutItem item, int quantity = 1)
    {
        switch (item.GetType().Name)
        {
            case nameof(StockItemDTO):
                StockItem = (StockItemDTO)item;
                Item = ItemType.Stock;
                break;
            case nameof(PizzaDTO):
                Pizza = (PizzaDTO)item;
                Item = ItemType.Pizza;
                break;
            default:
                break;
        };
        Quantity = quantity;
    }
}