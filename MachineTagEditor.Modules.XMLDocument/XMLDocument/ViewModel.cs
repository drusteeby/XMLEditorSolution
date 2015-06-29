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

namespace MachineTagEditor.Modules.XMLDocument
{
    public partial class ViewModel : DependencyObject
    {


        public ViewModel(IEventAggregator eventAggregator)
        {
            initProperties(eventAggregator);
            initCommands();
            initEvents();
        }



    }


}

    