using MCM.Core.Objects;
using Microsoft.Practices.Prism.Commands;
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

namespace MachineTagEditor.Modules.Alarms.AddAlarm
{
    public partial class ViewModel : DependencyObject
    {
        public ViewModel(IEventAggregator eventAggregator)
        {
            initCommands();
            initProperties(eventAggregator);
        }       
        
    }
}
