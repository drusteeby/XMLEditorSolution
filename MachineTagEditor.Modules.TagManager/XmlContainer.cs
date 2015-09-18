using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using MachineTagEditor.Infrastructure.Extensions.XML;

namespace MachineTagEditor.Modules.TagManager
{
    public partial class XmlContainer : DependencyObject
    {
        public ObservableCollection<XmlNode> XMLNodes { get; set; }
        XmlDataProvider xmlDataProvider { get; set; }

        XmlNode _root;

        public string FullFilePath
        {
            get { return xmlDataProvider.Source.LocalPath; }
        }


        public XmlContainer(string filePath, string XPath = null)
        {
            XMLNodes = new ObservableCollection<XmlNode>();
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.Document = new XmlDocument();
            xmlDataProvider.Document.Load(filePath);
            xmlDataProvider.XPath = XPath;

            _root = xmlDataProvider.Document.GetElementsByTagName("tags")[0];

            foreach (XmlNode node in _root.ChildNodes)
                XMLNodes.Add(node);

            HasUnsavedChanges = false;
        }

        public void AddNode(string name, Dictionary<string, string> attributes = null)
        {
            XmlNode node = xmlDataProvider.Document.CreateElement(name);

            if (attributes != null)
                foreach (string key in attributes.Keys)
                    node.AddOrAppendAttribute(key, true, attributes[key]);

            _root.AppendChild(node);
            XMLNodes.Add(node);

            HasUnsavedChanges = true;
        }

        public void RemoveNode(string name)
        {
            XmlNodeList nodeList = xmlDataProvider.Document.GetElementsByTagName("tags")[0].ChildNodes;

            IQueryable<XmlNode> queryList = (IQueryable<XmlNode>)nodeList.AsQueryable();
            XmlNode toRemove = queryList.Single((x) => x.Name == name);

            XMLNodes.Remove(toRemove);
            _root.RemoveChild(toRemove);

            HasUnsavedChanges = true;
        } 

        public void RemoveNode(XmlNode node)
        {
            XMLNodes.Remove(node);
            _root.RemoveChild(node);

            HasUnsavedChanges = true;
        }

        public void RemoveSelected()
        {
            if(SelectedNode != null)
            {
                _root.RemoveChild(SelectedNode);
                XMLNodes.Remove(SelectedNode);
            }
            HasUnsavedChanges = true;
        }

        public bool SaveAs(string fullPath)
        {
            try
            {
                xmlDataProvider.Document.Save(fullPath);                
            }
            catch
            {     
                //add message here
                return false;
            }
            HasUnsavedChanges = false;
            return true;
        }

        public bool SaveAs(string filePath, string fileName)
        {
            return SaveAs(filePath + fileName);
        }

        public bool Save()
        {
            return SaveAs(new Uri(xmlDataProvider.Document.BaseURI).LocalPath);
        }



        public XmlNode SelectedNode
        {
            get { return (XmlNode)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register("SelectedNode", typeof(XmlNode), typeof(XmlContainer), new UIPropertyMetadata(null));



        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(XmlContainer), new UIPropertyMetadata(null));

        
        void updateHeader(bool unsavedChanges)
        {
            if (xmlDataProvider.Source != null)
                Header = xmlDataProvider.Source.LocalPath.Split('\\').Last() + (unsavedChanges ? "*" : " ");
            else if (xmlDataProvider.Document != null)
                Header = xmlDataProvider.Document.BaseURI.Split('/').Last() + (unsavedChanges ? "*" : " "); 
            else 
                Header = "Source and Document Null!";
        }


        // Using a DependencyProperty as the backing store for HasUnsavedChanges.  This enables animation, styling, binding, etc...
        public static readonly DependencyPropertyKey HasUnsavedChangesPropertyKey =
            DependencyProperty.RegisterReadOnly("HasUnsavedChanges", typeof(bool), typeof(XmlContainer), new UIPropertyMetadata(true,new PropertyChangedCallback(OnHasUnsavedChangesChanged)));

        private static void OnHasUnsavedChangesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XmlContainer _container = (XmlContainer)d;
            bool newValue = (bool)e.NewValue;

            if (_container.xmlDataProvider.Source != null)
                _container.Header = _container.xmlDataProvider.Source.LocalPath.Split('\\').Last() + (newValue ? "*" : " ");
            else if (_container.xmlDataProvider.Document != null)
                _container.Header = _container.xmlDataProvider.Document.BaseURI.Split('/').Last() + (newValue ? "*" : " ");
            else
                _container.Header = "Source and Document Null!";
 	       
        }

        public static readonly DependencyProperty HasUnsavedChangesProperty = HasUnsavedChangesPropertyKey.DependencyProperty;

        public bool HasUnsavedChanges
        {
            get { return (bool)GetValue(HasUnsavedChangesProperty); }
            protected set { SetValue(HasUnsavedChangesPropertyKey, value); }
        }
        

    }
}
