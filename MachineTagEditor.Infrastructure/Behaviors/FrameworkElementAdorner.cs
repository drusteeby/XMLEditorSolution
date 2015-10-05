using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MachineTagEditor.Infrastructure.Behaviors
{
    class FrameworkElementAdorner : Adorner
    {
        private AdornerLayer adornerLayer;

        public FrameworkElementAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.adornerLayer = AdornerLayer.GetAdornerLayer(AdornedElement);
            this.adornerLayer.Add(this);
        }

        internal void Update()
        {
            this.adornerLayer.Update(this.AdornedElement);
            this.Visibility = System.Windows.Visibility.Visible;
        }

        public void Remove()
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);
           
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Red);
            renderBrush.Opacity = 0.5;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Black), 3);

            // Draw a line at the top
            drawingContext.DrawLine(renderPen, adornedElementRect.TopLeft, adornedElementRect.TopRight);

        }

    }
}
