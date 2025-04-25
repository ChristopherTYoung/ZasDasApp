using System.Diagnostics.Metrics;

namespace ZasAndDasWeb.Services
{
    public class MetricService
    {
        public static string MeterName = "zasanddas_meter";
        public readonly ObservableGauge<long> totalMemoryUsed;
        public Histogram<DateTime> internalErrors;
        public Meter Meter { get; private set; }
        public MetricService(IMeterFactory meterFactory)
        {
            Meter = meterFactory.Create(MeterName);
            totalMemoryUsed = Meter.CreateObservableGauge<long>("total_memory_used", () => GC.GetTotalMemory(true));
            internalErrors = Meter.CreateHistogram<DateTime>("internalErrors");
        }
    }
}
