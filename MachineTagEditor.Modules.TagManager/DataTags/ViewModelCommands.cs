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
        public DelegateCommand RemoveFile { get; set; }

        void initCommands()
        {
            RemoveFile = new DelegateCommand(OnRemoveFile);
           // SelectedTabChanged = new DelegateCommand<SelectionChangedEventArgs> (OnSelectedTabChanged);
        }

        private void OnRemoveFile()
        {
           if(SelectedFile.HasUnsavedChanges)
            {
                var result = MessageBox.Show("Do you wish to save unsaved changes?", "XML File", MessageBoxButton.YesNoCancel);

                if (result.Equals(MessageBoxResult.Yes))
                    SelectedFile.Save();
                else if (result.Equals(MessageBoxResult.Cancel))
                    return;
            }

            XmlFileList.Remove(SelectedFile);
        }

        private void OnSelectedTabChanged(SelectionChangedEventArgs e)
        {
            //SelectedFile = (XmlContainer)(e.Source as TabControl).SelectedItem;
        }
       
    }

    
}
