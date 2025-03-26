using System.Data.SqlTypes;
using System.Drawing;

namespace ZasUndDas.Shared;

public record Pizza : IStoreItem
{
    public int Id { set; get; }
    public PizzaSize Size { set; get; } //size indeed matters
    public string Name { set; get; } = "CYO";
    public double Price { set; get; } = 0d;
    public string Description { set; get; } = string.Empty;
    public List<Ingredient> Ingredients { set; get; } = new List<Ingredient>();
}
public enum PizzaSize { small, medium, large }