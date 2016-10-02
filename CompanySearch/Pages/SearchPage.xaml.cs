using System.Collections.Generic;
using CompanySearch.Models;
using CompanySearch.Services;
using CompanySearch.ViewModels;
using Xamarin.Forms;
using CompanySearch.Instrumentation;
using CompanySearch.Instrumentation.Metrics;

namespace CompanySearch.Pages
{
	public partial class SearchPage : ContentPage
	{
		public SearchPage()
		{
			InitializeComponent();

			BindingContext = new SearchViewModel(new SearchService());

			Query.Keyboard = Keyboard.Create(0);
			NavigationPage.SetBackButtonTitle(this, "");
		}

		private async void onCompanySelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var company = e.SelectedItem as Company;

			await Navigation.PushAsync(new CompanyPage(company));

			Companies.SelectedItem = null;
		}
	}
}