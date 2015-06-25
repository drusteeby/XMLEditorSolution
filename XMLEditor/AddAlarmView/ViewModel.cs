using MCM.Core.Objects;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XMLEditor.AddAlarmView
{
    public partial class ViewModel : DependencyObject
    {
        public ViewModel()
        {
            initCommands();
            initProperties();
        }       
        
    }
}
