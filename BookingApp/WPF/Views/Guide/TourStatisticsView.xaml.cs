﻿using BookingApp.WPF.ViewModels.Guide;
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

namespace BookingApp.WPF.Views.Guide
{
    /// <summary>
    /// Interaction logic for TourStatisticsView.xaml
    /// </summary>
    public partial class TourStatisticsView : Window
    {
        public TourStatisticsView(int guideId)
        {
            InitializeComponent();
            DataContext = new TourStatisticsViewModel(guideId);
        }

        
    }
}
