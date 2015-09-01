using MachineTagEditor.Infrastructure;
using MCM.Core.Tags;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace MachineTagEditor.Modules.Alarms.AddAlarm
{
    public partial class ViewModel : DependencyObject
    {

        public DelegateCommand SelectionChanged { get; set; }
        public DelegateCommand goBack { get; set; }
        public DelegateCommand AddAlarm { get; set; }

        void initCommands()
        {
            SelectionChanged = new DelegateCommand(OnSelectionChanged);
            goBack = new DelegateCommand(OnGoBack);
            AddAlarm = new DelegateCommand(OnAddAlarm, CanAddAlarm);
        }

        public void OnAddAlarm()
        {
            List<XmlAttribute> attributes = new List<XmlAttribute>();
            attributes.Add(_service.createAttribute("readFrom", AlarmReadFrom));
            attributes.Add(_service.createAttribute("readName", AlarmReadName));
            attributes.Add(_service.createAttribute("name", AlarmName));
            attributes.Add(_service.createAttribute("text", AlarmText));
            attributes.Add(_service.createAttribute("page", AlarmPage));
            attributes.Add(_service.createAttribute("group", "Alarms"));
            

            if(SelectedEnumName.Contains("New Item"))
            {
                List<XmlAttribute> messageAttributes = new List<XmlAttribute>();
                foreach (EnumerationContainer message in EnumerationValues)
                {                 
                    messageAttributes.Add(_service.createAttribute("value", message.Value));
                    messageAttributes.Add(_service.createAttribute("value", message.Text));
                }

                _service.AddElement("val", messageAttributes);

                attributes.Add(_service.createAttribute("parent", newEnumName));
                
            }
            else
                attributes.Add(_service.createAttribute("parent", SelectedEnumName));

            _service.AddElement("tag", attributes);
            
        }

        public bool CanAddAlarm()
        {
            return true;
        }

        public void OnGoBack()
        {
            var view = regionManager.Regions["WindowRegion"].Views.Single(x => x.GetType() == typeof(View));
            regionManager.Regions["WindowRegion"].Remove(view);

        }


        public void OnSelectionChanged()
        {
            if (SelectedEnumName.Contains("New Item"))
            {
                newVisibility = Visibility.Visible;
                EnumerationValues.Clear();
                for (int i = 0; i < 32; i++)
                    EnumerationValues.Add(new EnumerationContainer(i.ToString(),"Spare " + i.ToString()));
            }

            else
            {
                newVisibility = Visibility.Collapsed;
                DataTag tag = _service.VirtualTags.Single(dt => (dt.Name == SelectedEnumName));

                EnumerationValues = new ObservableCollection<EnumerationContainer>(TagHelper.getContainersfromEnum(TagHelper.getEnumfromTag(tag)));
            }


        }

    }
}
