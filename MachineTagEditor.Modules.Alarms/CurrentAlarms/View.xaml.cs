﻿using Microsoft.Practices.Prism;
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

namespace MachineTagEditor.Modules.Alarms.CurrentAlarms
{
    /// <summary>
    /// Interaction logic for CurrentAlarmsView.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View(ViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }


    }
}
