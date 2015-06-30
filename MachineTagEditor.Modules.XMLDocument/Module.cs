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

namespace MachineTagEditor.Modules.XMLDocument
{
    [Module(ModuleName = "XMLDocumentModule")]
    public class XMLDocumentModule : IModule
    {
        [Dependency]
        public IRegionManager regionMananger { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }
        public void Initialize()
        {
            //register services
            container.RegisterType(typeof(IXMLService), typeof(XMLService),new ContainerControlledLifetimeManager());

            //register views
            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(XMLDocument.View));
        }
    }
}
