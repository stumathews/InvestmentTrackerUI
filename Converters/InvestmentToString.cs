using InvestmentTrackerApiClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InvestmentTrackerUI.Converters
{
    [ValueConversion(typeof(Investment), typeof(string))]
    public class InvestmentToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var investment = value as Investment;
            return investment == null ? null : $"{investment.Name} {investment.Symbol} {investment.Currency}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
