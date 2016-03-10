using CompanySearch.Models;
using Xamarin.Forms;

namespace CompanySearch.Pages
{
	public partial class CompanyPage : ContentPage
	{
		public CompanyPage(Company company)
		{
			InitializeComponent();

			BindingContext = company;
		}
	}
}

