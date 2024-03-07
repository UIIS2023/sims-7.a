using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
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
    class TodaysToursViewModel : BaseViewModel
    {
        private TourAppointmentService _tourAppointmentService;
        public ObservableCollection<TourAppointment> GuidesAppointments { get; set; }
        public ICommand StartTourClick { get; private set; }
        public ICommand LiveTourClick { get; private set; }
        public TourAppointment SelectedAppointment { get; set; }
        public int guideId { get; set; }
        public TodaysToursViewModel(int guideId) 
        {
            _tourAppointmentService = new TourAppointmentService();
            GuidesAppointments = new ObservableCollection<TourAppointment>(_tourAppointmentService.GetTodaysAppointmentsByGuide(guideId));
            StartTourClick = new RelayCommand(StartTour_Click);
            LiveTourClick = new RelayCommand(LiveTour_Click);
            this.guideId = guideId;
        }

        private void LiveTour_Click()
        {
            if (!_tourAppointmentService.HasGuideStartedTour(guideId))
            {
                MessageBox.Show("You don't have a started tour ! ");
                return;
            }
            LiveTourWin liveTourWin = new LiveTourWin(guideId);
            liveTourWin.ShowDialog();

        }

        private void StartTour_Click()
        {
            if (_tourAppointmentService.HasGuideStartedTour(guideId))
            {
                MessageBox.Show("You already have a tour started ! ");
                return;
            }
            if (SelectedAppointment != null)
            {
                _tourAppointmentService.StartTour(SelectedAppointment.id);
                LiveTourWin liveTourWin = new LiveTourWin(guideId);
                liveTourWin.ShowDialog();
            }
        }
    }
}
