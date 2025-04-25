using System.Diagnostics.Metrics;

namespace ZasAndDasWeb.Services
{
    public class MetricService
    {
        public static string MeterName = "zasanddas_meter";
        private readonly ObservableGauge<long> totalMemoryUsed;
        public MetricService(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MeterName);
            totalMemoryUsed = meter.CreateObservableGauge<long>("total_memory_used", () => new Measurement<long>(GC.GetTotalMemory(true)));
        }
    }
}
