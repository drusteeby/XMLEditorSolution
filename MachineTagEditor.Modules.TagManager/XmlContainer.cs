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
using MachineTagEditor.Infrastructure.Interfaces;

namespace MachineTagEditor.Modules.TagManager
{
    public partial class XmlContainer : DependencyObject
    {        

        public ObservableCollection<XmlNodeContainer> XMLNodes
            {
                get { return (ObservableCollection<XmlNodeContainer>)GetValue(XMLNodesProperty); }
                set { SetValue(XMLNodesProperty, value); }
            }

            // Using a DependencyProperty as the backing store for XMLNodes.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty XMLNodesProperty =
                DependencyProperty.Register("XMLNodes", typeof(ObservableCollection<XmlNodeContainer>), typeof(XmlContainer), new UIPropertyMetadata(null));

        public XmlNodeList xmlNodes {
            get { return xmlDataProvider.Document.SelectNodes("tags / child::*"); }
        }

        XmlNode _root;
        XmlNode _toPaste;

        public string FullFilePath
        {
            get { return xmlDataProvider.Document.BaseURI; }
        }


        public XmlContainer(string filePath, string XPath = null)
        {
            XMLNodes = new ObservableCollection<XmlNodeContainer>();
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.Document = new XmlDocument();
            xmlDataProvider.Document.Load(filePath);
            xmlDataProvider.XPath = "tags/child::*";
            xmlDataProvider.InitialLoad();
            xmlDataProvider.DataChanged += XmlDataProvider_DataChanged;

            xmlDataProvider.Document.NodeChanged += Document_NodeModify;
            xmlDataProvider.Document.NodeInserted += Document_NodeModify;
            xmlDataProvider.Document.NodeRemoved += Document_NodeModify;

            foreach (XmlNode node in xmlDataProvider.Document.SelectNodes("tags / child::*"))
                XMLNodes.Add(new XmlNodeContainer(node));

            foreach (XmlNodeContainer container in XMLNodes)
            {
                container.copyCommandClicked += Container_copyCommandClicked;
                container.deleteCommandClicked += Container_deleteCommandClicked;
                container.pasteCommandClicked += Container_pasteCommandClicked;
            }

            HasUnsavedChanges = false;

            _root = xmlDataProvider.Document.SelectSingleNode("tags");
        }

        private void Container_pasteCommandClicked(object sender, string e)
        {
            var node = (sender as XmlNodeContainer).Node;
            if (node == null || _toPaste == null) return;

            if (e.ToLower().Contains("before"))
                _root.InsertBefore(_toPaste, node);
            else if (e.ToLower().Contains("after"))
                _root.InsertAfter(_toPaste, node);

            _toPaste = null;
            updateCanPaste();

        }

        private void Container_deleteCommandClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Container_copyCommandClicked(object sender, EventArgs e)
        {
            var node = (sender as XmlNodeContainer).Node;

            if (_toPaste == null || node == null) return;

            _toPaste = node;
            updateCanPaste();
        }


        private void updateCanPaste()
        {
            var _canPaste = (_toPaste == null) ? false : true;

            foreach (XmlNodeContainer container in XMLNodes)
            {
                container.canPaste = _canPaste;
                container.PasteCommand.RaiseCanExecuteChanged();
            }

        }

        private void XmlDataProvider_DataChanged(object sender, EventArgs e)
        {
            //XMLNodes.Clear();
            //foreach (XmlNode node in xmlDataProvider.Document.SelectNodes("tags / child::*"))
               // XMLNodes.Add(new XmlNodeContainer(node));
        }

        private void Document_NodeModify(object sender, XmlNodeChangedEventArgs e)
        {
            HasUnsavedChanges = true;

            if (e.Action == XmlNodeChangedAction.Remove && XMLNodes.Any((x) => x.Node == e.Node))
                XMLNodes.Remove(XMLNodes.Single((x) => x.Node == e.Node));
            else if (e.Action == XmlNodeChangedAction.Insert && !XMLNodes.Any((x) => x.Node == e.Node))
            {
                for (int index = 0; index < xmlNodes.Count; index++)
                {
                    if (xmlNodes[index] == e.Node)
                    {
                        XMLNodes.Insert(index, new XmlNodeContainer(e.Node));
                        break;
                    }
                }
            }  
            //else if (e.Action == XmlNodeChangedAction.Change)
            {
            //XMLNodes.Clear();
            //foreach (XmlNode node in xmlNodes)
                //XMLNodes.Add(new XmlNodeContainer(node));
            }


        }

        public void AddNode(XmlNode toAdd)
        {
            var attb = new Dictionary<string, string>();
            foreach (XmlAttribute a in toAdd.Attributes)
                attb.Add(a.Name, a.Value);

            AddNode(toAdd.Name, attb);
             
        }

        public void AddNode(string name, Dictionary<string, string> attributes = null)
        {
            XmlNode node = xmlDataProvider.Document.CreateElement(name);

            if (attributes != null)
                foreach (string key in attributes.Keys)
                    node.AddOrAppendAttribute(key, true, attributes[key]);

            _root.AppendChild(node);
            var nodeToAdd = new XmlNodeContainer(node);
            XMLNodes.Add(nodeToAdd);

            SelectedIndex = XMLNodes.IndexOf(nodeToAdd);
        }

        public void RemoveNode(string name)
        {
            XmlNodeList nodeList = xmlDataProvider.Document.GetElementsByTagName("tags")[0].ChildNodes;

            IQueryable<XmlNode> queryList = (IQueryable<XmlNode>)nodeList.AsQueryable();
            XmlNode toRemove = queryList.Single((x) => x.Name == name);

            //XMLNodes.Remove(XMLNodes.Single((x) => x.Node == toRemove));
            _root.RemoveChild(toRemove);
        } 

        public void RemoveNode(XmlNode node)
        {
            //XMLNodes.Remove(XMLNodes.Single((x) => x.Node == node));
            _root.RemoveChild(node);
        }

        public void RemoveSelected()
        {
            if(SelectedNode != null)
            {
                _root.RemoveChild(SelectedNode);
                //XMLNodes.Remove(XMLNodes.Single((x) => x.Node == SelectedNode));
            }
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




        public XmlDocument xmlDocument
        {
            get { return (XmlDocument)GetValue(xmlDocumentProperty); }
            set { SetValue(xmlDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for xmlDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xmlDocumentProperty =
            DependencyProperty.Register("xmlDocument", typeof(XmlDocument), typeof(XmlContainer), new UIPropertyMetadata(null));




        public XmlDataProvider xmlDataProvider
        {
            get { return (XmlDataProvider)GetValue(xmlDataProviderProperty); }
            set { SetValue(xmlDataProviderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for xmlDataProvider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xmlDataProviderProperty =
            DependencyProperty.Register("xmlDataProvider", typeof(XmlDataProvider), typeof(XmlContainer), new UIPropertyMetadata(null));




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



        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(XmlContainer), new UIPropertyMetadata(0));


    }
}
