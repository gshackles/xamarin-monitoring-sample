using System;
using System.Globalization;
using Xamarin.Forms;

namespace CompanySearch.Converters
{
	public class CompanyDomainConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var domain = value.ToString();

			if (!domain.StartsWith("http", StringComparison.Ordinal))
				return $"http://{domain}";

			return domain;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
	}
}
