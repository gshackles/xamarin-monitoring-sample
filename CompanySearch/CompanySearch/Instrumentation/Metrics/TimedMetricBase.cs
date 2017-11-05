namespace CompanySearch.Instrumentation.Metrics
{
	public abstract class TimedMetricBase : MetricBase
	{
		public long ElapsedMilliseconds { get; }

		protected TimedMetricBase(long elapsedMilliseconds)
		{
			ElapsedMilliseconds = elapsedMilliseconds;
		}
	}
}