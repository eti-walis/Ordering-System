﻿using System;
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
using System.Windows.Shapes;
using BE;
using BL;
namespace UI
{
    /// <summary>
    /// Interaction logic for HostingUnitList.xaml
    /// </summary>
    public partial class HostingUnitList : Window
    {
        public static IBL bl = BLFactory.getBL();
        public HostingUnitList()
        {
            InitializeComponent();
            listView.ItemsSource = bl.allHostingUnits();
        }
    }
}
