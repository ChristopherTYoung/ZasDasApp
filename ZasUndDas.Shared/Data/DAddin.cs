using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DAddin
{
    public int Id { set; get; }

    public string AddinName { set; get; } = null!;

    public int BasePriceId { set; get; }

    public virtual PricePerItem BasePrice { set; get; } = null!;

    public virtual ICollection<DrinkAddin> DrinkAddins { set; get; } = new List<DrinkAddin>();
}
