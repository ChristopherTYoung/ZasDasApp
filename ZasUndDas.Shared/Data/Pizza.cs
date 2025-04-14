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
    public int SizeId { set; get; } = 2;
    public int BaseId { set; get; }
    public bool CookedAtHome { get; set; }
    public PizzaBaseDTO Base { get; set; } = null!;
    public ItemSize Size { set; get; } = ItemSize.medium; //size indeed matters
    public PizzaSize PizzaSize { get; set; }
    public List<PAddinDTO> Addins { set; get; }
    public Sauce Sauce { get; set; }

    public double Price
    {
        set { }
        get
        {
            double price = 0;
            price += Base.Price;
            foreach (var addin in Addins)
                price += addin.Price;
            return price;
        }
    }
    public void AddTopping(PAddinDTO addin)
    {
        Addins.Add(addin);
    }
    public void ChangeSize(PizzaSize size)
    {
        PizzaSize = size;
        SizeId = size.Id;
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
}
