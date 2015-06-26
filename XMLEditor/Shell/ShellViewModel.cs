using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XMLEditor
{
    public class ShellViewModel : DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }

        public ShellViewModel()
        {
            regionManager.RegisterViewWithRegion("MainRegion", () => this.container.Resolve<XMLDocumentView.View>());
            ErrorRegionVisibility = Visibility.Collapsed;
            ErrorRegionGridHeight = new GridLength(0, GridUnitType.Star);
        }

        public Visibility ErrorRegionVisibility
        {
            get { return (Visibility)GetValue(ErrorRegionVisibilityProperty); }
            set { SetValue(ErrorRegionVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorRegionVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorRegionVisibilityProperty =
            DependencyProperty.Register("ErrorRegionVisibility", typeof(Visibility), typeof(ShellViewModel), new UIPropertyMetadata(null));




        public GridLength ErrorRegionGridHeight
        {
            get { return (GridLength)GetValue(ErrorRegionGridHeightProperty); }
            set { SetValue(ErrorRegionGridHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorRegionGridHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorRegionGridHeightProperty =
            DependencyProperty.Register("ErrorRegionGridHeight", typeof(GridLength), typeof(ShellViewModel), new UIPropertyMetadata(null));

        

        
    }
}
