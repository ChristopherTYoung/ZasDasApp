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
        var salad = await context.Salads.FindAsync(SaladId);
        var cheeseBread = await context.CheeseBreads.FindAsync(CheeseBreadId);
        var orderItem = new OrderItemDTO
        {
            Id = this.Id,
            OrderId = this.OrderId,
            StockItem = stock
        };

        if (pizza != null) orderItem.Pizza = await pizza.ToPizzaDTO(context);
        else if (drink != null) orderItem.Drink = await drink.ToDrinkDTO(context);
        else if (calzone != null) orderItem.Calzone = await calzone.ToCalzoneDTO(context);
        else if (salad != null) orderItem.Salad = await salad.ToSaladDTO(context);
        else if (cheeseBread != null) orderItem.CheeseBread = await cheeseBread.ToCheeseBreadDTO(context);

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
    public CalzoneDTO? Calzone { set; get; }
    public SaladDTO? Salad { set; get; }
    public CheeseBreadDTO? CheeseBread { set; get; }
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
            case nameof(SaladDTO):
                Salad = (SaladDTO)item;
                Item = ItemType.Salad;
                break;
            case nameof(CheeseBreadDTO):
                CheeseBread = (CheeseBreadDTO)item;
                Item = ItemType.CheeseBread;
                break;
            default:
                break;
        };
        Quantity = quantity;
    }
}