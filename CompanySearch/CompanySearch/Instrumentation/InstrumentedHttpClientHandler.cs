using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CompanySearch.Instrumentation.Metrics;

namespace CompanySearch.Instrumentation
{
	internal class InstrumentedHttpClientHandler : HttpClientHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var successful = false;
			var stopwatch = Stopwatch.StartNew();

			try
			{
                var result = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
				successful = result.IsSuccessStatusCode;

				return result;
			}
			finally
			{
				stopwatch.Stop();

				var metric = new ApiCallTimedMetric(request.RequestUri.LocalPath, stopwatch.ElapsedMilliseconds, successful);
				MetricService.Instance.Log(metric);
			}
		}
	}
}