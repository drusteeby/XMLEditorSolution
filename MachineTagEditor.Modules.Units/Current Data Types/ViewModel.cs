using MachineTagEditor.Infrastructure.Events;
using MCM.Core.Tags;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;
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

namespace MachineTagEditor.Modules.Units.CurrentDataTypes
{
    public partial class ViewModel : DependencyObject
    {

        public IEventAggregator _eventAggregator;


        public ViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<TagsUpdated>().Subscribe(OnTagsUpdated);
            DataTypesList = new ObservableCollection<DataTag>();

            ParseTagCollection();
        }

        private void ParseTagCollection()
        {
            DataTypesList.Clear();

            foreach (DataTag tag in TagCollection.VirtualTags.Where(x => (x.Units + x.UnitsMetric + x.UnitsUs) != String.Empty))
                DataTypesList.Add(tag);

          
        }

        private void OnTagsUpdated(bool obj)
        {  
            ParseTagCollection();
        }
        


        public ObservableCollection<DataTag> DataTypesList
        {
            get { return (ObservableCollection<DataTag>)GetValue(DataTypesListProperty); }
            set { SetValue(DataTypesListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTypesList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypesListProperty =
            DependencyProperty.Register("DataTypesList", typeof(ObservableCollection<DataTag>), typeof(ViewModel), new UIPropertyMetadata(null));

        
    }
}
