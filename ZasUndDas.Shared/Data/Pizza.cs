using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Pizza
{
    public int Id { set; get; }

    public int SizeId { set; get; }

    public int BaseId { set; get; }

    public bool CookedAtHome { set; get; }

}
public class PizzaDTO : ICheckoutItem
{
    public PizzaDTO()
    {
        Addins = new List<PAddinDTO>();
    }
    public PizzaDTO(PizzaBaseDTO pizzaBase)
    {
        Base = pizzaBase;
        Addins = new List<PAddinDTO>();
    }
    public int Id { set; get; }
    public int SizeId { set; get; }
    public int BaseId { set; get; }
    public bool CookedAtHome { get; set; }
    public PizzaBaseDTO Base { get; set; } = null!;
    public ItemSize Size { set; get; } //size indeed matters
    public List<PAddinDTO> Addins { set; get; }

    public double GetPrice()
    {
        double price = 0;
        price += Base.Price;
        foreach (var addin in Addins)
            price += addin.Price;
        return price;
    }
    public void AddTopping(PAddinDTO addin)
    {
        Addins.Add(addin);
    }
}
