using System.Diagnostics.Metrics;

namespace ZasAndDasWeb.Services
{
    public class MetricService
    {
        public static string MeterName = "zasanddas_meter";
        public readonly ObservableGauge<long> totalMemoryUsed;
        public MetricService(Meter meter)
        {
            totalMemoryUsed = meter.CreateObservableGauge<long>("total_memory_used", () => new Measurement<long>(GC.GetTotalMemory(true)));
        }
    }
}
