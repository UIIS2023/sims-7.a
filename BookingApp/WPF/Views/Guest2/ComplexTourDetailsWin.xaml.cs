using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ComplexTourDetailsWin.xaml
    /// </summary>
    public partial class ComplexTourDetailsWin : Window
    {
    
        public CreateComplexTour SelectedComplexTour { get; set; }
      
        public ComplexTourDetailsWin(CreateComplexTour selectedComplexTour)
        {
            InitializeComponent();
            SelectedComplexTour = selectedComplexTour;
            DataContext = this;
            
        }
    }
}
