using MachineTagEditor.Infrastructure.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MachineTagEditor.Infrastructure.TemplateSelectors
{
    public class GridPanelTemplateSelector: DataTemplateSelector
    {
        public DataTemplate TextRowTempalte { get; set; }
        public DataTemplate ComboBoxRowTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if ((item as UnitAttributeContainer).isLastRow)
                return ComboBoxRowTemplate;
            else
                return TextRowTempalte;
        }
    }
}
