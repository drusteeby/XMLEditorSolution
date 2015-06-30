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

namespace MachineTagEditor.Modules.Alarms
{
    [Module( ModuleName = "AlarmsModule")]
    public class AlarmsModule: IModule
    {
        [Dependency]
        public IRegionManager regionMananger { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }
       

        public void Initialize()
        {
            //Register Services
            IXMLService AlarmsXMLService = container.Resolve<IXMLService>();
            AlarmsXMLService.setDirectory(@"C:\appData\Machine Tag Editor\");
            AlarmsXMLService.setFileName(@"alarms.xml");
            AlarmsXMLService.init();
            container.RegisterInstance<IXMLService>("AlarmsXMLService", AlarmsXMLService);

            //Register Views
            regionMananger.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(AddAlarm.View));
            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(CurrentAlarms.View));
            regionMananger.RegisterViewWithRegion(RegionNames.HelpRegion, typeof(AddAlarm.Help));


        }
    }
}
