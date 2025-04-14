namespace ZasUndDas.Shared;

public interface IStoreItem
{
    public int Id { set; get; }
    public string Name { set; get; }
    public decimal Price { set; get; }
    public string? Description { set; get; }
}