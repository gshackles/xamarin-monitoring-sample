using System.Collections.Generic;
using CompanySearch.Instrumentation.Metrics;
using Xamarin.Forms;

namespace CompanySearch.Instrumentation.Api
{
    public class MetricsPost
    {
        public string OS { get; }
        public IList<TimedMetricBase> Timed { get; }
        public IList<CountedMetric> Counted { get; }

        public MetricsPost(TargetPlatform platform, IList<TimedMetricBase> timedMetrics, IList<CountedMetric> countedMetrics)
        {
            OS = platform.ToString();
            Timed = timedMetrics;
            Counted = countedMetrics;
        }
    }
}