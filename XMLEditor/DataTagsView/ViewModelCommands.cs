using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XMLEditor.DataTagsView
{
    public partial class ViewModel : DependencyObject
    {
        public DelegateCommand SelectedTagChanged {get;set;}
        void initCommands()
        {
            SelectedTagChanged = new DelegateCommand(OnSelectedTagChanged);
        }

        public void OnSelectedTagChanged()
        {
            DataTagPropertyContainerList.Clear();

            foreach (var prop in SelectedDataTag.GetType().GetProperties())
                try { DataTagPropertyContainerList.Add(new DataTagPropertyContainer(prop.Name.ToString(), prop.GetValue(SelectedDataTag).ToString())); }
                catch { }
                        
        }
    }

    
}
