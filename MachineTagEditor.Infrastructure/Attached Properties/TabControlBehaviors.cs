using MachineTagEditor.Infrastructure.Extensions.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MachineTagEditor.Infrastructure.Attached_Properties
{
    public class TabControlBehaviors
    {


        public static Thickness GetHeaderPanelMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(HeaderPanelMarginProperty);
        }

        public static void SetHeaderPanelMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(HeaderPanelMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderPanelMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderPanelMarginProperty =
            DependencyProperty.RegisterAttached("HeaderPanelMargin", typeof(Thickness), typeof(TabControlBehaviors), new UIPropertyMetadata(new Thickness(5),new PropertyChangedCallback(OnHeaderPanelMarginChanged)));

        private static void OnHeaderPanelMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TabControl).Loaded += TabControlBehaviors_Loaded;           

        }

        private static void TabControlBehaviors_Loaded(object sender, RoutedEventArgs e)
        {
            var list = (sender as TabControl).GetDescendants<UIElement>();

            foreach (UIElement element in list)
                try { element.SetValue(Control.MarginProperty, new Thickness(0)); }
                catch { }
        }
    }
}
