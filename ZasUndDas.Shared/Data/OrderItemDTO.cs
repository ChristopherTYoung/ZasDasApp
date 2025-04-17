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

    public async Task<OrderItemDTO> ToOrderItemDTO(PostgresContext context)
    {
        var pizza = await context.Pizzas.FindAsync(PizzaId);
        var stock = await context.StockItems.FindAsync(StockItemId);
        var drink = await context.Drinks.FindAsync(DrinkId);
        var calzone = await context.Calzones.FindAsync(CalzoneId);
        var orderItem = new OrderItemDTO
        {
            Id = this.Id,
            OrderId = this.OrderId,
            StockItem = stock
        };

        if (pizza != null) orderItem.Pizza = await pizza.ToPizzaDTO(context);
        else if (drink != null) orderItem.Drink = await drink.ToDrinkDTO(context);
        else if (calzone != null) orderItem.Calzone = await calzone.ToCalzoneDTO(context);
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
    public DrinkDTO? Drink { set; get; }
    public CalzoneDTO Calzone { set; get; }
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
            case nameof(DrinkDTO):
                Drink = (DrinkDTO)item;
                Item = ItemType.Drink;
                break;
            case nameof(CalzoneDTO):
                Calzone = (CalzoneDTO)item;
                Item = ItemType.Calzone;
                break;
            default:
                break;
        };
        Quantity = quantity;
    }
}