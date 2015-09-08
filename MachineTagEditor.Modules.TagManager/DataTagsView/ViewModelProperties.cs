using MachineTagEditor.Infrastructure.Events;
using MCM.Core.Tags;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager
{
    public partial class ViewModel : DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        TagManagerService _service;

        void initProperties()
        {
            _service = new TagManagerService();
            XmlFileList = new ObservableCollection<XmlContainer>();
            EventAggregator.GetEvent<LoadXMLFile>().Subscribe(OnLoadXMLFile);
            EventAggregator.GetEvent<SaveXMLFile>().Subscribe(OnSaveXMLFile);


        }

        private void OnSaveXMLFile(bool obj)
        {
            if (SelectedFile.xmlDataProvider.Document == null)
            {
                EventAggregator.GetEvent<DisplayMessage>().Publish("XML Document is null");
                return;
            }

            SelectedFile.xmlDataProvider.Document.Save(SelectedFile.xmlDataProvider.Source.LocalPath);
            EventAggregator.GetEvent<DisplayMessage>().Publish(SelectedFile.xmlDataProvider.Source.LocalPath + "Sucessfully Saved!");


            

        }

        private void OnLoadXMLFile(bool obj)
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

                //Creating the container and loading the XML file
                XmlContainer toAdd = new XmlContainer();
                toAdd.xmlDataProvider = _service.LoadFromXML(dialog.FileName);

                //Adding every node to a node list
                foreach (XmlNode node in toAdd.xmlDataProvider.Document.GetElementsByTagName("tags")[0].ChildNodes)
                    toAdd.xmlNodes.Add(node);

                ViewSource = new CollectionViewSource();
                ViewSource.Source = toAdd.xmlNodes;

                //Add the file to the file list
                XmlFileList.Add(toAdd);
                
                EventAggregator.GetEvent<DisplayMessage>().Publish("File: " + dialog.FileName + " Added");

                //Set the last directory and save the XML file
                Properties.Settings.Default.lastDirectory = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\'));
                Properties.Settings.Default.loadedFilePath = dialog.FileName;
                Properties.Settings.Default.Save();
            }
        }



        public CollectionViewSource ViewSource
        {
            get { return (CollectionViewSource)GetValue(ViewSourceProperty); }
            set { SetValue(ViewSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for View.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewSourceProperty =
            DependencyProperty.Register("ViewSource", typeof(CollectionViewSource), typeof(ViewModel), new UIPropertyMetadata(null));

        

        public ObservableCollection<XmlContainer> XmlFileList
        {
            get { return (ObservableCollection<XmlContainer>)GetValue(XmlFileListProperty); }
            set { SetValue(XmlFileListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XmlFileList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XmlFileListProperty =
            DependencyProperty.Register("XmlFileList", typeof(ObservableCollection<XmlContainer>), typeof(ViewModel), new UIPropertyMetadata(null));




        public XmlContainer SelectedFile
        {
            get { return (XmlContainer)GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register("SelectedFile", typeof(XmlContainer), typeof(ViewModel), new UIPropertyMetadata(null));

        

        
    }
}
