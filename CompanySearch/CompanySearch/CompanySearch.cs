using CompanySearch.Pages;
using CompanySearch.Instrumentation;
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace CompanySearch
{
	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new SearchPage());

            MetricService.Instance.Initialize();

            MobileCenter.Start(
               "ios={Your iOS App secret here};" + 
               "android={Your Android App secret here}",
               typeof(Analytics), typeof(Crashes));
		}
	}
}