using MachineTagEditor.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Modules.XMLDocument
{
    public partial class ViewModel
    {

        public DelegateCommand<RoutedPropertyChangedEventArgs<Object>> SelectedItemChanged { get; set; }
        public DelegateCommand<SelectionChangedEventArgs> SelectionChanged { get; set; }
        public DelegateCommand saveAs { get; set; }
        public DelegateCommand save { get; set; }
        public DelegateCommand AddXMLFile { get; set; }
        public DelegateCommand DeleteNode { get; set; }
        public DelegateCommand RemoveXMLFile { get; set; }
        public DelegateCommand ClearSettings { get; set; }
        public DelegateCommand AlarmView { get; set; }
        public DelegateCommand AddAlarm { get; set; }
        public DelegateCommand ViewDataTags { get; set; }
        public DelegateCommand ReloadTags { get; set; }

        public void initCommands()
        {
            saveAs = new DelegateCommand(OnSaveAs);
            save = new DelegateCommand(OnSave);
            SelectedItemChanged = new DelegateCommand<RoutedPropertyChangedEventArgs<Object>>(OnSelectedItemChanged);            
            AddXMLFile = new DelegateCommand(OnAddXMLFile);
            DeleteNode = new DelegateCommand(OnDeleteNode, CanDeleteNode);
            SelectionChanged = new DelegateCommand<SelectionChangedEventArgs>(OnSelectionChanged);
            RemoveXMLFile = new DelegateCommand(OnRemoveXMLFile);
            ClearSettings = new DelegateCommand(OnClearSettings);
            AlarmView = new DelegateCommand(OnAlarmView);
            AddAlarm = new DelegateCommand(OnAddAlarm);
            ViewDataTags = new DelegateCommand(OnViewDataTags);
            ReloadTags = new DelegateCommand(OnReloadTags);
        }

        public void OnReloadTags()
        {
            _model.ReloadTags();
            _eventAggregator.GetEvent<TagsUpdated>().Publish(true);
        }

        public void OnViewDataTags()
        {
        }

        public void RemoveAllPopups()
        {
            foreach( var v in regionManager.Regions["WindowRegion"].Views)
                regionManager.Regions["WindowRegion"].Remove(v);
        }

        public void OnAddAlarm()
        {
           
        }

        public void OnAlarmView()
        {

        }

        public void OnClearSettings()
        {
            _model.ClearSettings();
        }

        public void OnRemoveXMLFile()
        {
            _model.RemoveFile(SelectedDocument.FullFilePath);
            _model.ReloadTags();
            _eventAggregator.GetEvent<TagsUpdated>().Publish(true);
        }

        public void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            int index = (e.Source as TabControl).SelectedIndex;

            if (XMLDocumentViewContainers.Count >= index && index >= 0) 
                SelectedDocument = XMLDocumentViewContainers.ElementAt(index);

        }

        public void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<Object> e)
        {
            SelectedNode = (XmlNode)(e.NewValue);
            selectHelpView = SelectedNode.Name.ToLower();
            DeleteNode.RaiseCanExecuteChanged();

            
        }

        public void OnDeleteNode()
        {
            SelectedNode.ParentNode.RemoveChild(SelectedNode);
            SelectedNode = null;
            DeleteNode.RaiseCanExecuteChanged();
        }

        public bool CanDeleteNode()
        {
            return (SelectedNode != null);
        }

        public void OnAddXMLFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML files (*.xml)|*.xml";

            if (String.IsNullOrEmpty(Properties.Settings.Default.lastDirectory))
                dialog.InitialDirectory = @"C:\";
            else
                dialog.InitialDirectory = Properties.Settings.Default.lastDirectory;

            dialog.Title = "Please select an XML file to load.";

            if (dialog.ShowDialog() == true)
            {
                _model.AddFile(dialog.FileName);
                Properties.Settings.Default.lastDirectory = dialog.FileName.Substring(0,dialog.FileName.LastIndexOf('\\'));
                Properties.Settings.Default.Save();
            }

            _model.ReloadTags();
            _eventAggregator.GetEvent<TagsUpdated>().Publish(true);
        }


        public void OnSaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                SelectedDocument.XMLDataProvider.Document.Save(dialog.FileName);                           
            }
        }

        public void OnSave()
        {
            SelectedDocument.XMLDataProvider.Document.Save(SelectedDocument.FullFilePath);
            SelectedDocument.unsavedChanges = false;
        }


    }
}
