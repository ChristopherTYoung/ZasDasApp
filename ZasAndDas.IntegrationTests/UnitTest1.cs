using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace ZasAndDas.IntegrationTests
{
    public class APITests : IntegrationTestBase
    {

        public APITests(WebApplicationFactory<Program> app, ITestOutputHelper outputHelper) : base(app, outputHelper)
        {
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
