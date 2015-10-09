using MachineTagEditor.Infrastructure.Extensions.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MachineTagEditor.Infrastructure.Behaviors
{
    public class ListBoxItemBehaviors
    {
        public static bool GetSelectOnDescendantFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectOnDescendantFocusProperty);
        }

        public static void SetSelectOnDescendantFocus(
            DependencyObject obj, bool value)
        {
            obj.SetValue(SelectOnDescendantFocusProperty, value);
        }

        public static readonly DependencyProperty SelectOnDescendantFocusProperty =
            DependencyProperty.RegisterAttached(
                "SelectOnDescendantFocus",
                typeof(bool),
                typeof(ListBoxItemBehaviors),
                new UIPropertyMetadata(false, OnSelectOnDescendantFocusChanged));

        static void OnSelectOnDescendantFocusChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListBoxItem lbi = d as ListBoxItem;
            if (lbi == null) return;
            bool ov = (bool)e.OldValue;
            bool nv = (bool)e.NewValue;
            if (ov == nv) return;
            if (nv)
            {
                lbi.PreviewMouseDown += Lbi_PreviewMouseDown;
            }
            else
            {
                lbi.PreviewMouseDown -= Lbi_PreviewMouseDown;
            }
        }

        private static void Lbi_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;
            lbi.IsSelected = true;
        }

        private static void Lbi_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;
            lbi.IsSelected = true;
        }

        private static void Child_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBoxItem lbi = sender as ListBoxItem;
            lbi.IsSelected = true;
        }
    }
}
