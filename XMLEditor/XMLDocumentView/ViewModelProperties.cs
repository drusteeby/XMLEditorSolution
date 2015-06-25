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

namespace XMLEditor.XMLDocumentView
{
    public partial class ViewModel
    {
        private Model _model;

        [Dependency]
        public IUnityContainer container { get; set; }

        [Dependency]
        public IRegionManager regionManager { get; set; }

        protected void initProperties()
        {
            _model = new Model();
            XMLDocumentViewContainers = new ObservableCollection<ViewContainer>();

            _model.LoadXmlDataProviders();

            SelectedIndex = 1;
            
            
            foreach(XmlDataProvider provider in _model.XMLDataProviderList)
            {
                XMLDocumentViewContainers.Add(new ViewContainer(provider));                
            }

            if (XMLDocumentViewContainers.Count > 0) SelectedDocument = XMLDocumentViewContainers.First();

        }




        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ViewModel), new UIPropertyMetadata(null));

        

       

        public XmlNode SelectedNode
        {
            get { return (XmlNode)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register("SelectedNode", typeof(XmlNode), typeof(ViewModel), new UIPropertyMetadata(null));




        public ViewContainer SelectedDocument
        {
            get { return (ViewContainer)GetValue(SelectedDocumentProperty); }
            set { SetValue(SelectedDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDocumentProperty =
            DependencyProperty.Register("SelectedDocument", typeof(ViewContainer), typeof(ViewModel), new UIPropertyMetadata(null));



        public ObservableCollection<ViewContainer> XMLDocumentViewContainers
        {
            get { return (ObservableCollection<ViewContainer>)GetValue(XMLDocumentViewContainersProperty); }
            set { SetValue(XMLDocumentViewContainersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XMLDocumentViewContainers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XMLDocumentViewContainersProperty =
            DependencyProperty.Register("XMLDocumentViewContainers", typeof(ObservableCollection<ViewContainer>), typeof(ViewModel), new UIPropertyMetadata(null));

        

        
    }
}
