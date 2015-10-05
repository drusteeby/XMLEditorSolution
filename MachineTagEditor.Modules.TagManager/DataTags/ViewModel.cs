using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.TagManager.DataTags
{
    public partial class ViewModel : DependencyObject
    {
        public IEventAggregator EventAggregator { get; set; }
        private Model _model = new Model();
 
        public ViewModel(IEventAggregator eventAggregator, TagManagerService _tm)
        {
            Service = _tm;
            EventAggregator = eventAggregator;

            initProperties();
            initCommands();
            initEvents();            
        }
        
    }
}
