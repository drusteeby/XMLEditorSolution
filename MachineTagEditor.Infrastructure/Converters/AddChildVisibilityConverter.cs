using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Infrastructure.Converters
{
    public class AddChildVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            XmlNode node = value as XmlNode;

            //If Node is text type then do not show add.
            if (node != null && ((node.NodeType == XmlNodeType.Text) || (node.FirstChild != null && node.FirstChild.NodeType == XmlNodeType.Text)))
            {
                return Visibility.Collapsed;
            }
            else
            //If Node is of other type show add
            {
                return Visibility.Visible;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
