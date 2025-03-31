using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ZasAndDas.IntegrationTests
{
    public class XUnitLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _outputHelper;

        public XUnitLoggerProvider(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new XUnitLogger(_outputHelper, categoryName);
        }

        public void Dispose() { }
    }
}
