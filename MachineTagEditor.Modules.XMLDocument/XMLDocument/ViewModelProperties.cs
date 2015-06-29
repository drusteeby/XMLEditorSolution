using MachineTagEditor.Infrastructure.Events;
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
using System.Windows.Documents;
using System.Xml;

namespace MachineTagEditor.Modules.XMLDocument
{
    public partial class ViewModel
    {
        private Model _model;

        [Dependency]
        public IUnityContainer container { get; set; }

        [Dependency]
        public IRegionManager regionManager { get; set; }


        IEventAggregator _eventAggregator; 

        protected void initProperties(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _model = new Model();
            XMLDocumentViewContainers = new ObservableCollection<ViewContainer>();
            TreeTemplatesList = new ObservableCollection<string>();
            TreeTemplatesList.Add("Metal");
            TreeTemplatesList.Add("Blue");

            _model.LoadXmlDataProviders();            
            
            foreach(XmlDataProvider provider in _model.XMLDataProviderList)
            {
                XMLDocumentViewContainers.Add(new ViewContainer(provider));                
            }

            if (XMLDocumentViewContainers.Count > 0) SelectedDocument = XMLDocumentViewContainers.First();

            _eventAggregator.GetEvent<TagsUpdated>().Publish(true);
            
        }



        public ObservableCollection<string> TreeTemplatesList
        {
            get { return (ObservableCollection<string>)GetValue(TreeTemplatesListProperty); }
            set { SetValue(TreeTemplatesListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TreeTemplatesList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TreeTemplatesListProperty =
            DependencyProperty.Register("TreeTemplatesList", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));

        



        public string TreeViewItemTemplate
        {
            get { return (string)GetValue(TreeViewItemTemplateProperty); }
            set { SetValue(TreeViewItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TreeViewItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TreeViewItemTemplateProperty =
            DependencyProperty.Register("TreeViewItemTemplate", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));

               

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




        public string selectHelpView
        {
            get { return (string)GetValue(selectHelpViewProperty); }
            set { SetValue(selectHelpViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectHelpView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectHelpViewProperty =
            DependencyProperty.Register("selectHelpView", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));





        public FlowDocument EditableFlowDocument
        {
            get { return (FlowDocument)GetValue(EditableFlowDocumentProperty); }
            set { SetValue(EditableFlowDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditableFlowDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditableFlowDocumentProperty =
            DependencyProperty.Register("EditableFlowDocument", typeof(FlowDocument), typeof(ViewModel), new UIPropertyMetadata(null));

        
        
    }
}
