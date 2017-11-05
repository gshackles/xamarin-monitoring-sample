using System.Collections.Generic;
using CompanySearch.Instrumentation.Metrics;

namespace CompanySearch.Instrumentation.Api
{
    public class MetricsPost
    {
        public string OS { get; }
        public IList<TimedMetricBase> Timed { get; }
        public IList<CountedMetric> Counted { get; }

        public MetricsPost(string platform, IList<TimedMetricBase> timedMetrics, IList<CountedMetric> countedMetrics)
        {
            OS = platform;
            Timed = timedMetrics;
            Counted = countedMetrics;
        }
    }
}