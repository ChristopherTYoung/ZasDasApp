using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class CheeseBread
{
    public int Id { set; get; }

    public int SizeId { set; get; }

    public bool? CookedAtHome { set; get; }
    public decimal Price { set; get; }

    public async Task<CheeseBreadDTO> ToCheeseBreadDTO(PostgresContext context)
    {
        return new CheeseBreadDTO
        {
            Id = Id,
            Size = await context.PizzaSizes.FindAsync(SizeId),
            CookedAtHome = CookedAtHome,
            Price = Price
        };
    }
}

public class CheeseBreadDTO : ICheckoutItem
{
    public int Id { get; set; }

    public PizzaSize Size { set; get; }

    public bool? CookedAtHome { set; get; }
    public decimal Price { set; get; }
    public int Quantity { get; set; }
    public string? Name { get; set; }
    public string? ImagePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string? GetImagePath()
    {
        throw new NotImplementedException();
    }

    public CheeseBread ToCheeseBread()
    {
        return new CheeseBread
        {
            CookedAtHome = CookedAtHome,
            Price = Price,
            SizeId = Size.Id
        };
    }
}
