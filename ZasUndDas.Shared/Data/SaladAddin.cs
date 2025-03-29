using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class SaladAddin
{
    public int Id { get; set; }

    public int SaladId { get; set; }

    public int AddinId { get; set; }

    public int? Quantity { get; set; }

    public virtual PAddin Addin { get; set; } = null!;

    public virtual Salad Salad { get; set; } = null!;
}
