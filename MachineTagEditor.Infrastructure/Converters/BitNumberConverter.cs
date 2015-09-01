using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MachineTagEditor.Infrastructure.Converters
{
    public class BitNumberConverter : IMultiValueConverter
    {
       
        public object Convert(object[] values, Type targetType,
                object parameter, System.Globalization.CultureInfo culture)
        {
            int result =
                Int32.Parse((string)values[0]) + Int32.Parse((string)values[1]);
            return result.ToString();
        }
        public object[] ConvertBack(object value, Type[] targetTypes,
                object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
        
    }
}
