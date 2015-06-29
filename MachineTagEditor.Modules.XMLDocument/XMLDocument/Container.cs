using MCM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MachineTagEditor.Modules.XMLDocument
{
    public class ViewContainer
    {
        public XmlDataProvider XMLDataProvider { get; set; }
        public ObservableCollection<DataTag> DataTags { get; set; }        
        public string FullFilePath { get; set; }
        public bool unsavedChanges;

        public string Header
        {
            get
            {
                return FullFilePath.Split('\\').Last() + (unsavedChanges ? "*": " ");
            }

        }
        

        public ViewContainer(XmlDataProvider provider)
        {
            XMLDataProvider = provider;
            XMLDataProvider.DataChanged += XMLDataProvider_DataChanged;

            FullFilePath = provider.Source.LocalPath;

        }

        void XMLDataProvider_DataChanged(object sender, EventArgs e)
        {
            unsavedChanges = true;
        }


        

    }
}
