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

        public ViewModel()
        {
            MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(new MenuItem());
            MenuItems.First().Header = "_File";
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
