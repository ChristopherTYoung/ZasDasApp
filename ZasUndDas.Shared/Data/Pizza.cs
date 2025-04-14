using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Pizza
{
    public int Id { set; get; }

    public int SizeId { set; get; }

    public int BaseId { set; get; }

    public bool CookedAtHome { set; get; }

    public async Task<PizzaDTO> ToPizzaDTO(PostgresContext context)
    {
        return new PizzaDTO(context.PizzaBases.First(p => p.Id == this.Id))
        {
            SizeId = this.SizeId,
            CookedAtHome = this.CookedAtHome
        };
    }
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
        BaseId = pizzaBase.Id;
        Addins = new List<PAddinDTO>();
        Price = CalculatePrice();
        Size = ItemSize.medium;
        SizeId = 1;
    }
    public int Id { set; get; }
    public int SizeId { set; get; }
    public int BaseId { set; get; }
    public bool CookedAtHome { get; set; }
    public PizzaBaseDTO Base { get; set; }
    public ItemSize Size { set; get; } //size indeed matters
    public PizzaSize? PizzaSize { get; set; }
    public List<PAddinDTO> Addins { set; get; }
    public Sauce? Sauce { get; set; }

    public double Price { set; get; }

    private double CalculatePrice()
    {
        double price = 0;
        price += Base.Price;
        foreach (var addin in Addins)
            price += addin.Price;

        //price += PizzaSize.Price;
        return Math.Round(price, 2);
    }

    public void AddTopping(PAddinDTO addin)
    {
        Addins.Add(addin);
        Price = CalculatePrice();
    }
    public void ChangeSize(PizzaSize size)
    {
        PizzaSize = size;
        SizeId = size.Id;
        Price = CalculatePrice();
    }

    public void ChangeSauce(Sauce sauce)
    {
        Sauce = sauce;
    }
    public async Task SaveToppingsToDatabase(PostgresContext context)
    {
        foreach (var addin in Addins)
            await context.PizzaAddins.AddAsync(new PizzaAddin { AddinId = addin.Id, PizzaId = this.Id });
    }

    public Pizza ToPizza()
    {
        return new Pizza
        {
            SizeId = this.SizeId,
            BaseId = this.BaseId,
            CookedAtHome = this.CookedAtHome
        };
    }
}
