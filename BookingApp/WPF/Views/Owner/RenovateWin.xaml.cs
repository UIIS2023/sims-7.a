using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Owner;
using BookingApp.WPF.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace BookingApp.WPF.Views.Owner
{
    public partial class RenovateWin : Window
    {
        public RenovateWin(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            DataContext = new RenovateViewModel(selectedAccommodation);
        }
       
    }
}
