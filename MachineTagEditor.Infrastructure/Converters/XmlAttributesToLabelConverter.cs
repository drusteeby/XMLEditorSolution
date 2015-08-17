using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Infrastructure.Converters
{
    public class XmlAttributesToLableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            XmlAttributeCollection attributes = value as XmlAttributeCollection;
            string attributeString = String.Empty;
            if (attributes != null)
            {
                //Step to store name attribute if present.
                foreach (XmlAttribute item in attributes)
                {


                    if (item != null)
                    {
                        attributeString += item.Name + ": " + item.Value + " ";
                    }
                }

                return attributeString;

            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
