using System.Collections.Generic;

namespace MetricsApi.Models
{
	public class MetricsPost
	{
		public string OS { get; set; }
		public IList<TimedMetric> Timed { get; set; }
		public IList<CountedMetric> Counted { get; set; }
	}
}