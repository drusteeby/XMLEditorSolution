using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.XMLDocument
{
    [Module(ModuleName = "AlarmsModule")]
    public class XMLDocumentModule : IModule
    {
        [Dependency]
        public IRegionManager regionMananger { get; set; }
        public void Initialize()
        {
            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(XMLDocument.View));
        }
    }
}
