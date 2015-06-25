using MCM.Core.Objects;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XMLEditor.DataTagsView
{
    public partial class ViewModel : DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }
        void initProperties()
        {
            DataTagPropertyContainerList = new ObservableCollection<DataTagPropertyContainer>();
            DataTags = new ObservableCollection<DataTag>(TagCollection.DataTags);
        }

        public ObservableCollection<DataTagPropertyContainer> DataTagPropertyContainerList
        {
            get { return (ObservableCollection<DataTagPropertyContainer>)GetValue(DataTagContainerListProperty); }
            set { SetValue(DataTagContainerListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTagPropertyContainerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTagContainerListProperty =
            DependencyProperty.Register("DataTagPropertyContainerList", typeof(ObservableCollection<DataTagPropertyContainer>), typeof(ViewModel), new UIPropertyMetadata(null));




        public ObservableCollection<DataTag> DataTags
        {
            get { return (ObservableCollection<DataTag>)GetValue(DataTagsProperty); }
            set { SetValue(DataTagsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTags.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTagsProperty =
            DependencyProperty.Register("DataTags", typeof(ObservableCollection<DataTag>), typeof(ViewModel), new UIPropertyMetadata(null));



        public DataTag SelectedDataTag
        {
            get { return (DataTag)GetValue(SelectedDataTagProperty); }
            set { SetValue(SelectedDataTagProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDataTag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDataTagProperty =
            DependencyProperty.Register("SelectedDataTag", typeof(DataTag), typeof(ViewModel), new UIPropertyMetadata(null));

        

        
    }
}
