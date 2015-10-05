using MachineTagEditor.Infrastructure.Extensions.Visual;
using MachineTagEditor.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MachineTagEditor.Infrastructure.Behaviors
{
    public class FrameworkElementDragBehavior : Behavior<FrameworkElement>
    {
        private bool isMouseClicked = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
            this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            IDragable dragObject = this.AssociatedObject as IDragable;

            if (dragObject == null)
            {
                var allParents = this.AssociatedObject.GetSelfAndAncestors();

                foreach(var parent in allParents)
                {
                    dragObject = parent as IDragable;
                    if (dragObject != null) break;
                }
            }
                

            if (dragObject != null && isMouseClicked)
            {
                DataObject data = new DataObject();
                data.SetData(dragObject.DataType, this.AssociatedObject);
                DragDrop.DoDragDrop(this.AssociatedObject, data, DragDropEffects.Move);
                
            }

            isMouseClicked = false;

        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = false;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = true;
        }
    }
}
