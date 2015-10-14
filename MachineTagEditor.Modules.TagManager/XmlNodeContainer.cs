using MachineTagEditor.Infrastructure.Extensions.XML;
using MachineTagEditor.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager
{
    
    
    public class XmlNodeContainer: DependencyObject, IDropable, IDragable
    {

        public DelegateCommand AddAttributeCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand DeleteAttributeCommand { get; set; }



        public ObservableCollection<XmlAttribute> XmlAttributes
        {
            get { return (ObservableCollection<XmlAttribute>)GetValue(XmlAttributesProperty); }
            set { SetValue(XmlAttributesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XmlAttributes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XmlAttributesProperty =
            DependencyProperty.Register("XmlAttributes", typeof(ObservableCollection<XmlAttribute>), typeof(XmlNodeContainer), new UIPropertyMetadata(new PropertyChangedCallback(OnXmlAttributesChanged)));

        private static void OnXmlAttributesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XmlNodeContainer cur = (d as XmlNodeContainer);
        }





        public int selectedIndex
        {
            get { return (int)GetValue(selectedIndexProperty); }
            set { SetValue(selectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedIndexProperty =
            DependencyProperty.Register("selectedIndex", typeof(int), typeof(XmlNodeContainer), new UIPropertyMetadata(null));




        private static void selectedAttributeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as XmlNodeContainer).DeleteAttributeCommand.RaiseCanExecuteChanged();
        }

        public Visibility NewAttributeVisibility
        {
            get { return (Visibility)GetValue(NewAttributeVisibilityProperty); }
            set { SetValue(NewAttributeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewAttributeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewAttributeVisibilityProperty =
            DependencyProperty.Register("NewAttributeVisibility", typeof(Visibility), typeof(XmlNodeContainer), new UIPropertyMetadata(null));



        public string NewAttributeName
        {
            get { return (string)GetValue(NewAttributeNameProperty); }
            set { SetValue(NewAttributeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewAttributeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewAttributeNameProperty =
            DependencyProperty.Register("NewAttributeName", typeof(string), typeof(XmlNodeContainer), new UIPropertyMetadata(null));



        public string NewAttributeValue
        {
            get { return (string)GetValue(NewAttributeValueProperty); }
            set { SetValue(NewAttributeValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewAttributeValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewAttributeValueProperty =
            DependencyProperty.Register("NewAttributeValue", typeof(string), typeof(XmlNodeContainer), new UIPropertyMetadata(null));




        public XmlNode Node
        {
            get { return (XmlNode)GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Node.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NodeProperty =
            DependencyProperty.Register("Node", typeof(XmlNode), typeof(XmlNodeContainer), new UIPropertyMetadata(null, new PropertyChangedCallback(OnNodeChanged)));

        private static void OnNodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {        
            XmlNodeContainer cur = (d as XmlNodeContainer);
            cur.XmlAttributes.Clear();
            foreach (XmlAttribute attribute in cur.Node.Attributes)
                cur.XmlAttributes.Add(attribute);
        }

        public XmlNodeContainer()
        {            
        }

        private void OnAddAttributeCommand()
        {
            if (NewAttributeVisibility != Visibility.Visible)
                NewAttributeVisibility = Visibility.Visible;
            else
                NewAttributeVisibility = Visibility.Collapsed;

            NewAttributeName = String.Empty;
            NewAttributeValue = String.Empty;
        }


        public XmlNodeContainer(XmlNode node):base()
        {
            XmlAttributes = new ObservableCollection<XmlAttribute>();
            XmlAttributes.CollectionChanged += XmlAttributes_CollectionChanged;
            this.Node = node;

            AddAttributeCommand = new DelegateCommand(OnAddAttributeCommand);
            AddCommand = new DelegateCommand(OnAddCommand);
            DeleteAttributeCommand = new DelegateCommand(OnDeleteAttribute, CanDeleteAttribute);

            NewAttributeVisibility = Visibility.Collapsed;


        }

        private bool CanDeleteAttribute()
        {
            return true;
        }

        private void OnDeleteAttribute()
        {
            XmlAttributes.RemoveAt(selectedIndex);
        }

        private void XmlAttributes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    XmlAttribute oldvalue = (XmlAttribute)item;
                    if (oldvalue != null)
                        Node.Attributes.Remove(oldvalue);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    XmlAttribute newvalue = (XmlAttribute)item;
                    Node.AddOrAppendAttribute(newvalue.Name, false, newvalue.Value);
                }
            }



        }

        private void XmlNodeContainer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnAddCommand()
        {
            if (NewAttributeName == null || NewAttributeValue == null) return;

            XmlAttribute toAdd = Node.OwnerDocument.CreateAttribute(NewAttributeName);
            toAdd.Value = NewAttributeValue;
            XmlAttributes.Add(toAdd);
            NewAttributeVisibility = Visibility.Collapsed;

            NewAttributeName = String.Empty;
            NewAttributeValue = String.Empty;
        }

        public Type DataType
        {
            get
            {
                return typeof(XmlNodeContainer);
            }
        }

        public void Remove(object i)
        {

        }

        void IDropable.Drop(object data, int index)
        {
            XmlNode dragged = (XmlNode)(data as XmlNodeContainer).Node;

            if (dragged != null)
            {
                Node.ParentNode.InsertBefore(dragged, Node);
            }
        }
    }
}
