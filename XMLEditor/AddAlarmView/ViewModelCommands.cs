using MCM.Core.Objects;
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

namespace XMLEditor.AddAlarmView
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
            //<tag readFrom="PLC" readName="PcAlarms[0]" name="Alarms.System" parent="AlarmSystem" text="Alarms System A" group="Alarms" page="Alarms" />
             // <bits name="WarningsMasters">
             //   <val value="00" text="Masters Spare0" />

            XmlDataProvider dataProvider = (regionManager.Regions["WindowRegion"].Context as XmlDataProvider);
            XmlDocument document = dataProvider.Document;
            XmlNode root = document.SelectSingleNode("tags");

            XmlNode alarmToAdd = document.CreateElement("tag");

            XmlAttribute alarmReadFromAttr = document.CreateAttribute("readFrom");           
            XmlAttribute alarmReadNameAttr = document.CreateAttribute("readName");
            XmlAttribute alarmNameAttr = document.CreateAttribute("name");
            XmlAttribute alarmTextAttr = document.CreateAttribute("text");
            XmlAttribute alarmGroupAttr = document.CreateAttribute("group");
            XmlAttribute alarmPageAttr = document.CreateAttribute("page");
            XmlAttribute alarmParentAttr = document.CreateAttribute("parent");

            alarmReadFromAttr.Value = AlarmReadFrom;
            alarmReadNameAttr.Value = AlarmReadName;
            alarmNameAttr.Value = AlarmName;
            alarmTextAttr.Value = AlarmText;
            alarmGroupAttr.Value = "Alarms";
            alarmPageAttr.Value = AlarmPage;

            alarmToAdd.Attributes.Append(alarmReadFromAttr);
            alarmToAdd.Attributes.Append(alarmReadNameAttr);
            alarmToAdd.Attributes.Append(alarmNameAttr);
            alarmToAdd.Attributes.Append(alarmTextAttr);
            alarmToAdd.Attributes.Append(alarmGroupAttr);
            alarmToAdd.Attributes.Append(alarmPageAttr);
            

            if(SelectedEnumName.Contains("New Item"))
            {
                XmlNode parentToAdd = document.CreateElement("bits");

                XmlAttribute parentName = document.CreateAttribute("name");
                parentName.Value = newEnumName;
                alarmParentAttr.Value = newEnumName;

                parentToAdd.Attributes.Append(parentName);

                foreach(EnumerationContainer container in EnumerationValues)
                {
                    XmlElement val = document.CreateElement("val");
                    XmlAttribute value = document.CreateAttribute("value");
                    XmlAttribute text = document.CreateAttribute("text");

                    value.Value = container.Value;
                    text.Value = container.Text;

                    val.Attributes.Append(value);
                    val.Attributes.Append(text);

                    parentToAdd.AppendChild(val);                    
                }               

                root.AppendChild(parentToAdd);
            }

            else
              alarmParentAttr.Value = SelectedEnumName;

            alarmToAdd.Attributes.Append(alarmParentAttr);
            root.AppendChild(alarmToAdd);

            document.Save(dataProvider.Source.LocalPath);


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
                DataTag tag = TagCollection.VirtualTags.Single(dt => ((dt.DataType == MCM.Core.Enum.DataType.Bits || dt.DataType == MCM.Core.Enum.DataType.Enum) && dt.Name == SelectedEnumName));

                EnumerationValues = new ObservableCollection<EnumerationContainer>(TagHelper.getContainersfromEnum(TagHelper.getEnumfromTag(tag)));
            }


        }

    }
}
