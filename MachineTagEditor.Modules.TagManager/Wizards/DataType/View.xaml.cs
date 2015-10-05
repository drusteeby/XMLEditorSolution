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
using Xceed.Wpf.Toolkit;

namespace MachineTagEditor.Modules.TagManager.Wizards.DataType
{
    /// <summary>
    /// Interaction logic for AddDataType.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();
            Wizard.Next += Wizard_Next;
            Wizard.Previous += Wizard_Previous;
        }

        private void Wizard_Previous(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Wizard_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            
        }
    }
}
