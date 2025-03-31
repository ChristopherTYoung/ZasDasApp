namespace ZasUndDas.Shared;

public interface IStoreItem
{
    public int Id { get; set; }
    public string Name { set; get; }
    public double Price { set; get; }
}