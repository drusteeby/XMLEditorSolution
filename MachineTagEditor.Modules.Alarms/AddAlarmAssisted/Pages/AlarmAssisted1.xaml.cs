using MachineTagEditor.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineTagEditor.Modules.Alarms.AddAlarmAssisted.Pages
{
    /// <summary>
    /// Interaction logic for AlarmAssisted1.xaml
    /// </summary>
    public partial class AlarmAssisted1 : UserControl
    {
        public AlarmAssisted1(ViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

    }
}
