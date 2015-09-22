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
using MachineTagEditor.Infrastructure.Extensions.XML;

namespace MachineTagEditor.Modules.TagManager
{
    public class TagManagerService
    {
        public ObservableCollection<XmlContainer> XmlFileList;

        public TagManagerService()
        {
            XmlFileList = new ObservableCollection<XmlContainer>();
        }

        public List<XmlNode> AllTagsXML
        {
            get
            {
                List<XmlNode> result = new List<XmlNode>();
                foreach (XmlContainer container in XmlFileList)
                    result.AddRange(container.XMLNodes);

                return result;
            }            
        }

        public List<XmlNode> AlarmsList
        {
            get
            {
                List<XmlNode> result = new List<XmlNode>();
                foreach (XmlContainer container in XmlFileList)
                    foreach(XmlNode node in container.XMLNodes.Where((x) => x.IsAlarm()))
                        result.Add(node);

                return result;
            }
        }

        public List<XmlNode> WarningsList
        {
            get
            {
                List<XmlNode> result = new List<XmlNode>();
                foreach (XmlContainer container in XmlFileList)
                    foreach (XmlNode node in container.XMLNodes.Where((x) => x.IsWarning()))
                        result.Add(node);

                return result;
            }
        }

        public List<XmlNode> DataTypesList
        {
            get
            {
                List<XmlNode> result = new List<XmlNode>();
                foreach (XmlContainer container in XmlFileList)
                    foreach (XmlNode node in container.XMLNodes.Where((x) => x.IsDataType()))
                        result.Add(node);

                return result;
            }
        }

        public List<XmlNode> EnumerationsList
        {
            get
            {
                List<XmlNode> result = new List<XmlNode>();
                foreach (XmlContainer container in XmlFileList)
                    foreach (XmlNode node in container.XMLNodes.Where((x) => x.IsEnumeration()))
                        result.Add(node);

                return result;
            }
        }

        public XmlNode GetNodeByNameAttribute(string name)
        {
            return AllTagsXML.SingleOrDefault((x) => x.ContainsAttributeWithExactValue("name", name));
        }


        public void AddNodeToFile(XmlContainer file, string name, Dictionary<string,string> attributes = null)
        {          
            file.AddNode(name, attributes);
        }

        public void RemoveNodeFromFile(string fileName, string nodeName)
        {
            XmlContainer file = FindFile(fileName);
            file.RemoveNode(nodeName);
        }
        public void RemoveNodeFromFile(string fileName, XmlNode node)
        {
            XmlContainer file = FindFile(fileName);
            file.RemoveNode(node);
        }

        private XmlContainer FindFile(string fileName)
        {
            XmlContainer file = XmlFileList.Single((x) => x.Header.Contains(fileName));
            return file;
        }


        public bool LoadFromXML(string path)
        {
            XmlFileList.Add(new XmlContainer(path));
            return true;            
        }       

      }

}



