using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Windows.Controls;

namespace MachineTagEditor.Infrastructure.WizardWindow
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }


        public View()
        {
            InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.DataContext = regionManager.Regions["WindowRegion"].Context;
        }

        
    }
}
