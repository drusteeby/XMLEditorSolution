using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager
{
    public class AddTagViewModelBase: DependencyObject
    {
        public TagManagerService TagService { get; set; }
        public DelegateCommand Add { get; set; }

        public AddTagViewModelBase(TagManagerService _tm)
        {
            TagService = _tm;
            TagService.XmlFileList.CollectionChanged += XmlFileList_CollectionChanged;

            ParentsList = new ObservableCollection<string>();
            XmlFileList = new ObservableCollection<XmlContainer>();
            XmlFileList = TagService.XmlFileList;       
        }

        public void CloseWindow()
        {
            Window curWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            curWindow.Close();
        }

        public ObservableCollection<XmlContainer> XmlFileList
        {
            get { return (ObservableCollection<XmlContainer>)GetValue(XmlFileListProperty); }
            set { SetValue(XmlFileListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XmlFileList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XmlFileListProperty =
            DependencyProperty.Register("XmlFileList", typeof(ObservableCollection<XmlContainer>), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));

        void XmlFileList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            XmlFileList = TagService.XmlFileList;
        }


        public XmlContainer SelectedFile
        {
            get { return (XmlContainer)GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register("SelectedFile", typeof(XmlContainer), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));

        public XmlNode ParentNode
        {
            get { return (XmlNode)GetValue(ParentNodeProperty); }
            set { SetValue(ParentNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParentNodeProperty =
            DependencyProperty.Register("ParentNode", typeof(XmlNode), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));

        public string SelectedParent
        {
            get { return (string)GetValue(SelectedParentProperty); }
            set { SetValue(SelectedParentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEnumeration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedParentProperty =
            DependencyProperty.Register("SelectedParent", typeof(string), typeof(AddTagViewModelBase), new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedParentChanged)));


        private static void OnSelectedParentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddTagViewModelBase _vm = (AddTagViewModelBase)d;
            string newValue = (string)e.NewValue;

            XmlNode node = _vm.TagService.GetNodeByNameAttribute((string)e.NewValue);

            if (!newValue.ToLower().Contains("none"))
            {
                _vm.ClearValue(ParentNodeProperty);

                try { _vm.ParentNode = _vm.TagService.GetNodeByNameAttribute(newValue); }
                catch { MessageBox.Show("Couldn't find parent " + newValue); }
            }

        }

        public ObservableCollection<string> ParentsList
        {
            get { return (ObservableCollection<string>)GetValue(ParentsListProperty); }
            set { SetValue(ParentsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParentsListProperty =
            DependencyProperty.Register("ParentsList", typeof(ObservableCollection<string>), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));



        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));




        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));




        public string Group
        {
            get { return (string)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Group.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupProperty =
            DependencyProperty.Register("Group", typeof(string), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));




        public string Page
        {
            get { return (string)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Page.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(string), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));




        public string Min
        {
            get { return (string)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(string), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));




        public string Max
        {
            get { return (string)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(string), typeof(AddTagViewModelBase), new UIPropertyMetadata(null));



    }
}
