using System.Collections.Generic;

namespace MetricsApi.Models
{
	public class CountedMetric
	{
		public string KeyName { get; set; }
		public int Count { get; set; }
		public IList<string> Tags { get; set; }
	}
}