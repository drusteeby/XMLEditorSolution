using MachineTagEditor.Infrastructure;
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
        public void Initialize()
        {
            regionMananger.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(AddAlarm.View));
            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(CurrentAlarms.View));
            regionMananger.RegisterViewWithRegion(RegionNames.HelpRegion, typeof(AddAlarm.Help));
        }
    }
}
