using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Extensions.XML;
using MCM.Core.Tags;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;


namespace MachineTagEditor.Modules.TagManager.DataTags
{
    public partial class ViewModel : DependencyObject
    {


        void initEvents()
        {
            Service.XmlFileList.CollectionChanged += XmlFileList_CollectionChanged;
            EventAggregator.GetEvent<LoadXMLFile>().Subscribe(OnLoadXMLFile);
            EventAggregator.GetEvent<SaveXMLFile>().Subscribe(OnSaveXMLFile);
            EventAggregator.GetEvent<RibbonEvent>().Subscribe(OnRibbonDelete, ThreadOption.UIThread, true);
        }

        private void OnRibbonDelete(string command)
        {
            if (command.ToLower().Contains("delete"))
            {
                var result = MessageBox.Show("Are you sure you want to delete this node?", SelectedFile.SelectedNode.Name, MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result.Equals(MessageBoxResult.Yes))
                    SelectedFile.RemoveSelected();
            }

            else if(command.ToLower().Contains("save"))
            {
                if (command.ToLower().Contains("as"))
                {
                    var filePath = _xmlFileDialog(true);
                    if (filePath != null) SelectedFile.SaveAs(filePath);
                }
                else if (command.ToLower().Contains("all"))
                {
                    foreach (XmlContainer container in XmlFileList)
                        container.Save();
                }
                else
                    SelectedFile.Save();
            }

        }



        //auto selecting the tab of the file we just loaded
        void XmlFileList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            XmlFileList = Service.XmlFileList;
            Dispatcher.BeginInvoke((Action)(() => SelectedIndex = (sender as ObservableCollection<XmlContainer>).Count - 1));
            
        }

        private void OnSaveXMLFile(bool obj)
        {
            if (SelectedFile.Save())
            {
                EventAggregator.GetEvent<DisplayMessage>().Publish("XML Document is null");
                return;
            }


            EventAggregator.GetEvent<DisplayMessage>().Publish(SelectedFile.FullFilePath + " Sucessfully Saved!");


            

        }
        private string _xmlFileDialog(bool saveFile = false)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load XML File...";

            if (saveFile) 
            {
                dialog = new SaveFileDialog();
                dialog.Title = "Save File...";
            }

            dialog.Filter = "XML files (*.xml)|*.xml";


            if (String.IsNullOrEmpty(Properties.Settings.Default.lastDirectory))
                dialog.InitialDirectory = @"C:\";
            else
                dialog.InitialDirectory = Properties.Settings.Default.lastDirectory;

            

            if (dialog.ShowDialog() == true)
            {
                Properties.Settings.Default.lastDirectory = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\'));
                Properties.Settings.Default.loadedFilePath = dialog.FileName;
                Properties.Settings.Default.Save();

                return dialog.FileName;
            }
                

            return null;
        }


        private void OnLoadXMLFile(bool obj)
        {
            var fileName = _xmlFileDialog();

            if (File.Exists(fileName) && Service.LoadFromXML(fileName))
                    EventAggregator.GetEvent<DisplayMessage>().Publish("File: " + fileName + " Added");
            
        }         
        
    }
}
