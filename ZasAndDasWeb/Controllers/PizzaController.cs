using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZasUndDas.Shared;
namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public Pizza GetAll()
        {
            return new();
        }
    }
}