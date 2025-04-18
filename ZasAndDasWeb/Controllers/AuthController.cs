using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ZasAndDasWeb.Services;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAPIKeyValidationService validator) : ControllerBase
    {
        [HttpPost("authenticate")]
        public string Authenticate(AuthRequest request)
        {
            return validator.Authenticate(request);
        }
        [HttpPost("create")]
        public async Task<string> Create(CreateRequest request)
        {
            return await validator.CreateAccount(request);
        }
    }
}
