using CompanySearch.Pages;
using Xamarin.Forms;

namespace CompanySearch
{
	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new SearchPage());
		}
	}
}