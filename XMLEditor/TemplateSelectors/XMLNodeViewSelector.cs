using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace XMLEditor
{
    public class XMLNodeViewSelector : DataTemplateSelector
    {
        public HierarchicalDataTemplate NodeMetalTemplate { get; set; }
        public HierarchicalDataTemplate NodeBlueTemplate { get; set; }
        public HierarchicalDataTemplate NodeTextTemplate { get; set; }
        public HierarchicalDataTemplate DefaultTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return DefaultTemplate;

            string template = (string)item;

            if (String.IsNullOrEmpty(template)) return DefaultTemplate;

            if (template.Contains("Metal"))
                return NodeMetalTemplate;
            else if (template.Contains("Blue"))
                return NodeBlueTemplate;
            else if (template.Contains("Text"))
                return NodeTextTemplate;
            else
                return DefaultTemplate;
        }
    }




}

