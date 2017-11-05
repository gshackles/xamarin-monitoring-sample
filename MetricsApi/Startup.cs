using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StatsdClient;

namespace MetricsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) => services.AddMvc();

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();

            DogStatsd.Configure(new StatsdConfig { StatsdServerName = "127.0.0.1" });
        }
    }
}
