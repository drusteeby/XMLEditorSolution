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

namespace MachineTagEditor.Modules.Toolbar.Menu
{
    public class ViewModel: DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        public ViewModel()
        {
            MenuItem file = new MenuItem(); 
            file.Header = "_File";

            MenuItem AddGroup = new MenuItem();
            AddGroup.Header = "New Group";
            AddGroup.Command = new DelegateCommand(OnAddGroup);
            
            MenuItem AddTemplate = new MenuItem();
            AddTemplate.Header = "Add _Template";
            AddTemplate.Command = new DelegateCommand(OnAddConfig);

            MenuItem AddAlarm = new MenuItem();
            AddAlarm.Header = "Add _Alarm";
            AddAlarm.Command = new DelegateCommand(OnAddAlarm);

            file.Items.Add(AddGroup);
            file.Items.Add(AddTemplate);
            file.Items.Add(AddAlarm);

            MenuItem XML  = new MenuItem();
            XML.Header = "_XML";

            MenuItem AddXML = new MenuItem();
            AddXML.Header = "_Add XML";
            AddXML.Command = new DelegateCommand(OnAddXML);

            MenuItem RemoveXML = new MenuItem();
            RemoveXML.Header = "_Remove XML";
            RemoveXML.Command = new DelegateCommand(OnRemoveXML);

            MenuItem SaveXML = new MenuItem();
            SaveXML.Header = "_Remove XML";
            SaveXML.Command = new DelegateCommand(OnSaveXML);


            XML.Items.Add(AddXML);
            XML.Items.Add(RemoveXML);
            XML.Items.Add(SaveXML);

            MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(file);
            MenuItems.Add(XML);

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


        public ObservableCollection<MenuItem> MenuItems
        {
            get { return (ObservableCollection<MenuItem>)GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuItemsProperty =
            DependencyProperty.Register("MenuItems", typeof(ObservableCollection<MenuItem>), typeof(ViewModel), new UIPropertyMetadata(null));

        
    }
}
