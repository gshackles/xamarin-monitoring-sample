using System.Collections.Generic;

namespace CompanySearch.Instrumentation.Metrics
{
	public class ApiCallTimedMetric : TimedMetricBase
	{
		private readonly string _pathAndQuery;
		private readonly bool _successful;
		private readonly IList<string> _tags;

		public ApiCallTimedMetric(string pathAndQuery, long elapsedMilliseconds, bool successful)
			: base(elapsedMilliseconds)
		{
			_pathAndQuery = pathAndQuery;
			_successful = successful;

			_tags = new List<string>
			{
				$"call:{_pathAndQuery}",
				$"successful:{successful}"
			};
		}

		public override string ToString() =>
			$"[ApiCallMetric: TimeOccurredUtc={TimeOccurredUtc} PathAndQuery={_pathAndQuery}, ElapsedMilliseconds={ElapsedMilliseconds}, Successful={_successful}]";

		public override string KeyName => "api_calls";
		public override IEnumerable<string> Tags => _tags;
	}
}