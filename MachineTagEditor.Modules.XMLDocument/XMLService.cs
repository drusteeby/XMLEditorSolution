using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineTagEditor.Infrastructure.Interfaces;
using System.Xml;
using System.Collections.ObjectModel;
using MCM.Core.Objects;
using System.Windows.Data;
using System.IO;
using System.Xml.Schema;
using MachineTagEditor.Infrastructure;

namespace MachineTagEditor.Modules.XMLDocument
{
    public class XMLService : IXMLService
    {
        public XmlDataProvider DataProvider { get; set; }

        public ObservableCollection<DataTag> DataTags { get; set; }
        public ObservableCollection<DataTag> VirtualTags { get; set; }
        string _fileDirectory, _fileName;
        string _rootName = "tags";

        public XmlDocument Document
        {
            get { return DataProvider.Document; }
        }
        public string FullFilePath
        {
            get { return _fileDirectory + _fileName; }
        }

        public string TempFilePath
        {
            get { return _fileDirectory + "temp" + _fileName; }
        }


        public XMLService()
        {

        }

        public void init(string dir = null, string fileName = null)
        {
            if (dir != null) setDirectory(dir);
            if (fileName != null) setFileName(fileName);

            if (String.IsNullOrEmpty(_fileDirectory) || String.IsNullOrEmpty(_fileName))
            {
                throw new Exception("File name or directory not set when writing XML");
            }

            DataProvider = new XmlDataProvider();
            DataProvider.Document = new XmlDocument();

            if (File.Exists(FullFilePath))
                DataProvider.Document.Load(FullFilePath);
            else
            {
                Document.AppendChild(Document.CreateXmlDeclaration("1.0", "utf-16", "yes"));
                Document.AppendChild(Document.CreateElement("tags"));
                DataProvider.Document.Save(FullFilePath);
            }



            DataProvider.Source = new Uri(FullFilePath);
            DataProvider.XPath = _rootName;

            DataTags = new ObservableCollection<DataTag>();
            VirtualTags = new ObservableCollection<DataTag>();
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
            catch (Exception ex)
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

            DataTags.Clear();
            foreach (DataTag dt in TagCollection.DataTags)
                DataTags.Add(dt);

            VirtualTags.Clear();
            foreach (DataTag dt in TagCollection.VirtualTags)
                VirtualTags.Add(dt);

            TagCollection.ClearTags();

            Document.Save(FullFilePath);
        }
    }
}
