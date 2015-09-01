using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.Messages
{
    [Module(ModuleName = "MessagesModule")]
    public class MessagesModule : IModule
    {
        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        [Dependency]
        public IRegionManager regionManager { get; set; }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MessagesRegion, typeof(Message_Display.View));

        }

 
    }
}
