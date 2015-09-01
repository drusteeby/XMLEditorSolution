using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

using System.Configuration;
using MCM.Core.Tags;

namespace MachineTagEditor.Modules.XMLDocument
{

    public class Model
    {

        public ObservableCollection<XmlDataProvider> XMLDataProviderList { get; private set; }

        public Model()
        {
            if (Properties.Settings.Default.filePaths == null) Properties.Settings.Default.filePaths = new StringCollection();

            if (XMLDataProviderList == null)
                XMLDataProviderList = new ObservableCollection<XmlDataProvider>(); 

            XMLDataProviderList.CollectionChanged += XMLDataProviderList_CollectionChanged;
        }

        void XMLDataProviderList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            switch(e.Action) 
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(XmlDataProvider provider in e.NewItems)             
                    {
                        if (Properties.Settings.Default.filePaths != null && !Properties.Settings.Default.filePaths.Contains(provider.Source.LocalPath))
                        {
                            Properties.Settings.Default.filePaths.Add(provider.Source.LocalPath);
                            Properties.Settings.Default.Save();
                        }
                    }
                break;

                case NotifyCollectionChangedAction.Remove:
                    foreach(XmlDataProvider provider in e.OldItems)             
                    {
                        if (Properties.Settings.Default.filePaths != null && Properties.Settings.Default.filePaths.Contains(provider.Source.LocalPath))
                        {
                            Properties.Settings.Default.filePaths.Remove(provider.Source.LocalPath);
                            Properties.Settings.Default.Save();
                        }
                    }

                    break;
            }

            

            
        }

        public void RemoveFile(string filePath)
        {
            XmlDataProvider toRemove = XMLDataProviderList.Single(x => x.Source.LocalPath == filePath);

            if (toRemove != null) XMLDataProviderList.Remove(toRemove);


            
        }

        public void AddFile(string filePath)
        {
            XmlDataProvider provider = new XmlDataProvider();
            provider.Source = new Uri(filePath);
            provider.XPath = "tags";

            if (!XMLDataProviderList.Contains(provider))
                XMLDataProviderList.Add(provider);

        }

        public void ReloadTags()
        {
            TagCollection.ClearTags();

            foreach (string fn in Properties.Settings.Default.filePaths)
                try { TagCollection.LoadTags(fn); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                            
        }


        public void LoadXmlDataProviders()
        {
            XMLDataProviderList.Clear();
            TagCollection.ClearTags();
            

            //required to flag the removal of the file from the list after the foreach block has finished enumerating.
            List<int> indexFlags = new List<int>();

            foreach (string fn in Properties.Settings.Default.filePaths)
            {
                try
                {
                    XmlDataProvider provider = new XmlDataProvider();
                    provider.Source = new Uri(fn);
                    provider.XPath = "tags";

                    if(!XMLDataProviderList.Contains(provider))
                        XMLDataProviderList.Add(provider);


                    TagCollection.LoadTags(fn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (MessageBox.Show("File failed to load:  " + fn + System.Environment.NewLine + " Remove from file List?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        indexFlags.Add(XMLDataProviderList.IndexOf(XMLDataProviderList.Single(x => x.Source.LocalPath == fn)));
                     
                }
                               
            }

            foreach(int index in indexFlags)
            {
                XMLDataProviderList.RemoveAt(index);
            }



            
            
        }


        public void ClearSettings()
        {
            Properties.Settings.Default.filePaths.Clear();
            Properties.Settings.Default.Save();
            XMLDataProviderList.Clear();
        }
    }
}
