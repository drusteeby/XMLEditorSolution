using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.Units
{
    [Module(ModuleName = "UnitsModule")]
    public class UnitsModule : IModule
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }

        public void Initialize()
        {
            //Register Services
            IXMLService UnitsXMLService = container.Resolve<IXMLService>();
            UnitsXMLService.init(@"C:\appData\Machine Tag Editor\", @"UoMDefs.xml");
            container.RegisterInstance<IXMLService>("UnitsXMLService", UnitsXMLService);

            //Register Views
            regionManager.RegisterViewWithRegion(RegionNames.DataRegion, typeof(CurrentDataTypes.View));
            regionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(AddUnit.View));
        }
    }
}
