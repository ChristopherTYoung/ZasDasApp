using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public class Salad
{
    public int Id { set; get; }

    public decimal BasePrice { set; get; }

    public async Task<SaladDTO> ToSaladDTO(PostgresContext context)
    {
        var salad = new SaladDTO
        {
            Id = Id,
            Price = BasePrice,
        };
        var addinIds = await context.SaladAddins
                        .Where(a => a.SaladId == Id)
                        .Select(a => a.AddinId)
                        .ToListAsync();
        salad.Addins = await context.PAddins
                                .Where(a => addinIds.Contains(a.Id))
                                .ToListAsync();
        return salad;
    }
}
public class SaladDTO : ICheckoutItem
{
    public SaladDTO()
    {
        Addins = new List<PAddinDTO>();
    }
    public int Id { set; get; }
    public List<PAddinDTO> Addins { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
    public string? ImagePath { get; set; }
    public string? Name { get; set; }

    public void AddSaladAddin(PAddinDTO addin)
    {
        Addins.Add(addin);
    }

    public string? GetImagePath() => "default";

    public async Task SaveToppingsToDatabase(PostgresContext context)
    {
        foreach (var addin in Addins)
            await context.SaladAddins.AddAsync(new SaladAddin { AddinId = addin.Id, SaladId = this.Id });

        await context.SaveChangesAsync();
    }

    public Salad ToSalad()
    {
        return new Salad
        {
            BasePrice = Price,
        };
    }
}