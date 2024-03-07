using BookingApp.WPF.ViewModels.Guest2;
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

namespace BookingApp.WPF.Views.Guest2
{
    /// <summary>
    /// Interaction logic for TourFeedbackView.xaml
    /// </summary>
    public partial class TourFeedbackWin : Window
    {     
        public TourFeedbackWin(int idUser)
        {
            InitializeComponent();
            DataContext = new TourFeedbackViewModel(idUser);
        }
    }
}
