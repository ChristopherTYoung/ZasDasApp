using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared
{
    public class OrderDTO
    {
        public decimal GrossAmt { get; set; }
        public decimal NetAmt { get; set; }
        public decimal SalesTax { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
        public DateTime? DateOrdered { get; set; }
    }
}
