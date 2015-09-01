using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace MachineTagEditor.Infrastructure
{
    public class HelpViewSelector : DataTemplateSelector
    {
        public DataTemplate EnumTemplate { get; set; }
        public DataTemplate TagTemplate { get; set; }
        public DataTemplate ArrayTemplate { get; set; }
        public DataTemplate CopyTemplate { get; set; }
        public DataTemplate TableTemplate { get; set; }
        public DataTemplate ModTemplate { get; set; }
        public DataTemplate AlarmTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return DefaultTemplate;

            string selection = (string)item;
            switch (selection)
            {
                case "enum":
                case "bits":
                    return EnumTemplate;
                case "virtual":
                case "tag":
                    return TagTemplate;
                case "array":
                    return ArrayTemplate;
                case "copy":
                    return CopyTemplate;
                case "table":
                    return TableTemplate;
                case "mod":
                    return ModTemplate;
                case "AddAlarm":
                    return AlarmTemplate;
                default:
                    return DefaultTemplate;
            }

        }
    }
}
