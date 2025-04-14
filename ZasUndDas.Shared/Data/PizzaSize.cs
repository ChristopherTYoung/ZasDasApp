using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public class PizzaSize
{
    public int Id { set; get; }

    public string SizeName { set; get; } = null!;

    public decimal Price { get; set; }
}
