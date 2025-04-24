using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared.Data
{
    // This class is not in our database. Just for consistency in App
    public class CalzoneBase : IStoreItem
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Calzone";
        public decimal Price { get; set; } = 5.99m;
        public string? ImagePath { get; set; } = "calzone_item.jpg";
        public string? Description { get; set; }
    }
}
