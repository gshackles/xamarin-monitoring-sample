using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StatsdClient;

namespace MetricsApi
{
    public class ValuesController : Controller
    {
        [HttpPost("/metrics")]
        public OkResult LogMetrics([FromBody] LogMetricsCommand command)
        {
            var commonTags = new List<string> { $"os:{command.OS}" };

            foreach (var timedMetric in command.Timed ?? Enumerable.Empty<TimedMetric>())
            {
                var tags = commonTags.Union(timedMetric.Tags).ToArray();
                
                DogStatsd.Histogram($"codecamp.company_search.{timedMetric.KeyName}", timedMetric.ElapsedMilliseconds, tags: tags);
            }

            foreach (var countedMetric in command.Counted ?? Enumerable.Empty<CountedMetric>())
            {
                var tags = commonTags.Union(countedMetric.Tags).ToArray();
                
                DogStatsd.Increment($"codecamp.company_search.{countedMetric.KeyName}", countedMetric.Count, tags: tags);
            }

            return Ok();
        }
    }
}
