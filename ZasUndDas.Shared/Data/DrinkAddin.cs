using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DrinkAddin
{
    public int Id { set; get; }

    public int DrinkId { set; get; }

    public int AddinId { set; get; }

    public virtual DAddin Addin { set; get; } = null!;

    public virtual DrinkDTO\ Drink { set; get; } = null!;
}
