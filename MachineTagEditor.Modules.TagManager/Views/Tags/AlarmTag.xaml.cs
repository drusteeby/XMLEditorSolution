﻿using MachineTagEditor.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
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
using System.Xml;

namespace MachineTagEditor.Modules.TagManager.Views
{
    /// <summary>
    /// Interaction logic for AlarmTag.xaml
    /// </summary>
    public partial class AlarmTag : UserControl
    {
        public AlarmTag()
        {
            InitializeComponent();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
