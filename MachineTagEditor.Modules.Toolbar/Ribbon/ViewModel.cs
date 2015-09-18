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

            ViewAlarmsChecked = true;
            ViewWarningsChecked = true;
            ViewEnumerationsChecked = true;

        }



        public bool ViewAlarmsChecked
        {
            get { return (bool)GetValue(ViewAlarmsCheckedProperty); }
            set { SetValue(ViewAlarmsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewAlarmsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewAlarmsCheckedProperty =
            DependencyProperty.Register("ViewAlarmsChecked", typeof(bool), typeof(ViewModel), new UIPropertyMetadata(null));




        public bool ViewWarningsChecked
        {
            get { return (bool)GetValue(ViewWarningsCheckedProperty); }
            set { SetValue(ViewWarningsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewWarningsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewWarningsCheckedProperty =
            DependencyProperty.Register("ViewWarningsChecked", typeof(bool), typeof(ViewModel), new UIPropertyMetadata(null));



        public bool ViewEnumerationsChecked
        {
            get { return (bool)GetValue(ViewEnumerationsCheckedProperty); }
            set { SetValue(ViewEnumerationsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewEnumerationsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewEnumerationsCheckedProperty =
            DependencyProperty.Register("ViewEnumerationsChecked", typeof(bool), typeof(ViewModel), new UIPropertyMetadata(null));

        

        private void OnRibbonCommand(string commandParameter)
        {
           if(commandParameter.Contains("ViewWarnings"))
                eventAggregator.GetEvent<RibbonEvent>().Publish(commandParameter + ViewWarningsChecked.ToString());
           else if (commandParameter.Contains("ViewAlarms"))
               eventAggregator.GetEvent<RibbonEvent>().Publish(commandParameter + ViewAlarmsChecked.ToString());
           else if (commandParameter.Contains("ViewEnumerations"))
               eventAggregator.GetEvent<RibbonEvent>().Publish(commandParameter + ViewEnumerationsChecked.ToString());
           else
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
