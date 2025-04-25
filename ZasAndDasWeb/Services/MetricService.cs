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
            ResponseCodes = Meter.CreateHistogram<int>(
                 name: "response_codes",
        unit: "range",
        description: "the number of response codes that we have",
        advice: new InstrumentAdvice<int> { HistogramBucketBoundaries = [0, 100, 200, 300, 400, 500] });
        }
        public void RecordStatusCode(int statusCode)
        {
            // e.g., 2xx -> 200, 3xx -> 300
            int bucket = (statusCode / 100) * 100;
            ResponseCodes.Record(bucket);
        }
    }
}
