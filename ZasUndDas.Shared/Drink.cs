using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared
{
    public record Drink : IStoreItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "CYO";
        public string Description { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new List<string>();
        public double Price { get; set; } = 0d;
    }
}