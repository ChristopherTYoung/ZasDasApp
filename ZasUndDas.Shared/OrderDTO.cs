using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared
{
    public class OrderDTO
    {
        public decimal GrossAmt { set; get; }
        public decimal NetAmt { set; get; }
        public decimal SalesTax { set; get; }
        public List<OrderItemDTO> Items { set; get; } = new List<OrderItemDTO>();
        public DateTime? DateOrdered { set; get; }
    }
}
