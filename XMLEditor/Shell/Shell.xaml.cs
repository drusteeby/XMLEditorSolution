namespace XMLEditor
{
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Drawing;
    using System.Xml;

    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell(ShellViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;                     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var window = (sender as Window);
            var secondaryScreen = (Screen)System.Windows.Forms.Screen.AllScreens.GetValue(1);

            if (secondaryScreen != null)
            {
                Rectangle workingArea = secondaryScreen.WorkingArea;
                window.Left = workingArea.Left;
                window.Top = workingArea.Top;
                window.Width = workingArea.Width;
                window.Height = workingArea.Height;
                // If window isn't loaded then maxmizing will result in the window displaying on the primary monitor
                if (window.IsLoaded)
                    window.WindowState = WindowState.Maximized;
            }


        }
    }
}
