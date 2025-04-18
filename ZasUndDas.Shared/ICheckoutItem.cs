using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared
{
    public interface ICheckoutItem
    {
        public int Id { get; set; }

        public decimal Price { get; }
        public int Quantity { get; set; }
        public string? ImagePath { get; set; }
        public string? Name { get; set; }
    }
}
