using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.TagManager
{
    public partial class ViewModel : DependencyObject
    {
        public DelegateCommand SelectedTagChanged {get;set;} 
        void initCommands()
        {
            SelectedTagChanged = new DelegateCommand(OnSelectedTagChanged);
        }

        private void OnSelectedTagChanged()
        {
            throw new NotImplementedException();
        }

       
    }

    
}
