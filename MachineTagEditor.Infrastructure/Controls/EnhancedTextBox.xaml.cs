using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineTagEditor.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for EnhancedTextBox.xaml
    /// </summary>
    public partial class EnhancedTextBox : UserControl
    {

        #region Attached Properties
        public static int GetMyProperty(DependencyObject obj)
        {
            return (int)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, int value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached("MyProperty", typeof(int), typeof(EnhancedTextBox), new PropertyMetadata(0));



        public string NullText
        {
            get { return (string)GetValue(NullTextProperty); }
            set { SetValue(NullTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NullText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NullTextProperty =
            DependencyProperty.Register("NullText", typeof(string), typeof(EnhancedTextBox), new UIPropertyMetadata(null,new PropertyChangedCallback(OnNullTextChanged)));

        private static void OnNullTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            (d as EnhancedTextBox).updateBackground(newNullText: (string)e.NewValue);
        }

        public bool NullTextRetains
        {
            get { return (bool)GetValue(NullTextRetainsProperty); }
            set { SetValue(NullTextRetainsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NullTextRetains.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NullTextRetainsProperty =
            DependencyProperty.Register("NullTextRetains", typeof(bool), typeof(EnhancedTextBox), new UIPropertyMetadata(default(bool)));




        public Style NullTextStyle
        {
            get { return (Style)GetValue(NullTextStyleProperty); }
            set { SetValue(NullTextStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NullTextStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NullTextStyleProperty =
            DependencyProperty.Register("NullTextStyle", typeof(Style), typeof(EnhancedTextBox), new UIPropertyMetadata(default(Style)));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EnhancedTextBox), new UIPropertyMetadata(default(string)));




        public VisualBrush textBoxBG
        {
            get { return (VisualBrush)GetValue(textBoxBGProperty); }
            set { SetValue(textBoxBGProperty, value); }
        }

        // Using a DependencyProperty as the backing store for textBoxBG.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty textBoxBGProperty =
            DependencyProperty.Register("textBoxBG", typeof(VisualBrush), typeof(EnhancedTextBox), new UIPropertyMetadata(null));




        public Brush defaultBrush
        {
            get { return (Brush)GetValue(defaultBrushProperty); }
            set { SetValue(defaultBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for defaultBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty defaultBrushProperty =
            DependencyProperty.Register("defaultBrush", typeof(Brush), typeof(EnhancedTextBox), new UIPropertyMetadata(new SolidColorBrush(Colors.BlueViolet)));



        #endregion

        Label _nullTextLabel = new Label();


        public EnhancedTextBox()
        {
            InitializeComponent();
            //textBox.GotFocus += checkText;
            //textBox.MouseEnter += checkText;
            //textBox.MouseLeave += checkText;
            //textBox.LostFocus += checkText;
            //textBox.LostKeyboardFocus += checkText;

           // textBox.Loaded += TextBox_Loaded;
           // textBox.TextChanged += TextBox_TextChanged;

            //textBox.TextAlignment = TextAlignment.Left;
            //textBox.VerticalContentAlignment = VerticalAlignment.Center;

           // textBoxBG = new VisualBrush();


        }

        int i = 0;

        public void updateBackground(string newNullText = null)
        {
            if( newNullText != null)
                _nullTextLabel.Content = newNullText;

            _nullTextLabel.HorizontalAlignment = HorizontalAlignment.Left;
            _nullTextLabel.VerticalAlignment = VerticalAlignment.Center;

            _nullTextLabel.Background = new SolidColorBrush(Colors.White);


            var grid = new Grid();
            grid.Background = new SolidColorBrush(Colors.White);
            

            var b = new VisualBrush(_nullTextLabel);
            b.AlignmentX = AlignmentX.Left;
            b.AlignmentY = AlignmentY.Center;
            
            

            textBox.Background = b;
            
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = textBox.Text;
        }

        #region Event Handlers
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            defaultBrush = new VisualBrush();
        }

        void checkText(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox.Text) && !textBox.IsKeyboardFocused && !textBox.IsFocused)
                textBox.Background = textBoxBG;
            else
            {
                textBox.Background = new SolidColorBrush(Colors.White);

                if (e.RoutedEvent.Equals(GotFocusEvent) && String.IsNullOrEmpty(textBox.Text) && NullTextRetains)
                    textBox.Text = NullText;
            }
            

        }



        #endregion
    }
}
