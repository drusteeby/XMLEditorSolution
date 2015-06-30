using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;
using MCM.Core.Enum;
using MCM.Core.Objects;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.Alarms.AddAlarm
{
    public partial class ViewModel: DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        IEventAggregator _eventAggregator;
        IUnityContainer _container;
        IXMLService _service;

        void initProperties(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _container = container;
            _service = _container.Resolve<IXMLService>("AlarmsXMLService");

            EnumerationValues = new ObservableCollection<EnumerationContainer>();
            enumNames = new ObservableCollection<string>();
            enumNames.Add("New Item...");
            _service.VirtualTags.CollectionChanged += alarmVirtualTags_CollectionChanged;
            _service.reloadTags();
        }

        void alarmVirtualTags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (DataTag dt in e.NewItems)
                        if(((dt.DataType == DataType.Bits) || (dt.DataType == DataType.Enum)) && !enumNames.Contains(dt.Name))
                        {
                            enumNames.Add(dt.Name);
                        }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (DataTag dt in e.OldItems)
                        if (((dt.DataType == DataType.Bits) || (dt.DataType == DataType.Enum)) && enumNames.Contains(dt.Name))
                        {
                            enumNames.Remove(dt.Name);
                        }
                    break;
            }
        }


      
        public Visibility newVisibility
        {
            get { return (Visibility)GetValue(newVisibilityProperty); }
            set { SetValue(newVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newVisibilityProperty =
            DependencyProperty.Register("newVisibility", typeof(Visibility), typeof(ViewModel), new UIPropertyMetadata(Visibility.Collapsed));

        

        public ObservableCollection<string> enumNames
        {
            get { return (ObservableCollection<string>)GetValue(enumNamesProperty); }
            set { SetValue(enumNamesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for enumNames.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty enumNamesProperty =
            DependencyProperty.Register("enumNames", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));



        public string newEnumName
        {
            get { return (string)GetValue(newEnumNameProperty); }
            set { SetValue(newEnumNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newEnumName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newEnumNameProperty =
            DependencyProperty.Register("newEnumName", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));

        



        public string SelectedEnumName
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEnumName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedEnumName", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));

        
        public ObservableCollection<EnumerationContainer> EnumerationValues
        {
            get { return (ObservableCollection<EnumerationContainer>)GetValue(EnumerationValuesProperty); }
            set { SetValue(EnumerationValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnumerationValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnumerationValuesProperty =
            DependencyProperty.Register("EnumerationValues", typeof(ObservableCollection<EnumerationContainer>), typeof(ViewModel), new UIPropertyMetadata(null));




        public string AlarmName
        {
            get { return (string)GetValue(AlarmNameProperty); }
            set { SetValue(AlarmNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmNameProperty =
            DependencyProperty.Register("AlarmName", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));




        public string AlarmText
        {
            get { return (string)GetValue(AlarmTextProperty); }
            set { SetValue(AlarmTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmTextProperty =
            DependencyProperty.Register("AlarmText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));





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
            DependencyProperty.Register("AlarmReadFrom", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));




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
