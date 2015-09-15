using MachineTagEditor.Infrastructure.Containers;
using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager
{
    public class TagManagerService
    {

        public Dictionary<string, Dictionary<string,DataTagContainer>> AllTags { get; set; }
        public Dictionary<string, XmlNode> AllTagsXML { get; set; }
        public Dictionary<string, XmlDataProvider> AllXMLFiles { get; set; }
        public ObservableCollection<XmlContainer> XmlFileList;

        public TagManagerService()
        {
            AllTagsXML = new Dictionary<string, XmlNode>();
            AllXMLFiles = new Dictionary<string, XmlDataProvider>();
            AllTags = new Dictionary<string, Dictionary<string, DataTagContainer>>();
            XmlFileList = new ObservableCollection<XmlContainer>();
        }

        public bool AddGroup(string name)
        {
            if(AllTags.ContainsKey(name))
                return false;

            AllTags.Add(name, new Dictionary<string, DataTagContainer>());

            var filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)  +name + ".xml";
            var provider = new XmlDataProvider();

            if (AllXMLFiles.ContainsKey(filePath))
                return false;

            provider.Source = new Uri(filePath);

            AllXMLFiles.Add(filePath, provider);

            return true;
        }

        public bool AddTagToGroup(string groupName,DataTagContainer tag)
        {
            if (!AllTags.ContainsKey(groupName))
                return false;
            if (AllTags[groupName].ContainsKey(tag.Name))
                return false;

            AllTags[groupName].Add(tag.Name, tag);

            return true;
        }

        public bool LoadFromXML(string path)
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.Document = new XmlDocument();
            _dataProvider.XPath = "tags";

            try{ _dataProvider.Document.Load(path); }
            catch{ return false; }


            //Creating the container and loading the XML file
            XmlContainer toAdd = new XmlContainer();

            toAdd.xmlDataProvider = _dataProvider;

            //Adding every node to a node list
            foreach (XmlNode node in toAdd.xmlDataProvider.Document.GetElementsByTagName("tags")[0].ChildNodes)
                toAdd.XMLNodes.Add(node);

            XmlFileList.Add(toAdd);

            return true;
            
        }

        private void ParseMod(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private void ParseTable(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private void ParseCopy(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private void ParseArray(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private void ParseTag(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private void ParseEnum(XmlNode node)
        {
            throw new NotImplementedException();
        }

      }

}



