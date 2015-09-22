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
using System.Xml;
using MachineTagEditor.Infrastructure.Extensions.XML;
using Microsoft.Practices.Prism.Commands;
using MachineTagEditor.Infrastructure.Extensions;

namespace MachineTagEditor.Modules.TagManager.AddAlarm
{
    public partial class ViewModel : DependencyObject
    {
        public TagManagerService TagService { get; set; }
        public DelegateCommand AddAlarm { get; set; }
        public ViewModel(TagManagerService _tm)
        {
            TagService = _tm;
            TagService.XmlFileList.CollectionChanged += XmlFileList_CollectionChanged;

            ParentsList = new ObservableCollection<string>();
            EnumerationsList = new ObservableCollection<string>();
            MessageList = new ObservableCollection<string>();
            XmlFileList = new ObservableCollection<XmlContainer>();

            AddAlarm = new DelegateCommand(OnAddAlarm,CanAddAlarm);


            XmlFileList = TagService.XmlFileList;

            foreach (XmlNode node in TagService.AlarmsList)
                ParentsList.Add(node.Attributes["name"].Value);

            foreach (XmlNode node in TagService.EnumerationsList)
                EnumerationsList.Add(node.Attributes["name"].Value);
        }

        private bool CanAddAlarm()
        {
            return !String.IsNullOrEmpty(AlarmName) && !String.IsNullOrEmpty(AlarmReadFrom);           
        }

        private void OnAddAlarm()
        {

            if (!TagService.XmlFileList.Contains(SelectedFile))
            {
                MessageBox.Show(SelectedFile + " file not found!");
                return;
            }

            Dictionary<string, string> attrList = new Dictionary<string, string>();


            if (ParentNode != null)
            {
                foreach (XmlAttribute attr in ParentNode.Attributes)
                    attrList.AddOrUpdate(attr.Name, attr.Value);

                attrList.AddOrUpdate("parent", ParentNode.AttributeValue("name"));
            }



            attrList.AddOrUpdate("name", AlarmName);
            attrList.AddOrUpdate("readFrom", AlarmReadFrom);
            attrList.AddOrUpdate("group", "Alarms");

            if (!String.IsNullOrEmpty(AlarmReadName)) attrList.AddOrUpdate("readName", AlarmReadName);
            if (!String.IsNullOrEmpty(AlarmText)) attrList.AddOrUpdate("text", AlarmText);
            if (!String.IsNullOrEmpty(AlarmPage)) attrList.AddOrUpdate("page", AlarmPage);
            if (!String.IsNullOrEmpty(AlarmPrepend)) attrList.AddOrUpdate("prepend", AlarmPrepend);

            TagService.AddNodeToFile(SelectedFile, "tag", attrList);

            Window curWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            curWindow.Close();
        }

        public ObservableCollection<string> ParentsList
        {
            get { return (ObservableCollection<string>)GetValue(ParentsListProperty); }
            set { SetValue(ParentsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParentsListProperty =
            DependencyProperty.Register("ParentsList", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));  
 




        public ObservableCollection<string> EnumerationsList
        {
            get { return (ObservableCollection<string>)GetValue(EnumerationsListProperty); }
            set { SetValue(EnumerationsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnumerationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnumerationsListProperty =
            DependencyProperty.Register("EnumerationsList", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));





        public string SelectedEnumeration
        {
            get { return (string)GetValue(SelectedEnumerationProperty); }
            set { SetValue(SelectedEnumerationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEnumeration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEnumerationProperty =
            DependencyProperty.Register("SelectedEnumeration", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null,new PropertyChangedCallback(OnSelectedEnumerationChanged)));


        private static void OnSelectedEnumerationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ViewModel _vm = (ViewModel)d;
            string newValue = (string)e.NewValue;
            _vm.MessageList.Clear();

            XmlNode node = _vm.TagService.GetNodeByNameAttribute((string)e.NewValue);

            foreach (XmlNode child in node.ChildNodes)
                _vm.MessageList.Add("Value: " + child.Attributes["value"].Value + " Message: " + child.Attributes["text"].Value);


            if (!newValue.ToLower().Contains("none"))
            {
                _vm.ClearValue(ParentNodeProperty);

                try { _vm.ParentNode = _vm.TagService.GetNodeByNameAttribute(newValue); }
                catch { MessageBox.Show("Couldn't find parent " + newValue); }
            }

        }



        public ObservableCollection<string> MessageList
        {
            get { return (ObservableCollection<string>)GetValue(MessageListProperty); }
            set { SetValue(MessageListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageListProperty =
            DependencyProperty.Register("MessageList", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));

        public ObservableCollection<XmlContainer> XmlFileList
        {
            get { return (ObservableCollection<XmlContainer>)GetValue(XmlFileListProperty); }
            set { SetValue(XmlFileListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XmlFileList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XmlFileListProperty =
            DependencyProperty.Register("XmlFileList", typeof(ObservableCollection<XmlContainer>), typeof(ViewModel), new UIPropertyMetadata(null));

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
            DependencyProperty.Register("SelectedFile", typeof(XmlContainer), typeof(ViewModel), new UIPropertyMetadata(null));

        public XmlNode ParentNode
        {
            get { return (XmlNode)GetValue(ParentNodeProperty); }
            set { SetValue(ParentNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParentNodeProperty =
            DependencyProperty.Register("ParentNode", typeof(XmlNode), typeof(ViewModel), new UIPropertyMetadata(null));





        public string AlarmName
        {
            get { return (string)GetValue(AlarmNameProperty); }
            set { SetValue(AlarmNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmNameProperty =
            DependencyProperty.Register("AlarmName", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null, new PropertyChangedCallback(OnAlarmNameChanged)));

        private static void OnAlarmNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ViewModel _vm = (ViewModel)d;
            _vm.AddAlarm.RaiseCanExecuteChanged();
        }

        public string AlarmText
        {
            get { return (string)GetValue(AlarmTextProperty); }
            set { SetValue(AlarmTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmTextProperty =
            DependencyProperty.Register("AlarmText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null, new PropertyChangedCallback(OnAlarmTextChanged)));

        private static void OnAlarmTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ViewModel _vm = (ViewModel)d;
            _vm.AddAlarm.RaiseCanExecuteChanged();
        }

        public string AlarmPrepend
        {
            get { return (string)GetValue(AlarmPrependProperty); }
            set { SetValue(AlarmPrependProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmPrepend.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmPrependProperty =
            DependencyProperty.Register("AlarmPrepend", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));




        public string AlarmReadFrom
        {
            get { return (string)GetValue(AlarmReadFromProperty); }
            set { SetValue(AlarmReadFromProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmReadFrom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmReadFromProperty =
            DependencyProperty.Register("AlarmReadFrom", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null, new PropertyChangedCallback(OnAlarmReadFromChanged)));

        private static void OnAlarmReadFromChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ViewModel _vm = (ViewModel)d;
            _vm.AddAlarm.RaiseCanExecuteChanged();
        }

        public string AlarmReadName
        {
            get { return (string)GetValue(AlarmReadNameProperty); }
            set { SetValue(AlarmReadNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmReadName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmReadNameProperty =
            DependencyProperty.Register("AlarmReadName", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));




        public string AlarmPage
        {
            get { return (string)GetValue(AlarmPageProperty); }
            set { SetValue(AlarmPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmPageProperty =
            DependencyProperty.Register("AlarmPage", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));


    }
}
