using Square.Inventory;
using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DrinkDTO : ICheckoutItem
{
    public int Id { set; get; }

    public int BaseId { set; get; }

    public double Price
    {
        set;
        get;
    }

    DrinkBaseDTO Base { set; get; } = null!;

    List<DrinkAddin> DrinkAddin { set; get; } = null!;

    public Double CalculatePrice()
    {

        double baseprice = Base.Price;
        foreach (var adin in DrinkAddin)
        {
            baseprice += adin.Addin.Price;
        }
        return baseprice;

    }
}
