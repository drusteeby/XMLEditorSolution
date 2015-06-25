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

namespace XMLEditor.CurrentAlarmsView
{
    public class ViewModel: DependencyObject
    {
        public DelegateCommand goBack { get; set; }
        public DelegateCommand SelectionChanged { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }

        [Dependency]
        public IRegionManager regionManager { get; set; }


        public ViewModel()
        {
            goBack = new DelegateCommand(OnGoBack);
            SelectionChanged = new DelegateCommand(OnSelectionChanged);

            AlarmEnumerations = new ObservableCollection<DataTag>(TagCollection.DataTags.Where(dt => (dt.DataType == MCM.Core.Enum.DataType.Bits || dt.DataType == MCM.Core.Enum.DataType.Enum)));
            EnumerationValues = new ObservableCollection<EnumerationContainer>();
        }

        public void OnGoBack()
        {
           var view = regionManager.Regions["WindowRegion"].Views.Single(x => x.GetType() == typeof(CurrentAlarmsView.View));
           regionManager.Regions["WindowRegion"].Remove(view);

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

        
        
        

        public ObservableCollection<DataTag> AlarmEnumerations
        {
            get { return (ObservableCollection<DataTag>)GetValue(AlarmEnumerationsProperty); }
            set { SetValue(AlarmEnumerationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmEnumerations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmEnumerationsProperty =
            DependencyProperty.Register("AlarmEnumerations", typeof(ObservableCollection<DataTag>), typeof(ViewModel), new UIPropertyMetadata(null));

        
        

    }



}
