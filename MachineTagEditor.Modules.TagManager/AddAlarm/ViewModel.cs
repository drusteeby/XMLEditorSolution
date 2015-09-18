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

namespace MachineTagEditor.Modules.TagManager.AddAlarm
{
    public partial class ViewModel : DependencyObject
    {
        public TagManagerService TagService { get; set; }
        public ViewModel(TagManagerService _tm)
        {
            TagService = _tm;
            ParentsList = new ObservableCollection<string>();
            EnumerationsList = new ObservableCollection<string>();
            MessageList = new ObservableCollection<string>();

            foreach (XmlNode node in TagService.AlarmsList)
                ParentsList.Add(node.Attributes["name"].Value);

            foreach (XmlNode node in TagService.EnumerationsList)
                EnumerationsList.Add(node.Attributes["name"].Value);
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
            _vm.MessageList.Clear();

            XmlNode node = _vm.TagService.GetNodeByNameAttribute((string)e.NewValue);

            foreach (XmlNode child in node.ChildNodes)
                _vm.MessageList.Add("Value: " + child.Attributes["value"].Value + " Message: " + child.Attributes["text"].Value);

        }



        public ObservableCollection<string> MessageList
        {
            get { return (ObservableCollection<string>)GetValue(MessageListProperty); }
            set { SetValue(MessageListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageListProperty =
            DependencyProperty.Register("MessageList", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));


    }
}
