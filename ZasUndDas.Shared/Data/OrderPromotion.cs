using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class OrderPromotion
{
    public int Id { get; set; }

    public int PromotionId { get; set; }

    public int OrderId { get; set; }

    public decimal? DollarAmountOff { get; set; }

    public virtual PizzaOrder Order { get; set; } = null!;

    public virtual Promotion Promotion { get; set; } = null!;
}
