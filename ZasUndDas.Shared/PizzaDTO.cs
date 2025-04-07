using System.Data.SqlTypes;
using System.Drawing;
using ZasUndDas.Shared.Data;
namespace ZasUndDas.Shared;

public record PizzaDTO : IStoreItem
{
    public ItemSize Size { set; get; } //size indeed matters
    public string Name { set; get; } = "CYO";
    public double Price { set; get; } = 0d;
    public string Description { set; get; } = string.Empty;
    public List<int> Ingredients { set; get; } = new List<int>();
    public int Id { set; get; }
}
public enum ItemSize { small, medium, large }