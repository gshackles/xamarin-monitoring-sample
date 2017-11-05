using System;
using System.Collections.Generic;

namespace CompanySearch.Instrumentation.Metrics
{
	public abstract class MetricBase
	{
		public abstract string KeyName { get; }
		public abstract IEnumerable<string> Tags { get; }

		public DateTime TimeOccurredUtc { get; }

		protected MetricBase()
		{
			TimeOccurredUtc = DateTime.UtcNow;
		}
	}
}