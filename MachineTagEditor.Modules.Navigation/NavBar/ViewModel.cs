using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.Navigation.NavBar
{

    public class ViewModel
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        public DelegateCommand NavigateToAlarms { get; set; }
        public DelegateCommand NavigateToXML { get; set; }
        public DelegateCommand NavigateToUnits { get; set; }

        public ViewModel()
        {
            NavigateToAlarms = new DelegateCommand(OnNavigateToAlarms);
            NavigateToXML = new DelegateCommand(OnNavigateToXML);
            NavigateToUnits = new DelegateCommand(OnNavigateToUnits);
        }

        public void OnNavigateToAlarms()
        {
            regionManager.RequestNavigate(RegionNames.DataRegion, regionManager.Regions[RegionNames.DataRegion].Views.Single(x => x.ToString().Contains("CurrentAlarms")).ToString());
            regionManager.RequestNavigate(RegionNames.ActionRegion, regionManager.Regions[RegionNames.ActionRegion].Views.Single(x => x.ToString().Contains("AddAlarm")).ToString());
            regionManager.RequestNavigate(RegionNames.HelpRegion, regionManager.Regions[RegionNames.HelpRegion].Views.Single(x => x.ToString().Contains("AddAlarm")).ToString());

            eventAggregator.GetEvent<DisplayMessage>().Publish("Navigating to Alarms");

            
        }

        public void OnNavigateToXML()
        {
            regionManager.RequestNavigate(RegionNames.DataRegion, regionManager.Regions[RegionNames.DataRegion].Views.Single(x => x.ToString().Contains("TagManager")).ToString());
            regionManager.RequestNavigate(RegionNames.ActionRegion, regionManager.Regions[RegionNames.ActionRegion].Views.Single(x => x.ToString().Contains("Blank")).ToString());
            regionManager.RequestNavigate(RegionNames.HelpRegion, regionManager.Regions[RegionNames.HelpRegion].Views.Single(x => x.ToString().Contains("Blank")).ToString());

            eventAggregator.GetEvent<DisplayMessage>().Publish("Navigating to XML");
        }

        public void OnNavigateToUnits()
        {
            regionManager.RequestNavigate(RegionNames.DataRegion, regionManager.Regions[RegionNames.DataRegion].Views.Single(x => x.ToString().Contains("Data")).ToString());
            regionManager.RequestNavigate(RegionNames.ActionRegion, regionManager.Regions[RegionNames.ActionRegion].Views.Single(x => x.ToString().Contains("Unit")).ToString());

            eventAggregator.GetEvent<DisplayMessage>().Publish("Navigating to Units");
        }
    }
}
