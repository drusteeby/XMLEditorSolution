using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Common.Controls.Wizard
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }
        public DelegateCommand GoBack { get; set; }
        public DelegateCommand GoForward { get; set; }

        public View()
        {
            InitializeComponent();
            pageListing = new ObservableCollection<string>();
            GoBack = new DelegateCommand(OnGoBack, CanGoBack);
            GoForward = new DelegateCommand(OnGoForward, CanGoForward);
            this.DataContext = this;
        }
        
        int pageIndex = 0;

        void Navigate()
        {
            regionManager.RequestNavigate("PageRegion", pageListing.ElementAt(pageIndex));
            GoBack.RaiseCanExecuteChanged();
            GoForward.RaiseCanExecuteChanged();
        }

        public void OnGoBack()
        {
            pageIndex--;
            Navigate();
        }

        public bool CanGoBack()
        {
            return (pageIndex > 0);
        }

        public void OnGoForward()
        {
            pageIndex++;
            Navigate();
        }

        public bool CanGoForward()
        {
            return (pageIndex < pageListing.Count - 1);
        }


        public ObservableCollection<string> pageListing
        {
            get { return (ObservableCollection<string>)GetValue(pageListingProperty); }
            set { SetValue(pageListingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for pageListing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty pageListingProperty =
            DependencyProperty.Register("pageListing", typeof(ObservableCollection<string>), typeof(View), new UIPropertyMetadata(null));


        
    }
}
