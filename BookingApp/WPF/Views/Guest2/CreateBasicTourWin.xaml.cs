using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.WPF.ViewModels.Guest2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for CreateBasicTourWin.xaml
    /// </summary>
    public partial class CreateBasicTourWin : Window
    {
        
        public CreateBasicTourWin()
        {
            InitializeComponent();
            DataContext = new CreateBasicTourViewModel();

           
        }
       private void ExpandCommand(object sender, RoutedEventArgs e)
       {
           if (dataGrid.SelectedItem != null)
           {

               CreateBasicTour selectedBasicTour = (CreateBasicTour)dataGrid.SelectedItem;

                int id = selectedBasicTour.id;
                string city = selectedBasicTour.city;
                string state = selectedBasicTour.state;
                string language = selectedBasicTour.language;
                int numberOfGuests = selectedBasicTour.numberOfGuests;
                DateTime? start = selectedBasicTour.start;
                DateTime? end = selectedBasicTour.end;
                string description = selectedBasicTour.description;
                Status status = selectedBasicTour.status;
                DateTime? appointment = selectedBasicTour.appointment;



                BasicTourDetailsWin basicTourDetailsWin = new BasicTourDetailsWin(id, city, state, language, numberOfGuests, start, end, description, status, appointment);
                basicTourDetailsWin.Show();
           }
       }
    }
}
