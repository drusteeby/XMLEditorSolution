using MachineTagEditor.Infrastructure.Containers;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.Units.AddUnit
{
    public class ViewModel: DependencyObject
    {
        #region Dependency Properties


        public string firstLabelContent
        {
            get { return (string)GetValue(firstLabelContentProperty); }
            set { SetValue(firstLabelContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for firstLabelContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty firstLabelContentProperty =
            DependencyProperty.Register("firstLabelContent", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));





        public string firstETBnullText
        {
            get { return (string)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));





        public string firstETBText
        {
            get { return (string)GetValue(firstETBTextProperty); }
            set { SetValue(firstETBTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for firstETBText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty firstETBTextProperty =
            DependencyProperty.Register("firstETBText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));






        public string secondLabelContent
        {
            get { return (string)GetValue(secondLabelContentProperty); }
            set { SetValue(secondLabelContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for secondLabelContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty secondLabelContentProperty =
            DependencyProperty.Register("secondLabelContent", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));





        public string secondETBText
        {
            get { return (string)GetValue(secondETBTextProperty); }
            set { SetValue(secondETBTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for secondETBText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty secondETBTextProperty =
            DependencyProperty.Register("secondETBText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));



        public string secondETBNullText
        {
            get { return (string)GetValue(secondETBNullTextProperty); }
            set { SetValue(secondETBNullTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for secondETBNullText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty secondETBNullTextProperty =
            DependencyProperty.Register("secondETBNullText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));



        public Visibility USMetricVisibility
        {
            get { return (Visibility)GetValue(USMetricVisibilityProperty); }
            set { SetValue(USMetricVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for USMetricVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty USMetricVisibilityProperty =
            DependencyProperty.Register("USMetricVisibility", typeof(Visibility), typeof(ViewModel), new UIPropertyMetadata(null));




        public string thirdLabelContent
        {
            get { return (string)GetValue(thirdLabelContentProperty); }
            set { SetValue(thirdLabelContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for thirdLabelContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty thirdLabelContentProperty =
            DependencyProperty.Register("thirdLabelContent", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));




        public string thirdETBText
        {
            get { return (string)GetValue(thirdETBTextProperty); }
            set { SetValue(thirdETBTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for thirdETBText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty thirdETBTextProperty =
            DependencyProperty.Register("thirdETBText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));



        public string thirdETBNullText
        {
            get { return (string)GetValue(thirdETBNullTextProperty); }
            set { SetValue(thirdETBNullTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for thirdETBNullText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty thirdETBNullTextProperty =
            DependencyProperty.Register("thirdETBNullText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));







        public string fourthLabelContent
        {
            get { return (string)GetValue(fourthLabelContentProperty); }
            set { SetValue(fourthLabelContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for fourthLabelContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty fourthLabelContentProperty =
            DependencyProperty.Register("fourthLabelContent", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));




        public string fourthETBText
        {
            get { return (string)GetValue(fourthETBTextProperty); }
            set { SetValue(fourthETBTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for fourthETBText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty fourthETBTextProperty =
            DependencyProperty.Register("fourthETBText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));



        public string fourthETBNullText
        {
            get { return (string)GetValue(fourthETBNullTextProperty); }
            set { SetValue(fourthETBNullTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for fourthETBNullText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty fourthETBNullTextProperty =
            DependencyProperty.Register("fourthETBNullText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));


        #endregion

        #region Commands
        public DelegateCommand SingleCommand { get; set; }
        public DelegateCommand USMetricCommand { get; set;}
        #endregion
        public ViewModel()
        {
            SingleCommand = new DelegateCommand(OnSingleCommand);
            USMetricCommand = new DelegateCommand(OnUSMetricCommand);
            firstLabelContent = "Label:";
            USMetricVisibility = Visibility.Collapsed;

        }

        private void OnUSMetricCommand()
        {
            USMetricVisibility = Visibility.Visible;
            firstLabelContent = "Metric Label:";
            firstETBnullText = "mm";

            secondLabelContent = "U.S. Label:";
            secondETBNullText = "\"";


        }

        private void OnSingleCommand()
        {
            firstLabelContent = "Label:";
            firstETBnullText = default(string);
            USMetricVisibility = Visibility.Collapsed;
        }
    }
}
