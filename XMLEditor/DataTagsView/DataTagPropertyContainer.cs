using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLEditor.DataTagsView
{
    public class DataTagPropertyContainer
    {
        public DataTagPropertyContainer() { }
        public DataTagPropertyContainer(string p_name, string p_value) { PropertyName = p_name; PropertyValue = p_value; }

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
