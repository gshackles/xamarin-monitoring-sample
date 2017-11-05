using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CompanySearch.Instrumentation.Api
{
    public class MetricsApiClient
    {
        private readonly HttpClient _client = new HttpClient(new InstrumentedHttpClientHandler())
        {
            BaseAddress = new Uri("https://f01776df.ngrok.io")
        };

        public async Task SendMetrics(MetricsPost metrics)
        {
            var json = JsonConvert.SerializeObject(metrics);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await _client.PostAsync("metrics", content).ConfigureAwait(false);
        }
    }
}