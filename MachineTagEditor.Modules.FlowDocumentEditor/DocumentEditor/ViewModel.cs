using Microsoft.Practices.Prism;
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

namespace MachineTagEditor.Modules.FlowDocumentEditor.DocumentEditor
{
    public partial class ViewModel : DependencyObject
    {
        public DelegateCommand printDocument { get; set; }
        public ViewModel()
        {
            printDocument = new DelegateCommand(OnPrintDocument);
        }

        private void OnPrintDocument()
        {
            return;
        }

        public string FlowDocument
        {
            get { return (string)GetValue(FlowDocumentProperty); }
            set { SetValue(FlowDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FlowDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlowDocumentProperty =
            DependencyProperty.Register("FlowDocument", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));
    }
}
