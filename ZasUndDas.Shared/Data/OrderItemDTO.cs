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

    public virtual CheeseBread? CheeseBread { set; get; }

    public virtual DrinkDTO? Drink { set; get; }

    public virtual Pizza? Pizza { set; get; }

    public virtual Salad? Salad { set; get; }

    public virtual StockItem? StockItem { set; get; }
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
            default:
                break;
        };
        Quantity = quantity;
    }
}