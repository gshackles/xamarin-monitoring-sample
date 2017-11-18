using System.Collections.Generic;
using CompanySearch.Models;
using CompanySearch.Services;
using CompanySearch.ViewModels;
using Xamarin.Forms;
using CompanySearch.Instrumentation;
using CompanySearch.Instrumentation.Metrics;
using Microsoft.Azure.Mobile.Analytics;

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

            MetricService.Instance.Log(new CountedMetric("viewcompany", tags: new List<string> { $"name:{company.Name}" }));
            Analytics.TrackEvent("Company Viewed", new Dictionary<string, string> { ["name"] = company.Name });

			await Navigation.PushAsync(new CompanyPage(company));

			Companies.SelectedItem = null;
		}
	}
}