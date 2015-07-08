using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
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

namespace XMLEditor
{
    public class ShellViewModel : DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }

        public DelegateCommand GoBack { get; set; }
        public DelegateCommand GoForward { get; set; }

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            ErrorRegionVisibility = Visibility.Collapsed;
            ErrorRegionGridHeight = new GridLength(0, GridUnitType.Star);

            eventAggregator.GetEvent<SaveSetting>().Subscribe(OnSaveSetting);
            eventAggregator.GetEvent<ChangeWizardVisibility>().Subscribe(OnChangeWizardVisibility);

            GoBack = new DelegateCommand(OnGoBack, CanGoBack);
            GoForward = new DelegateCommand(OnGoForward, CanGoForward);
            pageListing = new ObservableCollection<string>();

        }

        int pageIndex = 0;

        void Navigate()
        {
            regionManager.RequestNavigate("PageOverlayRegion", pageListing.ElementAt(pageIndex));
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
            DependencyProperty.Register("pageListing", typeof(ObservableCollection<string>), typeof(ShellViewModel), new UIPropertyMetadata(null));

        public void OnSaveSetting(SaveSettingContainer container)
        {
           
        }

        public void OnChangeWizardVisibility(Visibility vis)
        {
            WizardVisibility = vis;
        }


        public Visibility WizardVisibility
        {
            get { return (Visibility)GetValue(WizardVisibilityProperty); }
            set { SetValue(WizardVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WizardVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WizardVisibilityProperty =
            DependencyProperty.Register("WizardVisibility", typeof(Visibility), typeof(ShellViewModel), new UIPropertyMetadata(Visibility.Collapsed));

        

        public Visibility ErrorRegionVisibility
        {
            get { return (Visibility)GetValue(ErrorRegionVisibilityProperty); }
            set { SetValue(ErrorRegionVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorRegionVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorRegionVisibilityProperty =
            DependencyProperty.Register("ErrorRegionVisibility", typeof(Visibility), typeof(ShellViewModel), new UIPropertyMetadata(null));




        public GridLength ErrorRegionGridHeight
        {
            get { return (GridLength)GetValue(ErrorRegionGridHeightProperty); }
            set { SetValue(ErrorRegionGridHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorRegionGridHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorRegionGridHeightProperty =
            DependencyProperty.Register("ErrorRegionGridHeight", typeof(GridLength), typeof(ShellViewModel), new UIPropertyMetadata(null));


    }
}
