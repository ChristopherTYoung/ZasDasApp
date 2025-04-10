using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaAddin
{
    public int Id { set; get; }

    public int PizzaId { set; get; }

    public int AddinId { set; get; }

    public int? AddinQuantity { set; get; }

    public virtual PAddin Addin { set; get; } = null!;

    public virtual Pizza _Pizza { set; get; } = null!;
}

public class PizzaAddinDTO
{
    public int Id { set; get; }
    public int PizzaId { set; get; }
    public int AddinId { set; get; }
    public int? AddinQuantity { set; get; }
}