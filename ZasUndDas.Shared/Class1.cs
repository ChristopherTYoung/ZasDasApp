using System.Data.SqlTypes;
using System.Drawing;

namespace ZasUndDas.Shared;

public record Pizza
{
    public string Name { set; get; } = "";
    public PizzaSize Size { set; get; } //size indeed matters

    public string Description;
    public List<Ingredient> Ingredients { set; get; }
}
public record Ingredient
{
    public string Name { set; get; }
    public string Description { set; get; }
}
public enum PizzaSize { small, medium, large}