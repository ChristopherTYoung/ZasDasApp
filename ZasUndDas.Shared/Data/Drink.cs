using Square.Inventory;
using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DrinkDTO : ICheckoutItem
{
    public DrinkDTO()
    {
        DrinkAddin = new List<DrinkAddin>();
    }

    public DrinkDTO(DrinkBaseDTO _base)
    {
        Base = _base;
        BaseId = Base.Id;
        Name = Base.Name;
        DrinkAddin = new List<DrinkAddin>();
    }
    public int Id { set; get; }

    public int BaseId { set; get; }
    public int Quantity { get; set; } = 1;

    public string? Name { set; get; } = null;

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

    public void ChangeQuantity(int quantity)
    {
        Quantity = quantity;
    }
    public DrinkDTO Clean()
    {
        DrinkAddin = new List<DrinkAddin>();
        Base = null;
        return this;
    }
}
