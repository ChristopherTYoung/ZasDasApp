using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class OrderPromotion
{
    public int Id { set; get; }

    public int PromotionId { set; get; }

    public int OrderId { set; get; }

    public decimal? DollarAmountOff { set; get; }

    public virtual PizzaOrder Order { set; get; } = null!;

    public virtual Promotion Promotion { set; get; } = null!;
}
