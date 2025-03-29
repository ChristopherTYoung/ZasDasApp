using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Sauce
{
    public int Id { get; set; }

    public string SauceName { get; set; } = null!;

    public virtual ICollection<Calzone> Calzones { get; set; } = new List<Calzone>();
}
