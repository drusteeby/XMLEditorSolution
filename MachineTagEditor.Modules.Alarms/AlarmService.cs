using MachineTagEditor.Infrastructure;
using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Modules.Alarms
{
    public class AlarmService
    {
        public XmlDataProvider alarmDataProvider { get; set; }

        public ObservableCollection<DataTag> alarmDataTags { get; set; }
        public ObservableCollection<DataTag> alarmVirtualTags { get; set; }
        string _fileDirectory, _fileName;
        string _rootName = "tags";

        public XmlDocument Document
        {
            get { return alarmDataProvider.Document; }
        }
        public string FullFilePath
        {
            get { return _fileDirectory + _fileName; }
        }

        public string TempFilePath
        {
            get { return _fileDirectory + "temp" + _fileName; }
        }

        public AlarmService()
        {
            setDirectory(@"C:\appData\Machine Tag Editor\");
            setFileName(@"alarms.xml");

            alarmDataProvider = new XmlDataProvider();
            alarmDataProvider.Document = new XmlDocument();
            try
            {
                alarmDataProvider.Document.Load(FullFilePath);
            }
            catch
            {
                
                alarmDataProvider.Document.AppendChild(alarmDataProvider.Document.CreateElement("tags"));
                alarmDataProvider.Document.Save(FullFilePath);
            }


            alarmDataProvider.Source = new Uri(FullFilePath);
            alarmDataProvider.XPath = _rootName;

            alarmDataTags = new ObservableCollection<DataTag>();
            alarmVirtualTags = new ObservableCollection<DataTag>();
            reloadTags();

            

        }

        public void setDirectory(string dir)
        {
            _fileDirectory = dir;

            if (!Directory.Exists(_fileDirectory))
                Directory.CreateDirectory(_fileDirectory);  

        }
        public void setFileName(string fileName)
        {
            _fileName = fileName;
        }

        public XmlAttribute createAttribute(string name, string value)
        {
            if (name == null || value == null) throw new Exception("Parameters cannot be null. name: " + name + " value: " + value);

            XmlAttribute creation = Document.CreateAttribute(name);
            creation.Value = value;

            return creation;
        }

        public XmlNode AddElement(string name, IList<XmlAttribute> attributes)
        {
            Document.Save(TempFilePath);
            Document.Load(FullFilePath);

            XmlNode root = Document.SelectSingleNode(_rootName);
            XmlNode toAdd = Document.CreateElement(name);

            foreach (XmlAttribute attr in attributes)
                toAdd.Attributes.Append(attr);

            root.AppendChild(toAdd);
            Document.Save(FullFilePath);
            try
            {
                reloadTags();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Document.Load(TempFilePath);
                Document.Save(FullFilePath);
                reloadTags();
            }

            return toAdd;

        }

        public void reloadTags()
        {
            TagCollection.ClearTags();
            TagCollection.LoadTags(FullFilePath);

            alarmDataTags.Clear();
            foreach (DataTag dt in TagCollection.DataTags)
                alarmDataTags.Add(dt);

            alarmVirtualTags.Clear();
            foreach (DataTag dt in TagCollection.VirtualTags)
                alarmVirtualTags.Add(dt);

            TagCollection.ClearTags();

            Document.Save(FullFilePath);
        }

    }
}
