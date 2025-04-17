using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;
public class Calzone
{
    public int Id { set; get; }
    public int? SauceId { set; get; }
    public bool? CookedAtHome { set; get; }
    public decimal Price { set; get; }
}
public class CalzoneDTO : ICheckoutItem
{
    public int Id { set; get; }
    public int? SauceId { set; get; }
    public bool? CookedAtHome { set; get; }
    public decimal Price { set; get; }
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
}
