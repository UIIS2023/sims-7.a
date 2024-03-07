using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using BookingApp.WPF.Views.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    class MyToursViewModel
    {
        private TourAppointmentService _tourAppointmentService;
        public int guideId { get; set; }
        public ObservableCollection<TourAppointment> GuidesAppointments { get; set; }
        public TourAppointment SelectedAppointment { get; set; }
        public ICommand AddTourClick { get; private set; }
        public ICommand TodaysToursClick { get; private set; }
        public ICommand RemoveTourClick { get; private set; }
        public MyToursViewModel(int guideId)
        {
            this.guideId = guideId;
            _tourAppointmentService = new TourAppointmentService();
            GuidesAppointments = new ObservableCollection<TourAppointment>(_tourAppointmentService.GetAllByGuide(guideId));
            AddTourClick = new RelayCommand(AddTour_Click);
            TodaysToursClick = new RelayCommand(TodaysTours_Click);
            RemoveTourClick = new RelayCommand(RemoveTour_Click);
        }

        private void RemoveTour_Click()
        {
            if (!_tourAppointmentService.CancelTour(SelectedAppointment))
            {
                MessageBox.Show("You can't cancel this tour !");
                return;
            }
            GuidesAppointments.Remove(SelectedAppointment);
        }

        private void TodaysTours_Click()
        {
            TodaysToursWin todaysToursWin = new TodaysToursWin(guideId);
            todaysToursWin.ShowDialog();
        }

        private void AddTour_Click()
        {
            AddTourWin addTourWin = new AddTourWin(guideId);
            addTourWin.ShowDialog();
        }
    }
}
