﻿using CommunityToolkit.Mvvm.Input;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.Guest1.HelpWizard
{
    /// <summary>
    /// Interaction logic for Wizard.xaml
    /// </summary>
    public partial class Wizard : Window
    {
        public RelayCommand ShowHelpCommand { get; set; }

        public Wizard()
        {
            InitializeComponent();
            this.DataContext = this;

            ShowHelpCommand = new RelayCommand(() => { ShowHelp(); });

        }

        private void ShowHelp()
        {
            var helpDoc =  new HelpDocument();
            helpDoc.ShowDialog();        
        }
    }
}
