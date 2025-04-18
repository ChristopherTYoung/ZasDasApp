using Microsoft.EntityFrameworkCore;

namespace ZasUndDas.Shared.Data;
public class Drink
{
    public int Id { set; get; }
    public int BaseId { set; get; }

    public async Task<DrinkDTO> ToDrinkDTO(PostgresContext context)
    {
        var drink = new DrinkDTO(context.DrinkBases.First(b => b.Id == BaseId));

        var addinIds = await context.DrinkAddins
                                .Where(a => a.DrinkId == Id)
                                .Select(a => a.AddinId)
                                .ToListAsync();
        drink.Addins = await context.DAddins
                                .Where(a => addinIds.Contains(a.Id))
                                .ToListAsync();
        return drink;
    }
}
public partial class DrinkDTO : ICheckoutItem
{
    public DrinkDTO()
    {
        Addins = new List<DAddinDTO>();
    }

    public DrinkDTO(DrinkBaseDTO _base)
    {
        Base = _base;
        BaseId = Base.Id;
        Name = Base.Name;
        Price = Base.Price;
        Addins = new List<DAddinDTO>();
    }
    public int Id { set; get; }

    public int BaseId { set; get; }
    public int Quantity { get; set; } = 1;
    public string? GetImagePath() => Base.ImagePath;
    public string? Name { set; get; } = null;

    public decimal Price
    {
        set;
        get;
    }

    public DrinkBaseDTO Base { set; get; } = null!;

    public List<DAddinDTO> Addins { set; get; } = null!;
    public void AddDrinkAddin(DAddinDTO addin)
    {
        Addins.Add(addin);
        Price = CalculatePrice();
    }
    public decimal CalculatePrice()
    {

        decimal baseprice = Base.Price;
        foreach (var adin in Addins)
        {
            baseprice += adin.Price;
        }
        return baseprice;

    }

    public void ChangeQuantity(int quantity)
    {
        Quantity = quantity;
    }
    public DrinkDTO Clean()
    {
        Addins = new List<DAddinDTO>();
        Base = null;
        return this;
    }

    public async Task SaveAddinsToDatabase(PostgresContext context)
    {
        foreach (var ad in Addins)
            await context.DrinkAddins.AddAsync(new() { DrinkId = Id, AddinId = ad.Id });

        await context.SaveChangesAsync();
    }

    public Drink ToDrink()
    {
        return new Drink()
        {
            BaseId = this.BaseId
        };

    }
}
