using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaAddin
{
    public int Id { get; set; }

    public int PizzaId { get; set; }

    public int AddinId { get; set; }

    public int? AddinQuantity { get; set; }

    public virtual PAddin Addin { get; set; } = null!;

    public virtual Pizza _Pizza { get; set; } = null!;
}
