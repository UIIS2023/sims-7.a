using BookingApp.Domain.Models.Guest2;
using BookingApp.WPF.ViewModels.Guest2;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.WPF.Views.Guest2
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class StatisticsWin : Window
    {    
        public StatisticsWin()
        {
            InitializeComponent();
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel();    
            DataContext = statisticsViewModel;

        }   
    }
}
