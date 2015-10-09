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


            TagService.XmlFileList.CollectionChanged += XmlFileList_CollectionChanged;

            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(DataTags.View));
            regionMananger.RegisterViewWithRegion(RegionNames.HelpRegion, typeof(QuickActions.View));
            eventAggregator.GetEvent<RibbonEvent>().Subscribe(OnRibbonCommand,ThreadOption.PublisherThread,true,(x) => x.ToLower().Contains("new"));

            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(Views.AlarmTag));
            regionMananger.RegisterViewWithRegion(RegionNames.DataRegion, typeof(Views.ListBoxTagViewer));
        }

        private void XmlFileList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (TagService.XmlFileList.Count > 0)
                (regionMananger.Regions[RegionNames.DataRegion].Views.Single((x) => x.GetType() == typeof(DataTags.View)) as DataTags.View).Visibility = Visibility.Visible;
            else
                (regionMananger.Regions[RegionNames.DataRegion].Views.Single((x) => x.GetType() == typeof(DataTags.View)) as DataTags.View).Visibility = Visibility.Visible;
        }

        private void OnRibbonCommand(string commandParameter)
        {
            if (commandParameter.ToLower().Contains("datatype"))
                regionMananger.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(Wizards.DataType.View));
            //regionMananger.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(AddDataType.View));

            if (commandParameter.ToLower().Contains("alarm"))
                regionMananger.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(AddAlarm.View));

            if (commandParameter.ToLower().Contains("parameter"))
                regionMananger.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(AddParameter.View));
        }
    }
}
