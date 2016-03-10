using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CompanySearch.Instrumentation.Api;
using CompanySearch.Instrumentation.Metrics;
using Xamarin.Forms;

namespace CompanySearch.Instrumentation
{
	public class MetricService
	{
		private const int FlushWindowMinutes = 5;
		private const int FlushIntervalSeconds = 10;

		private readonly ConcurrentQueue<MetricBase> _metrics = new ConcurrentQueue<MetricBase>(); 
        private readonly MetricsApiClient _metricsClient = new MetricsApiClient();
		private bool _isSyncing = true;
        private TargetPlatform _platform = TargetPlatform.Other;

		public static readonly MetricService Instance = new MetricService();

		public void Log(MetricBase timedMetric)
		{
			Debug.WriteLine($"logging metric {timedMetric}");

			_metrics.Enqueue(timedMetric);
		}

        public void Initialize(TargetPlatform platform)
		{
            _platform = platform;

			Task.Run(async () =>
			{
				while (_isSyncing)
				{
                    await Task.Delay(FlushIntervalSeconds*1000).ConfigureAwait(false);
                    await Flush().ConfigureAwait(false);
				}
			});
		}

		internal async Task Flush()
		{
			var metrics = new List<MetricBase>();

			try
			{
				MetricBase queuedMetric;
				while (_metrics.TryDequeue(out queuedMetric))
					metrics.Add(queuedMetric);

				Debug.WriteLine($"Flush called for {metrics.Count} metric(s)");

				if (!metrics.Any())
					return;

				// to avoid skewing stats we'll give up sending metrics after the window passes
				var dropMetricsBeforeUtc = DateTime.UtcNow.AddMinutes(-FlushWindowMinutes);
				var droppedCount = metrics.RemoveAll(metric => metric.TimeOccurredUtc < dropMetricsBeforeUtc);

				if (droppedCount > 0)
					Debug.WriteLine($"Dropped {droppedCount} old metric(s)");

				if (!metrics.Any())
					return;

				Debug.WriteLine($"Flushing {metrics.Count} metric(s)");

                await _metricsClient.SendMetrics(
                    new MetricsPost(
                        _platform, 
                        metrics.OfType<TimedMetricBase>().ToList(),
                        metrics.OfType<CountedMetric>().ToList())).ConfigureAwait(false);

				Debug.WriteLine($"Successfully flushed {metrics.Count} metric(s)");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error flushing metrics, adding them back to the queue");

				Debug.WriteLine(ex);

				// if something goes wrong, just add 'em back so we'll try again
				foreach (var metric in metrics)
					_metrics.Enqueue(metric);
			}
		}

		public void Dispose()
		{
			_isSyncing = false;

			Debug.WriteLine("Disposing, doing one last flush");

			// if we're going away, try to squeeze out any remaining metrics
			Flush();
		}
	}
}