using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Promotion
{
    public int Id { set; get; }

    public string PromotionName { set; get; } = null!;

    public virtual ICollection<OrderPromotion> OrderPromotions { set; get; } = new List<OrderPromotion>();
}
