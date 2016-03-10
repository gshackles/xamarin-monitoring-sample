using System;
using Nancy.Hosting.Self;
using StatsdClient;

namespace MetricsApi
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var statsConfig = new StatsdConfig
			{
				StatsdServerName = "127.0.0.1"
			};

			DogStatsd.Configure(statsConfig);

			using (var host = new NancyHost(new Uri("http://localhost:3141")))
			{
				host.Start();
                Console.WriteLine("Listening on localhost:3141");
				Console.ReadLine();
			}
		}
	}
}
