using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.MachineConfiguration
{
    [Module(ModuleName = "MachineConfigModule")]
    public class MachineConfigModule : IModule
    {
        [Dependency]
        public IRegionManager regionMananger { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }

        public void Initialize()
        {
            regionMananger.RegisterViewWithRegion(RegionNames.PageOverlayRegion, typeof(AddConfig.View));
        }
    }
}
