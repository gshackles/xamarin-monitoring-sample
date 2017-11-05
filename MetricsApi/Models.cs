using System.Collections.Generic;

namespace MetricsApi 
{
    public class LogMetricsCommand
	{
		public string OS { get; set; }
		public IList<TimedMetric> Timed { get; set; }
		public IList<CountedMetric> Counted { get; set; }
	}

    public class CountedMetric
	{
		public string KeyName { get; set; }
		public int Count { get; set; }
		public IList<string> Tags { get; set; }
	}

    public class TimedMetric
	{
		public string KeyName { get; set; }
        public long ElapsedMilliseconds { get; set; }
		public IList<string> Tags { get; set; }
	}
}