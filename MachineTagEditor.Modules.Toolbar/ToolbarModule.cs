﻿using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.Toolbar
{
    [Module(ModuleName = "ToolbarModule")]
    public class ToolbarModule: IModule
    {
        [Dependency]
        public IRegionManager regionMananger { get; set; }

        public void Initialize()
        {
            regionMananger.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(Menu.View));
            regionMananger.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(Ribbon.View));
        }
    }
}
