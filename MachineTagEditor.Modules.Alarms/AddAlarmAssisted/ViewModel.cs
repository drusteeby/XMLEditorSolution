using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.Alarms.AddAlarmAssisted
{
    public class ViewModel: DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }
        public DelegateCommand GoBack { get; set; }
        public DelegateCommand GoForward { get; set; }

        List<string> pageList;
        int pageIndex = 0;

        public ViewModel()
        {
            pageList = new List<string>();
            GoBack = new DelegateCommand(OnGoBack,CanGoBack);
            GoForward = new DelegateCommand(OnGoForward,CanGoForward);

            pageList.Add("AlarmAssisted1");
            pageList.Add("AlarmAssisted2");
            pageList.Add("AlarmAssisted3");
            pageList.Add("AlarmAssisted4");
            pageList.Add("AlarmAssisted5");
        }

        void Navigate()
        {
            regionManager.RequestNavigate(RegionNames.PageRegion, pageList.ElementAt(pageIndex));
            GoBack.RaiseCanExecuteChanged();
            GoForward.RaiseCanExecuteChanged();
        }

        public void OnGoBack()
        {
            pageIndex--;
            Navigate();
        }

        public bool CanGoBack()
        {
            return (pageIndex > 0);
        }

        public void OnGoForward()
        {
            pageIndex++;
            Navigate();
        }

        public bool CanGoForward()
        {
            return (pageIndex < pageList.Count - 1);
        }
    }
}
