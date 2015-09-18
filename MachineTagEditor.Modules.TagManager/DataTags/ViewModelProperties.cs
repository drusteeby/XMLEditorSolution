using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Extensions.XML;
using MCM.Core.Tags;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;


namespace MachineTagEditor.Modules.TagManager.DataTags
{
    public partial class ViewModel : DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        public TagManagerService Service { get; set; }

        void initProperties()
        {
            XmlFileList = new ObservableCollection<XmlContainer>();      
        }
        

        public ObservableCollection<XmlContainer> XmlFileList
        {
            get { return (ObservableCollection<XmlContainer>)GetValue(XmlFileListProperty); }
            set { SetValue(XmlFileListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XmlFileList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XmlFileListProperty =
            DependencyProperty.Register("XmlFileList", typeof(ObservableCollection<XmlContainer>), typeof(ViewModel), new UIPropertyMetadata(null));




        public XmlContainer SelectedFile
        {
            get { return (XmlContainer)GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register("SelectedFile", typeof(XmlContainer), typeof(ViewModel), new UIPropertyMetadata(null));




        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ViewModel), new UIPropertyMetadata(null));





        

        
        
    }
}
