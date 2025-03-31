using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DAddin
{
    public int Id { get; set; }

    public string AddinName { get; set; } = null!;

    public int BasePriceId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<DrinkAddin> DrinkAddins { get; set; } = new List<DrinkAddin>();
}
