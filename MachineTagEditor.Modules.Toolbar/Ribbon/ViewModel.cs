using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;
using MachineTagEditor.Infrastructure.WizardWindow;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MachineTagEditor.Modules.Toolbar.Ribbon
{
    public class ViewModel: DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        public DelegateCommand OpenXMLCommand { get; set; }

        public DelegateCommand<string> RibbonCommand { get; set; }

        public ViewModel()
        {
            OpenXMLCommand = new DelegateCommand(OnOpenXMLCommand);
            RibbonCommand = new DelegateCommand<string>(OnRibbonCommand);

        }

        private void OnRibbonCommand(string commandParameter)
        {
            eventAggregator.GetEvent<RibbonEvent>().Publish(commandParameter);
        }

        private void OnOpenXMLCommand()
        {
            eventAggregator.GetEvent<LoadXMLFile>().Publish(true);
        }

        private void OnSaveXML()
        {
            eventAggregator.GetEvent<SaveXMLFile>().Publish(true);
        }

        private void OnAddGroup()
        {
            var Result = Interaction.InputBox("Question?", "Title", "Default Text");
        }

        private void OnRemoveXML()
        {
            
        }

        private void OnAddXML()
        {
            eventAggregator.GetEvent<LoadXMLFile>().Publish(true);
        }

        public void OnAddAlarm()
        {
            eventAggregator.GetEvent<AddAlarmConfigWizard>().Publish(true);
        }

        public void OnAddConfig()
        {
            eventAggregator.GetEvent<MachineConfigWizard>().Publish(true);
        }
        
    }
}
