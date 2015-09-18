﻿using MachineTagEditor.Infrastructure;
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

namespace MachineTagEditor.Modules.TagManager
{
    [Module(ModuleName = "TagManagerModule")]
    public class TagManagerModule : IModule
    {
        [Dependency]
        public IRegionManager regionMananger { get; set; }

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }

        public TagManagerService TagService = new TagManagerService(); 

        public void Initialize()
        {
            container.RegisterInstance(TagService, new ExternallyControlledLifetimeManager());

            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(DataTags.View));
            regionMananger.RegisterViewWithRegion(RegionNames.HelpRegion, typeof(QuickActions.View));
            eventAggregator.GetEvent<RibbonEvent>().Subscribe(OnRibbonCommand,ThreadOption.PublisherThread,true,(x) => x.ToLower().Contains("new"));
        }

        private void OnRibbonCommand(string commandParameter)
        {
            if (commandParameter.ToLower().Contains("datatype"))
                regionMananger.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(AddDataType.View));
            
            if (commandParameter.ToLower().Contains("alarm"))
                regionMananger.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(AddAlarm.View));
        }
    }
}
