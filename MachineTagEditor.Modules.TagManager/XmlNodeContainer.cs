﻿using MachineTagEditor.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
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
        public DelegateCommand CopyCommand { get; set; }
        public DelegateCommand<string> PasteCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public bool canPaste { get; set; }

        public event EventHandler copyCommandClicked;
        public event EventHandler<string> pasteCommandClicked;
        public event EventHandler deleteCommandClicked;

        public XmlNode Node
        {
            get { return (XmlNode)GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Node.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NodeProperty =
            DependencyProperty.Register("Node", typeof(XmlNode), typeof(XmlNodeContainer), new UIPropertyMetadata(null));


        public XmlNodeContainer()
        {
            CopyCommand = new DelegateCommand(OnCopyCommand);
            PasteCommand = new DelegateCommand<string>(OnPasteCommand, CanPasteCommand);
            DeleteCommand = new DelegateCommand(OnDeleteCommand);
        }

        private void OnDeleteCommand()
        {
            throw new NotImplementedException();
        }

        private bool CanPasteCommand(string pos)
        {
            return canPaste;
        }

        private void OnPasteCommand(string pos)
        {
            pasteCommandClicked(this, pos);
        }

        private void OnCopyCommand()
        {
            copyCommandClicked(this, new EventArgs());
        }

        public XmlNodeContainer(XmlNode node):base()
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
