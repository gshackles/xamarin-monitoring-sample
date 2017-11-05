using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanySearch.Models;
using CompanySearch.Services;
using Xamarin.Forms;

namespace CompanySearch.ViewModels
{
	public class SearchViewModel : ViewModelBase
	{
		private readonly ISearchService _searchService;

		public SearchViewModel(ISearchService searchService)
		{
			_searchService = searchService;

			SearchCommand = new Command(async () => await performSearch());
		}

		public ICommand SearchCommand { get; }

		private string _query;
		public string Query
		{
			get { return _query; }
			set 
			{
				_query = value;
				RaisePropertyChanged(nameof(Query));
			}
		}

		private IList<Company> _companies = new List<Company>();
		public IEnumerable<Company> Companies
		{
			get { return _companies; }
			set 
			{
				_companies = value?.OrderBy(company => company.Name).ToList();
				RaisePropertyChanged(nameof(Companies));
			}
		}

		private bool _isSearching;
		public bool IsSearching
		{
			get { return _isSearching; }
			set
			{
				_isSearching = value;
				RaisePropertyChanged(nameof(IsSearching));
			}
		}

		private async Task performSearch()
		{
			if (string.IsNullOrWhiteSpace(Query))
				return;

			Companies = Enumerable.Empty<Company>();
			IsSearching = true;

            Companies = await _searchService.FindCompanies(Query).ConfigureAwait(false);
			IsSearching = false;
		}
	}
}