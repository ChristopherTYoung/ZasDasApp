using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared.Services
{
    public class CreateRequest
    {
        public string Name { set; get; } = null!;
        public string Email { set; get; } = null!;
        public string PassCode { set; get; } = null!;
    }
}
