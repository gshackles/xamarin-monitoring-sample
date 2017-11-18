using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CompanySearch.Models;
using Newtonsoft.Json;
using CompanySearch.Instrumentation;
using CompanySearch.Instrumentation.Metrics;
using Microsoft.Azure.Mobile.Analytics;

namespace CompanySearch.Services
{
	public interface ISearchService
	{
		Task<IEnumerable<Company>> FindCompanies(string query);
	}

	public class SearchService : ISearchService
	{
        private const string BaseUrl = "https://autocomplete.clearbit.com/v1/companies/";
        private readonly HttpClient _client = new HttpClient(new InstrumentedHttpClientHandler())
		{
			BaseAddress = new Uri(BaseUrl)
		};

		public async Task<IEnumerable<Company>> FindCompanies(string query)
		{
            var url = $"suggest?query={Uri.EscapeUriString(query)}";
            var json = await _client.GetStringAsync(url).ConfigureAwait(false);

//            if (query.ToLowerInvariant() == "olo")
//                throw new CompanyTooAwesomeException();

            MetricService.Instance.Log(new CountedMetric("findcompanies", tags: new List<string> { $"query:{query}" }));
            Analytics.TrackEvent("Company Search", new Dictionary<string, string> { ["query"] = query });

            return JsonConvert.DeserializeObject<IEnumerable<Company>>(json);
		}
	}
}