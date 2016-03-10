using System;
using System.Diagnostics;
using CompanySearch.Instrumentation.Metrics;

namespace CompanySearch.Instrumentation
{
	internal class MetricTimer : IDisposable
	{
		private readonly Func<long, TimedMetricBase> _metricCreator;
		private readonly Stopwatch _stopWatch;
		private bool _disposed;

		public MetricTimer(Func<long, TimedMetricBase> metricCreator)
		{
			_metricCreator = metricCreator;
			_stopWatch = new Stopwatch();
			_stopWatch.Start();
		}

		public void Dispose()
		{
			if (_disposed)
				return;

			_disposed = true;
			_stopWatch.Stop();

			var metric = _metricCreator(_stopWatch.ElapsedMilliseconds);

            MetricService.Instance.Log(metric);
		}
	}
}

