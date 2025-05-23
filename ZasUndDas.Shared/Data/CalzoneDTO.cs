﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;
public class Calzone
{
    public int Id { set; get; }
    public int? SauceId { set; get; }
    public bool? CookedAtHome { set; get; }
    public decimal Price { set; get; }

    public async Task<CalzoneDTO> ToCalzoneDTO(PostgresContext context)
    {
        var calzoneDTO = new CalzoneDTO()
        {
            Id = Id,
            SauceId = SauceId,
            CookedAtHome = CookedAtHome,
            Price = Price
        };
        var addinIds = await context.CalzonAddins
                                .Where(a => a.CalzoneId == Id)
                                .Select(a => a.AddinId)
                                .ToListAsync();
        calzoneDTO.Addins = await context.PAddins
                                .Where(a => addinIds.Contains(a.Id))
                                .ToListAsync();
        return calzoneDTO;
    }
}
public class CalzoneDTO : ICheckoutItem
{
    public CalzoneDTO()
    {
        Addins = new List<PAddinDTO>();
    }

    public CalzoneDTO(CalzoneBase CalzoneBase)
    {
        Base = CalzoneBase;
        Name = Base.Name;
        Price = Base.Price;
        Addins = new List<PAddinDTO>();
    }
    public int Id { set; get; }
    public CalzoneBase? Base { set; get; }
    public int? SauceId { set; get; }
    public bool? CookedAtHome { set; get; }
    public decimal Price { set; get; }
    public string? GetImagePath() => Base.ImagePath;
    public Sauce? Sauce { set; get; }
    public List<PAddinDTO> Addins { set; get; }
    public int Quantity { get; set; }
    public string? Name { get; set; }

    public void AddTopping(PAddinDTO addin)
    {
        Addins.Add(addin);
    }
    public Calzone ToCalzone()
    {
        return new Calzone
        {
            SauceId = this.SauceId,
            CookedAtHome = this.CookedAtHome,
            Price = this.Price
        };
    }
    public async Task SaveToppingsToDatabase(PostgresContext context)
    {
        foreach (var addin in Addins)
            await context.CalzonAddins.AddAsync(new CalzonAddin { AddinId = addin.Id, CalzoneId = this.Id });

        await context.SaveChangesAsync();
    }

}
