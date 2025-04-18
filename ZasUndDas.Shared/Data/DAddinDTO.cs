using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DAddinDTO
{
    public int Id { set; get; }

    public string AddinName { set; get; } = null!;

    public decimal Price { set; get; }
    public bool IsChecked { get; set; } = false;
}
