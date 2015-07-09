using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;
using MachineTagEditor.Modules.MachineConfiguration.AddConfig.Pages;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
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

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        public void Initialize()
        {
            regionMananger.RegisterViewWithRegion(RegionNames.PageRegion, typeof(AddConfig.Pages.NamePage));
            
            eventAggregator.GetEvent<MachineConfigWizard>().Subscribe(OnMachineConfigWizard,true);
        }

        public void OnMachineConfigWizard(bool obj)
        {
            var ViewList = new List<NameTypeContainer>();

            ViewList.Add(new NameTypeContainer(typeof(AddConfig.Pages.NamePage).FullName,typeof(AddConfig.Pages.NamePage)));
            eventAggregator.GetEvent<OpenWizard>().Publish(ViewList);
        }
    }
}
