using Microsoft.EntityFrameworkCore;
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
        var pizza = new PizzaDTO(context.PizzaBases.First(p => p.Id == this.Id))
        {
            SizeId = this.SizeId,
            CookedAtHome = this.CookedAtHome
        };
        var addinIds = await context.PizzaAddins
                                .Where(a => a.PizzaId == Id)
                                .Select(a => a.AddinId)
                                .ToListAsync();
        pizza.Addins = await context.PAddins
                                .Where(a => addinIds.Contains(a.Id))
                                .ToListAsync();
        return pizza;
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
        Name = Base.Name;
        Addins = new List<PAddinDTO>();
        ImagePath = Base.ImagePath;
        Price = CalculatePrice();
        Size = ItemSize.medium;
        SizeId = 1;
    }
    public int Id { set; get; }
    public int SizeId { set; get; }
    public int BaseId { set; get; }
    public bool CookedAtHome { get; set; }
    public string? Name { get; set; } = null;
    public string? ImagePath { get; set; }
    public PizzaBaseDTO Base { get; set; }
    public ItemSize Size { set; get; } //size indeed matters
    public PizzaSize? PizzaSize { get; set; }
    public List<PAddinDTO> Addins { set; get; }
    public Sauce? Sauce { get; set; }

    public int Quantity { get; set; } = 1;

    public decimal Price { set; get; }

    private decimal CalculatePrice()
    {
        decimal price = 0;
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

    public void ChangeQuantity(int quantity)
    {
        Quantity = quantity;
    }
    public void ChangeSauce(Sauce sauce)
    {
        Sauce = sauce;
    }
    public async Task SaveToppingsToDatabase(PostgresContext context)
    {
        foreach (var addin in Addins)
            await context.PizzaAddins.AddAsync(new PizzaAddin { AddinId = addin.Id, PizzaId = this.Id });

        await context.SaveChangesAsync();
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
