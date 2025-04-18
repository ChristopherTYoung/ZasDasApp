using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared.Services
{
    public class AuthRequest
    {
        public string Email { get; set; } = null!;
        public string PassCode { get; set; } = null!;
    }
}
