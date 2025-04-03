using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;
using ZasAndDasWeb.Components;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDas.IntegrationTests
{
    public class MobileTests : IntegrationTestBase
    {

        public MobileTests(WebApplicationFactory<Program> app, ITestOutputHelper outputHelper) : base(app, outputHelper)
        {
        }
        [Fact]
        public async Task CanUpdateMenuWithPizzaBase()
        {

        }
    }
}
