using MCM.Controls.Composite;
using MCM.Core.Controls;
using MCM.Core.Enum;
using MCM.Core;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MCM.Core.Objects;
using System.Reflection;
using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.PubSubEvents;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;

namespace MachineTagEditor.Modules.Alarms.CurrentAlarms
{
    public class ViewModel: DependencyObject
    {
        public DelegateCommand SelectionChanged { get; set; }

        IUnityContainer _container;
        IXMLService _service;

        [Dependency]
        public IRegionManager regionManager { get; set; }




        public ViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            SelectionChanged = new DelegateCommand(OnSelectionChanged);

            _service = _container.Resolve<IXMLService>("AlarmsXMLService");
            _service.DataTags.CollectionChanged+=alarmDataTags_CollectionChanged;
            _service.reloadTags();
            
            EnumerationValues = new ObservableCollection<EnumerationContainer>();

            
        }

        void alarmDataTags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ObservableCollection<DataTag>))
                AlarmCollection = new ObservableCollection<DataTag>((sender as ObservableCollection<DataTag>).Where(dt => dt.Group.Contains("Alarms")));
            
        }


        public void OnSelectionChanged()
        {
            TagEnum te = TagHelper.getEnumfromTag(SelectedAlarm);
            EnumerationValues.Clear();

            if (te != null)
                EnumerationValues = new ObservableCollection<EnumerationContainer>(TagHelper.getContainersfromEnum(te));
           
        }

        public DataTag SelectedAlarm
        {
            get { return (DataTag)GetValue(SelectedAlarmProperty); }
            set { SetValue(SelectedAlarmProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedAlarm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedAlarmProperty =
            DependencyProperty.Register("SelectedAlarm", typeof(DataTag), typeof(ViewModel), new UIPropertyMetadata(null));


        public ObservableCollection<EnumerationContainer> EnumerationValues
        {
            get { return (ObservableCollection<EnumerationContainer>)GetValue(EnumerationValuesProperty); }
            set { SetValue(EnumerationValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnumerationValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnumerationValuesProperty =
            DependencyProperty.Register("EnumerationValues", typeof(ObservableCollection<EnumerationContainer>), typeof(ViewModel), new UIPropertyMetadata(null));

       
        

        public ObservableCollection<DataTag> AlarmCollection
        {
            get { return (ObservableCollection<DataTag>)GetValue(AlarmEnumerationsProperty); }
            set { SetValue(AlarmEnumerationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmEnumerationsProperty =
            DependencyProperty.Register("AlarmCollection", typeof(ObservableCollection<DataTag>), typeof(ViewModel), new UIPropertyMetadata(null));

        
        

    }



}
