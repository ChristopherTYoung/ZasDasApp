using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DrinkAddin
{
    public int Id { get; set; }

    public int DrinkId { get; set; }

    public int AddinId { get; set; }

    public virtual DAddin Addin { get; set; } = null!;

    public virtual Drink Drink { get; set; } = null!;
}
