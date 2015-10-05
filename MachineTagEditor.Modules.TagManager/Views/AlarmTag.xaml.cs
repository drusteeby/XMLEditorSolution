﻿using MachineTagEditor.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager.Views
{
    /// <summary>
    /// Interaction logic for AlarmTag.xaml
    /// </summary>
    public partial class AlarmTag : UserControl, IDragable, IDropable
    {
        public AlarmTag()
        {
            InitializeComponent();
        }

        public Type DataType
        {
            get
            {
                return typeof(AlarmTag);
            }
        }

        public void Remove(object i)
        {
            
        }

        void IDropable.Drop(object data, int index)
        {
            XmlNode dragged = (XmlNode)(data as AlarmTag).DataContext;

            if(dragged != null)
            {
                XmlNode curNode = this.DataContext as XmlNode;

                curNode.ParentNode.InsertBefore(dragged, curNode);
            }
        }
    }
}
