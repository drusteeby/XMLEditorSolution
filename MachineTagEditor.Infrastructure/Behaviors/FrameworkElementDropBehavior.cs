using MachineTagEditor.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace MachineTagEditor.Infrastructure.Behaviors
{
    public class FrameworkElementDropBehavior: Behavior<FrameworkElement>
    {
        Type dataType;
        FrameworkElementAdorner adorner;
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.AllowDrop = true;
            //occurs once when a drag enters this element
            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            //occurs once when a drag leaves this element
            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            //occurs constently when a drag is pending over this element
            this.AssociatedObject.DragOver += AssociatedObject_DragOver;
            //Occurs when an object is dropped within the bounds of an element that is acting as a drop target.
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (this.dataType == null && this.AssociatedObject != null)
            {
                IDropable dropObject = this.AssociatedObject.DataContext as IDropable;
                if(dropObject != null)
                {
                    this.dataType = dropObject.DataType;
                }
            }

            if (this.adorner == null)
                this.adorner = new FrameworkElementAdorner(sender as UIElement);
            e.Handled = true;

        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (this.adorner != null)
                this.adorner.Remove();
            e.Handled = true;
        }



        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (dataType != null)
            {
                //if the data type can be dropped 
                if (e.Data.GetDataPresent(dataType))
                {
                    //drop the data
                    IDropable target = this.AssociatedObject.DataContext as IDropable;
                    target.Drop(e.Data.GetData(dataType));

                    //remove the data from the source
                    IDragable source = e.Data.GetData(dataType) as IDragable;
                    source.Remove(e.Data.GetData(dataType));
                }
            }
            if (this.adorner != null)
                this.adorner.Remove();

            e.Handled = true;
            return;
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (dataType != null)
            {
                //if item can be dropped
                if (e.Data.GetDataPresent(dataType))
                {
                    //give mouse effect
                    this.SetDragDropEffects(e);
                    //draw the dots
                    if (this.adorner != null)
                        this.adorner.Update();
                }
            }
            e.Handled = true;
        }

        private void SetDragDropEffects(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;  //default to None

            //if the data type can be dropped 
            if (e.Data.GetDataPresent(dataType))
            {
                e.Effects = DragDropEffects.Move;
            }
        }


    }
}
