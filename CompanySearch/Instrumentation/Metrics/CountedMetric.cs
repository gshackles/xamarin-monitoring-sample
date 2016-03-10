using System.Collections.Generic;

namespace CompanySearch.Instrumentation.Metrics
{
	public class CountedMetric : MetricBase
	{
		public CountedMetric(string key, int count = 1, IList<string> tags = null)
		{
            KeyName = key;
			Count = count;
			Tags = tags;
		}

		public override string ToString() =>
			$"[CountedMetric: TimeOccurredUtc={TimeOccurredUtc} Key={KeyName}, Count={Count}]";

        public override string KeyName { get; }
        public override IEnumerable<string> Tags { get; }
        public int Count { get; }
	}
}