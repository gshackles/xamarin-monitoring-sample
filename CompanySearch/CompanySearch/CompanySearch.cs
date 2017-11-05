using CompanySearch.Pages;
using CompanySearch.Instrumentation;
using Xamarin.Forms;

namespace CompanySearch
{
	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new SearchPage());

            MetricService.Instance.Initialize();
		}
	}
}