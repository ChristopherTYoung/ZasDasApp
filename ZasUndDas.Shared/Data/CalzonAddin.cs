using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class CalzonAddin
{
    public int Id { get; set; }

    public int CalzoneId { get; set; }

    public int AddinId { get; set; }

    public int? Quantity { get; set; }

    public virtual PAddin Addin { get; set; } = null!;

    public virtual Calzone Calzone { get; set; } = null!;
}
