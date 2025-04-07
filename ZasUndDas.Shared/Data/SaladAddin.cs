using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class SaladAddin
{
    public int Id { set; get; }

    public int SaladId { set; get; }

    public int AddinId { set; get; }

    public int? Quantity { set; get; }

    public virtual PAddin Addin { set; get; } = null!;

    public virtual Salad Salad { set; get; } = null!;
}
