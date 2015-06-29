using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.Navigation.NavBar
{

    public class ViewModel
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        public DelegateCommand NavigateToAlarms { get; set; }
        public DelegateCommand NavigateToXML { get; set; }

        public ViewModel()
        {
            NavigateToAlarms = new DelegateCommand(OnNavigateToAlarms);
            NavigateToXML = new DelegateCommand(OnNavigateToXML);
        }

        public void OnNavigateToAlarms()
        {
            regionManager.RequestNavigate(RegionNames.DataRegion, regionManager.Regions[RegionNames.DataRegion].Views.Single(x => x.ToString().Contains("CurrentAlarms")).ToString());
            regionManager.RequestNavigate(RegionNames.ActionRegion, regionManager.Regions[RegionNames.ActionRegion].Views.Single(x => x.ToString().Contains("AddAlarm")).ToString());
            regionManager.RequestNavigate(RegionNames.HelpRegion, regionManager.Regions[RegionNames.HelpRegion].Views.Single(x => x.ToString().Contains("AddAlarm")).ToString());

            
        }

        public void OnNavigateToXML()
        {
            regionManager.RequestNavigate(RegionNames.DataRegion, regionManager.Regions[RegionNames.DataRegion].Views.Single(x => x.ToString().Contains("XMLDocument")).ToString());
            regionManager.RequestNavigate(RegionNames.ActionRegion, regionManager.Regions[RegionNames.ActionRegion].Views.Single(x => x.ToString().Contains("Blank")).ToString());
            regionManager.RequestNavigate(RegionNames.HelpRegion, regionManager.Regions[RegionNames.HelpRegion].Views.Single(x => x.ToString().Contains("Blank")).ToString());
        }
    }
}
