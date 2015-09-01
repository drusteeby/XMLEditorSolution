
using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MachineTagEditor.Infrastructure.Interfaces
{
    public interface IXMLService
    {
        //Properties
        ObservableCollection<DataTag> DataTags { get; set; }
        ObservableCollection<DataTag> VirtualTags { get; set; }

        //Methods
        XmlNode AddElement(string name, IList<XmlAttribute> attributes);
        XmlAttribute createAttribute(string name, string value);

        void setDirectory(string dir);
        void setFileName(string fileName);

        void reloadTags();
        void init(string dir = null, string fileName = null);
        
    }
}
