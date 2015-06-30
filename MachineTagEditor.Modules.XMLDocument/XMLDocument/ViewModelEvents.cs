using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Containers;

namespace MachineTagEditor.Modules.XMLDocument
{
    public partial class ViewModel : DependencyObject
    {
       
        public void initEvents()
        {
            _model.XMLDataProviderList.CollectionChanged += XMLDataProviderList_CollectionChanged;
            
        }

        void XMLDataProviderList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (XmlDataProvider provider in e.NewItems)
                {
                    XMLDocumentViewContainers.Add(new ViewContainer(provider));
                }                
            }

            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(XmlDataProvider provider in e.OldItems)
                {
                    XMLDocumentViewContainers.Remove(XMLDocumentViewContainers.Single(x => x.XMLDataProvider == provider));
                }
            }

        }


    }
}

    