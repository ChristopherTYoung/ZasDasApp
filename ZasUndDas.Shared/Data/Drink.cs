using Square.Inventory;
using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DrinkDTO : ICheckoutItem
{
    public int Id { set; get; }

    public int BaseId { set; get; }

    public decimal Price
    {
        set;
        get;
    }

    DrinkBaseDTO Base { set; get; } = null!;

    List<DrinkAddin> DrinkAddin { set; get; } = null!;

    public decimal CalculatePrice()
    {

        decimal baseprice = Base.Price;
        foreach (var adin in DrinkAddin)
        {
            baseprice += adin.Addin.Price;
        }
        return baseprice;

    }
    public DrinkDTO Clean()
    {
        DrinkAddin = new List<DrinkAddin>();
        Base = null;
        return this;
    }
}
