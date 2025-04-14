using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public class CalzoneDTO
{
    public int Id { set; get; }
    public int? SauceId { set; get; }
    public bool? CookedAtHome { set; get; }
    public decimal Price { set; get; }
    public virtual Sauce? Sauce { set; get; }
}
