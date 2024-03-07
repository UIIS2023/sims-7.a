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
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.WPF.ViewModels.Guest1;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for MoveReservationWin.xaml
    /// </summary>
    public partial class MoveReservationWin : Window,ICloseable
    {
        private MoveReservationViewModel moveReservationViewModel;

        public MoveReservationWin(AccommodationReservation reservation)
        {
            InitializeComponent();
            moveReservationViewModel = new MoveReservationViewModel(reservation);
            this.DataContext = moveReservationViewModel;

            moveReservationViewModel.GetValidDates(StartDatePicker);
            moveReservationViewModel.GetValidDates(EndDatePicker);

            StartDatePicker.SelectedDateChanged += (e, o) =>
            {
                if(StartDatePicker.SelectedDate.Value!=null)
                    moveReservationViewModel.GetValidEndDates(EndDatePicker,StartDatePicker.SelectedDate.Value);
            };
        }

    }
}
