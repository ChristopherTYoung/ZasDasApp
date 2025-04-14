using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PAddin
{
    public int Id { set; get; }

    public string AddinName { set; get; } = null!;

    public int BasePrice { set; get; }
}
public class PAddinDTO
{
    public int Id { set; get; }
    public string AddinName { set; get; } = null!;
    public double Price { get; set; }
    public bool IsChecked { get; set; } = false;
}
