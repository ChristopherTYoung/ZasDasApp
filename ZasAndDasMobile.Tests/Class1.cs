using Square.Webhooks.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasAndDasWeb.Controllers;
using ZasAndDasWeb.Services;
using Shouldly;
namespace ZasAndDasMobile.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void Auth()
        {
            var auth = new AuthController(new MockAPIKeyValidationService());
            auth.Create(new ZasUndDas.Shared.Services.CreateRequest() { Name = "jeff", Email = "jeff@gmail.com", PassCode = "jefaf" }).ShouldBe("jeff@gmail.comtest");
            auth.Authenticate(new ZasUndDas.Shared.Services.AuthRequest() { Email = "jeff@gmail.com", PassCode = "jefaf" }).ShouldBe("jeff@gmail.comtest");
        }
    }
}
