namespace ZasUndDas.Shared.Data;

public partial class DrinkBaseDTO : IStoreItem
{
    public int Id { set; get; }

    public string Name { set; get; } = null!;

    public string? Description { set; get; }
    public string? ImagePath { set; get; } = "doodle_drink.png";

    public decimal Price { set; get; }

}
