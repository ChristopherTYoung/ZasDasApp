using System.Diagnostics.Metrics;
using System.Threading;

namespace ZasAndDasWeb.Services
{
    public class MetricService
    {
        public static string MeterName = "zasanddas_meter";
        public readonly ObservableGauge<long> totalMemoryUsed;
        public Histogram<int> ResponseCodes;
        public Meter Meter { get; private set; }
        public MetricService(IMeterFactory meterFactory)
        {
            Meter = meterFactory.Create(MeterName);
            totalMemoryUsed = Meter.CreateObservableGauge<long>("total_memory_used", () => GC.GetTotalMemory(true));
            ResponseCodes = Meter.CreateHistogram<int>("response_codes");
        }
    }
}
