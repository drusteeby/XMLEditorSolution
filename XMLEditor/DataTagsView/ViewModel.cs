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
        public ViewModel()
        {
            initProperties();
            initCommands();
        }

    }
}
