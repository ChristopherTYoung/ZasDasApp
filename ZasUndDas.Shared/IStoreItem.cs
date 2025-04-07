namespace ZasUndDas.Shared;

public interface IStoreItem
{
    public int Id { set; get; }
    public string Name { set; get; }
    public double Price { set; get; }
}