using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.TagManager
{
    public partial class ViewModel : DependencyObject
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            initProperties();
            initCommands();

            
        }
        
    }
}
