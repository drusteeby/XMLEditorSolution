using MachineTagEditor.Infrastructure;
using MachineTagEditor.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        [Dependency]
        public IEventAggregator eventAggregator { get; set; }

        public DelegateCommand NewAlarm { get; set; }
        public DelegateCommand ExistingAlarm { get; set; }

        public ViewModel()
        {
            NewAlarm = new DelegateCommand(OnNewAlarm);
            ExistingAlarm = new DelegateCommand(OnExistingAlarm);
            messageList = new ObservableCollection<EnumerationContainer>();

            for (int i = 0; i < 32; i++)
                messageList.Add(new EnumerationContainer(i.ToString()));
        }

        private void OnExistingAlarm()
        {
            eventAggregator.GetEvent<NavigateToPage>().Publish(typeof(AddAlarmAssisted.Pages.AlarmAssisted4).FullName);
        }

        private void OnNewAlarm()
        {
            eventAggregator.GetEvent<NavigateToPage>().Publish(typeof(AddAlarmAssisted.Pages.AlarmAssisted3).FullName);
        }



        public ObservableCollection<EnumerationContainer> messageList
        {
            get { return (ObservableCollection<EnumerationContainer>)GetValue(messageListProperty); }
            set { SetValue(messageListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for messageList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty messageListProperty =
            DependencyProperty.Register("messageList", typeof(ObservableCollection<EnumerationContainer>), typeof(ViewModel), new UIPropertyMetadata(null));

        
    }
}
