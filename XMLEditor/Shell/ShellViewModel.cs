using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;
using MachineTagEditor.Infrastructure.WizardWindow;
using Microsoft.Practices.Prism.Commands;
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
        public DelegateCommand Exit { get; set; }

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            ErrorRegionVisibility = Visibility.Collapsed;
            ErrorRegionGridHeight = new GridLength(0, GridUnitType.Star);

            eventAggregator.GetEvent<SaveSetting>().Subscribe(OnSaveSetting);
            eventAggregator.GetEvent<OpenWizard>().Subscribe(OnOpenWizard);

        }

        private void OnOpenWizard(List<NameTypeContainer> ViewList)
        {
            NavigationParameters navParam = new NavigationParameters();
            navParam.Add("ViewList", ViewList);

            WizardViewModel vm = (WizardViewModel)container.Resolve(typeof(WizardViewModel));

            regionManager.Regions[RegionNames.WindowRegion].Context = vm;
            regionManager.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(MachineTagEditor.Infrastructure.WizardWindow.View));
            regionManager.RequestNavigate(RegionNames.WindowRegion, typeof(MachineTagEditor.Infrastructure.WizardWindow.View).FullName,navParam);
            
        }
     
        public void OnSaveSetting(SaveSettingContainer container)
        {
           
        }


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
