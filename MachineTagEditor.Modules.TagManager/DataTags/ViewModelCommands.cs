using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MachineTagEditor.Modules.TagManager.DataTags
{
    public partial class ViewModel : DependencyObject
    {
        public DelegateCommand<SelectionChangedEventArgs> SelectedTabChanged {get;set;}

        void initCommands()
        {
           // SelectedTabChanged = new DelegateCommand<SelectionChangedEventArgs> (OnSelectedTabChanged);
        }

        private void OnSelectedTabChanged(SelectionChangedEventArgs e)
        {
            //SelectedFile = (XmlContainer)(e.Source as TabControl).SelectedItem;
        }
       
    }

    
}
