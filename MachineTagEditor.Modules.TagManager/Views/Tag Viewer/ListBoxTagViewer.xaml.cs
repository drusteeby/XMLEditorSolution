using MachineTagEditor.Modules.TagManager.TemplateSelectors;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml;

namespace MachineTagEditor.Modules.TagManager.Views
{
    /// <summary>
    /// Interaction logic for ListBoxTagViewer.xaml
    /// </summary>
    public partial class ListBoxTagViewer : UserControl
    {
        public ListBoxTagViewer()
        {
            InitializeComponent();
        }


        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            var selected = (lb.SelectedItem as XmlNodeContainer).Node;
            if (selected != null)
                Clipboard.SetData("XmlNode", selected.OuterXml);
        }

        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string xml = null;

            if (Clipboard.ContainsData("XmlNode"))
                xml = (string)Clipboard.GetData("XmlNode");
            else if (Clipboard.ContainsText())
                xml = (string)Clipboard.GetText();

            XmlContainer allNodes = (this.DataContext as XmlContainer);
            if (allNodes == null) return;

            XmlDocument doc = new XmlDocument();
            try {

                doc.LoadXml(xml);
                foreach (XmlNode node in doc.ChildNodes)
                    allNodes.AddNode(node);

            }

            catch { MessageBox.Show("Copied text did not translate to XML");}



            Clipboard.Clear();

        }

        private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsData("XmlNode") || Clipboard.ContainsText();
        }

        private void OnListBoxItemContainerFocused(object sender, RoutedEventArgs e)
        {
            (sender as ListBoxItem).IsSelected = true;
        }
    }
}
