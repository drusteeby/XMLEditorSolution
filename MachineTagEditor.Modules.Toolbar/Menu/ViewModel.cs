using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;
using MachineTagEditor.Infrastructure.WizardWindow;
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
            
            MenuItem AddTemplate = new MenuItem();
            AddTemplate.Header = "Add _Template";
            AddTemplate.Command = new DelegateCommand(OnAddConfig);

            MenuItem AddAlarm = new MenuItem();
            AddAlarm.Header = "Add _Alarm";
            AddAlarm.Command = new DelegateCommand(OnAddAlarm);

            file.Items.Add(AddTemplate);
            file.Items.Add(AddAlarm);

            MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(file);

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
