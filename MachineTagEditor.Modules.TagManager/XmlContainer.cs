using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager
{
    public partial class XmlContainer : DependencyObject
    {
        public ObservableCollection<XmlNode> XMLNodes { get; set; }
        public XmlDataProvider xmlDataProvider { get; set; }

        public string Header
        {
            get
            {
                if (xmlDataProvider.Source != null)
                    return xmlDataProvider.Source.LocalPath.Split('\\').Last() /*+ (unsavedChanges ? "*" : " ")*/;

                else if (xmlDataProvider.Document != null)
                    return xmlDataProvider.Document.BaseURI.Split('/').Last();

                return "Source and Document Null!";
            }

        }

        public XmlContainer()
        {
            XMLNodes = new ObservableCollection<XmlNode>();
            xmlDataProvider = new XmlDataProvider();
        }


    }
}
