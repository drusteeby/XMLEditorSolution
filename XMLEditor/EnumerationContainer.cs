using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLEditor
{
    public class EnumerationContainer
    {
        public EnumerationContainer()
        {

        }
        public EnumerationContainer(string value)
        {
            Value = value;
        }

        public EnumerationContainer(string value, string text)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }
    }
}
