using System.Collections.Generic;

namespace MetricsApi.Models
{
	public class TimedMetric
	{
		public string KeyName { get; set; }
        public long ElapsedMilliseconds { get; set; }
		public IList<string> Tags { get; set; }
	}
}