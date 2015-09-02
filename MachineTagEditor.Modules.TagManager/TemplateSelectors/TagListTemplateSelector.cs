using Microsoft.Practices.Prism;
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
using System.Windows.Controls;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager.TemplateSelectors
{
    public partial class TagListTemplateSelector : DataTemplateSelector
    {
        HierarchicalDataTemplate _alarmTagTemplate;
        HierarchicalDataTemplate _warningTagTemplate;
        HierarchicalDataTemplate _enumTagTemplate;
        HierarchicalDataTemplate _dataTypeTagTemplate;
        HierarchicalDataTemplate _defaultTagTemplate;

        public HierarchicalDataTemplate AlarmTagTemplate { get; set; }
        public HierarchicalDataTemplate WarningTagTemplate { get; set; }
        public HierarchicalDataTemplate EnumTemplate { get; set; }
        public HierarchicalDataTemplate DataTypeTagTemplate { get; set; }
        public HierarchicalDataTemplate DefaultTagTemplate { get; set; }

        public TagListTemplateSelector()
        {

        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null || item.GetType() != typeof(XmlElement)) return DefaultTagTemplate;

            XmlElement element = (XmlElement)item;

            if (element.Name.Contains("bit") || element.Name.Contains("enum"))
                return EnumTemplate;
            
            if(element.HasAttribute("group"))
            {
                var groupAttr = element.Attributes["group"];

                if (groupAttr.Value.Contains("Alarm"))
                    return AlarmTagTemplate;
                else if (groupAttr.Value.Contains("Warning"))
                    return WarningTagTemplate;
            }

            return DefaultTagTemplate;
        }
    }
}
