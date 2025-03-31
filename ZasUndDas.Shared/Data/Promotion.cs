using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Promotion
{
    public int Id { get; set; }

    public string PromotionName { get; set; } = null!;

    public virtual ICollection<OrderPromotion> OrderPromotions { get; set; } = new List<OrderPromotion>();
}
