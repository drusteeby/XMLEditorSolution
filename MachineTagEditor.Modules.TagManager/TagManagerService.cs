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

        public TagManagerService()
        {
            AllTagsXML = new Dictionary<string, XmlNode>();
            AllXMLFiles = new Dictionary<string, XmlDataProvider>();
            AllTags = new Dictionary<string, Dictionary<string, DataTagContainer>>();
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

        public XmlDataProvider LoadFromXML(string path)
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.Document = new XmlDocument();
            _dataProvider.XPath = "tags";

            try
            {
                _dataProvider.Document.Load(path);
                

                /*foreach (XmlNode node in _dataProvider.Document.ChildNodes)
                {
                    switch (node.Name.ToLower())
                    {
                        case "enum":
                        case "bits":
                            ParseEnum(node);
                            break;
                        case "virtual":
                        case "tag":
                            ParseTag(node);
                            break;
                        case "array":
                            ParseArray(node);
                            break;
                        case "copy":
                            ParseCopy(node);
                            break;
                        case "table":
                            ParseTable(node);
                            break;
                        case "mod":
                            ParseMod(node);
                            break;
                        default:
                            throw new XmlException("Unknown node " + node.Name + ".");
                    }
                }*/
            }

            catch
            {
                return null;
            }
            return _dataProvider;
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



