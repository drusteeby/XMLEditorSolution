using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.Navigation
{
    [Module(ModuleName = "NavigationModule")]
    public class NavigationModule: IModule
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavBar.View));
            regionManager.RegisterViewWithRegion(RegionNames.OverlayRegion, typeof(Splash.View));
            regionManager.RegisterViewWithRegion(RegionNames.OverlayRegion, typeof(Blank));

            regionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(Blank));
            regionManager.RegisterViewWithRegion(RegionNames.HelpRegion, typeof(Blank));
            regionManager.RegisterViewWithRegion(RegionNames.DataRegion, typeof(Blank));
        }
    }
}
