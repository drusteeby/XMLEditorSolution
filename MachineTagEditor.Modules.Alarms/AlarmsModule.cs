using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Events;
using MachineTagEditor.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
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

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        public void Initialize()
        {
            //Register Services
            IXMLService AlarmsXMLService = container.Resolve<IXMLService>();
            AlarmsXMLService.setDirectory(@"C:\appData\Machine Tag Editor\");
            AlarmsXMLService.setFileName(@"alarms.xml");
            AlarmsXMLService.init();
            container.RegisterInstance<IXMLService>("AlarmsXMLService", AlarmsXMLService);
            container.RegisterInstance(typeof(AddAlarmAssisted.ViewModel), new AddAlarmAssisted.ViewModel(),new ExternallyControlledLifetimeManager());
            //Register Views
            regionMananger.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(AddAlarm.View));
            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(CurrentAlarms.View));
            regionMananger.RegisterViewWithRegion(RegionNames.HelpRegion, typeof(AddAlarm.Help));

            eventAggregator.GetEvent<AddAlarmConfigWizard>().Subscribe(OnAddAlarmConfigWizard, true);
        }

        private void OnAddAlarmConfigWizard(bool obj)
        {
            List<NameTypeContainer> ViewList = new List<NameTypeContainer>();
            ViewList.Add(new NameTypeContainer(typeof(AddAlarmAssisted.Pages.AlarmAssisted1).FullName,typeof(AddAlarmAssisted.Pages.AlarmAssisted1)));
            ViewList.Add(new NameTypeContainer(typeof(AddAlarmAssisted.Pages.AlarmAssisted2).FullName, typeof(AddAlarmAssisted.Pages.AlarmAssisted2)));
            ViewList.Add(new NameTypeContainer(typeof(AddAlarmAssisted.Pages.AlarmAssisted3).FullName, typeof(AddAlarmAssisted.Pages.AlarmAssisted3)));
            ViewList.Add(new NameTypeContainer(typeof(AddAlarmAssisted.Pages.AlarmAssisted4).FullName, typeof(AddAlarmAssisted.Pages.AlarmAssisted4)));
            ViewList.Add(new NameTypeContainer(typeof(AddAlarmAssisted.Pages.AlarmAssisted5).FullName, typeof(AddAlarmAssisted.Pages.AlarmAssisted5)));

            eventAggregator.GetEvent<OpenWizard>().Publish(ViewList);
        }
    }
}
