using BookingApp.WPF.ViewModels.Guide;
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
    /// Interaction logic for MyToursWin.xaml
    /// </summary>
    public partial class MyToursWin : Window
    {
        public MyToursWin(int guideId)
        {
            InitializeComponent();
            DataContext = new MyToursViewModel(guideId);
        }
    }
}
