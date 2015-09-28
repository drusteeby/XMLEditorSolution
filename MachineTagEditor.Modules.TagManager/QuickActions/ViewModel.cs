using MachineTagEditor.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.TagManager.QuickActions
{

    public class ViewModel
    {
        public DelegateCommand OpenXMLFileCommand { get; set; }
        public DelegateCommand<string> AddParameterCommand { get; set; }
        public DelegateCommand<string> AddDataTypeCommand { get; set; }
        public DelegateCommand<string> AddAlarmCommand { get; set; }

        [Dependency]
        public IEventAggregator EventAggregator { get; set; } 
        public ViewModel()
        {
            OpenXMLFileCommand = new DelegateCommand(OnOpenXMLFileCommand);
            AddParameterCommand = new DelegateCommand<string>(OnRibbonCommand);
            AddDataTypeCommand = new DelegateCommand<string>(OnRibbonCommand);
            AddAlarmCommand = new DelegateCommand<string>(OnRibbonCommand);
        }

        private void OnRibbonCommand(string commandParameter)
        {
            EventAggregator.GetEvent<RibbonEvent>().Publish(commandParameter);
        }

        private void OnOpenXMLFileCommand()
        {
            EventAggregator.GetEvent<LoadXMLFile>().Publish(true);
        }
    }
}
