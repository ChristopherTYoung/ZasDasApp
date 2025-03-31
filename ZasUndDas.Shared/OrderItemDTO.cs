using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared.Data;

namespace ZasUndDas.Shared;

public class OrderItemDTO
{
    //public DrinkDTO {set; get;}
    public StockItemDTO? StockItem { set; get; }
    public PizzaDTO? Pizza { set; get; }
    public ItemType? Item { set; get; }
    public int Quantity { set; get; }
    public OrderItemDTO()
    {
    }

    public OrderItemDTO(IStoreItem item, int quantity = 1)
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

public enum ItemType { Stock, Pizza, Drink, Salad, Calzone, CheeseBread }
