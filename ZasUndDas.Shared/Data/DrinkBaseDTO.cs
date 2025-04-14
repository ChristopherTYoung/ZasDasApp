namespace ZasUndDas.Shared.Data;

public partial class DrinkBaseDTO
{
    public int Id { set; get; }

    public string DrinkName { set; get; } = null!;

    public string? Description { set; get; }

    public decimal Price { set; get; }

}
