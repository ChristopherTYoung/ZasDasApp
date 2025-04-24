using System.Diagnostics.Metrics;

namespace ZasAndDasWeb.Services
{
    public class MetricService
    {
        private readonly ObservableGauge<long> totalMemoryUsed;
        public MetricService(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create("zasanddas_meter");
            totalMemoryUsed = meter.CreateObservableGauge<long>("total_memory_used", () => GC.GetTotalMemory(true));
        }
    }
}
