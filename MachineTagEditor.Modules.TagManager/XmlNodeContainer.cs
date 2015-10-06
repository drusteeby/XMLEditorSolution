using MachineTagEditor.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager
{
    public class XmlNodeContainer: DependencyObject, IDropable, IDragable
    {


        public XmlNode Node
        {
            get { return (XmlNode)GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Node.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NodeProperty =
            DependencyProperty.Register("Node", typeof(XmlNode), typeof(XmlNodeContainer), new UIPropertyMetadata(null));



        public XmlNodeContainer(XmlNode node)
        {
            this.Node = node;
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
