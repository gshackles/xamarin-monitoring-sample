using System;
using System.Collections.Generic;
using System.Linq;
using MetricsApi.Models;
using Nancy;
using Nancy.ModelBinding;
using StatsdClient;

namespace MetricsApi.Modules
{
	public class MetricsModule : NancyModule
	{
		public MetricsModule()
		{
			Post["/metrics"] = _ =>
			{
				var post = this.Bind<MetricsPost>();
				var commonTags = new List<string> { $"os:{post.OS}" };

				foreach (var timedMetric in post.Timed ?? Enumerable.Empty<TimedMetric>())
				{
					var tags = commonTags.Union(timedMetric.Tags).ToArray();

                    DogStatsd.Histogram($"evolve.company_search.{timedMetric.KeyName}", timedMetric.ElapsedMilliseconds, tags: tags);
				}

				foreach (var countedMetric in post.Counted ?? Enumerable.Empty<CountedMetric>())
				{
					var tags = commonTags.Union(countedMetric.Tags).ToArray();

                    DogStatsd.Increment($"evolve.company_search.{countedMetric.KeyName}", countedMetric.Count, tags: tags);
				}

				return 200;
			};
		}
	}
}