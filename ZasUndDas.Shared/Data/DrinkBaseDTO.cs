namespace ZasUndDas.Shared.Data;

public partial class DrinkBaseDTO : IStoreItem
{
    public int Id { set; get; }

    public string Name { set; get; } = null!;

    public string? Description { set; get; }

    public decimal Price { set; get; }

}
