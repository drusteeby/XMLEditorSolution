namespace XMLEditor
{
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Xml;

    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        [Dependency]
        public IUnityContainer container { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("MainRegion", () => this.container.Resolve<XMLDocumentView.View>());
                      
        }




        public double MainWindowHeight
        {
            get { return (double)GetValue(MainWindowHeightProperty); }
            set { SetValue(MainWindowHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainWindowHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainWindowHeightProperty =
            DependencyProperty.Register("MainWindowHeight", typeof(double), typeof(Shell), new PropertyMetadata(null));



        public double MainWindowWidth
        {
            get { return (double)GetValue(MainWindowWidthProperty); }
            set { SetValue(MainWindowWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainWindowWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainWindowWidthProperty =
            DependencyProperty.Register("MainWindowWidth", typeof(double), typeof(Shell), new PropertyMetadata(null));     

        

    }
}
