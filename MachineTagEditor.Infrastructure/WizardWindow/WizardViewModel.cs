using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;
using Microsoft.Practices.Prism;
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

namespace MachineTagEditor.Infrastructure.WizardWindow
{
    public class WizardViewModel: DependencyObject, INavigationAware
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        public DelegateCommand GoBack { get; set; }
        public DelegateCommand GoForward { get; set; }

        int _pageIndex = 0;
        bool _active = false;

        public WizardViewModel(IEventAggregator _eventAggregator)
        {
            eventAggregator = _eventAggregator;
            eventAggregator.GetEvent<NavigateToPage>().Subscribe(OnNavigateToPage);
            pageListing = new ObservableCollection<string>();
            GoBack = new DelegateCommand(OnGoBack,CanGoBack);
            GoForward = new DelegateCommand(OnGoForward,CanGoForward);
        }

        private void OnNavigateToPage(string obj)
        {
            if (pageListing.Contains(obj))
                regionManager.RequestNavigate(RegionNames.PageRegion, obj);
        }


        void Navigate()
        {
            regionManager.RequestNavigate(RegionNames.PageRegion, pageListing.ElementAt(_pageIndex), (NavigationResult nr) =>
            {
                var error = nr.Error;
                var result = nr.Result;
                // put a breakpoint here and checkout what NavigationResult contains
            });
            GoBack.RaiseCanExecuteChanged();
            GoForward.RaiseCanExecuteChanged();
        }

        public void OnGoBack()
        {
            _pageIndex--;
            Navigate();
        }

        public bool CanGoBack()
        {
            return (_pageIndex > 0);
        }

        public void OnGoForward()
        {
            _pageIndex++;
            Navigate();
        }

        public bool CanGoForward()
        {
            return (_pageIndex < pageListing.Count - 1);
        }


        public ObservableCollection<string> pageListing
        {
            get { return (ObservableCollection<string>)GetValue(pageListingProperty); }
            set { SetValue(pageListingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for pageListing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty pageListingProperty =
            DependencyProperty.Register("pageListing", typeof(ObservableCollection<string>), typeof(WizardViewModel), new UIPropertyMetadata(null));

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.NavigationService.Region.Name == RegionNames.WindowRegion)
            {
                //remove the old views
                foreach (var view in regionManager.Regions[RegionNames.PageRegion].Views)
                    regionManager.Regions[RegionNames.PageRegion].Remove(view);

                //get the new views
                List<NameTypeContainer> ViewList = (List<NameTypeContainer>)navigationContext.Parameters["ViewList"];

                //add them to the region and the viewmodel list
                foreach (NameTypeContainer view in ViewList)
                {
                    regionManager.RegisterViewWithRegion(RegionNames.PageRegion, view.Type);
                    pageListing.Add(view.Name);
                }

                //navigate to the first page
                regionManager.RequestNavigate(RegionNames.PageRegion, pageListing.First());
            }
            
        }

    }
}
