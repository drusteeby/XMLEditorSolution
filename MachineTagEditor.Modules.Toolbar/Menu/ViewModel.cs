using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Events;
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
            MenuItem test = new MenuItem();

            file.Header = "_File";
            test.Header = "_Add Template";
            test.Command = new DelegateCommand(OnAddConfig);
            file.Items.Add(test);

            MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(file);

        }

        public void OnAddConfig()
        {
            eventAggregator.GetEvent<ChangeWizardVisibility>().Publish(Visibility.Visible);
            regionManager.RequestNavigate(RegionNames.PageOverlayRegion, ViewNames.AddConfig);
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
