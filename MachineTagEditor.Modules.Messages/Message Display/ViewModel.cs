using MachineTagEditor.Infrastructure.Events;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;
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

namespace MachineTagEditor.Modules.Message_Display
{
    public partial class ViewModel : BindableBase
    {
        public ObservableCollection<string> MessageList { get; set; }
        IEventAggregator _eventAggregator;

        public ViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DisplayMessage>().Subscribe(OnDisplayMessage);
            MessageList = new ObservableCollection<string>();
        }

        private void OnDisplayMessage(string msg)
        {
            MessageList.Add(System.DateTime.Now + ": " + msg);
        }
    }
}
