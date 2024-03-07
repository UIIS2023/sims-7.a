using BookingApp.Domain.DTO;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class TourStatisticsViewModel : INotifyPropertyChanged
    {
        public ICommand Statistics { get; private set; }
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourAttendenceService _tourAttendenceService;
        private int guideId { get; set; }

        private ObservableCollection<TourAppointment> _finishedTours;
        public ObservableCollection<TourAppointment> FinishedTours
        {
            get { return _finishedTours; }
            set
            {
                _finishedTours = value;
                OnPropertyChanged(nameof(FinishedTours));
            }
        }
        private TourStatisticsDTO _tourStatistics;

        public TourStatisticsDTO TourStatistics
        {
            get { return _tourStatistics; }
            set
            {
                _tourStatistics = value;
                OnPropertyChanged(nameof(TourStatistics));
            }
        }
        private TourAppointment _mostVisitedTour;

        public TourAppointment MostVisitedTour
        {
            get { return _mostVisitedTour; }
            set
            {
                _mostVisitedTour = value;
                OnPropertyChanged(nameof(MostVisitedTour));
            }
        }
        private TourAppointment _selectedTour;
        public TourAppointment SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }
        

        public TourStatisticsViewModel(int guideId)
        {
            _tourAppointmentService = new TourAppointmentService();
            _tourAttendenceService = new TourAttendenceService();
            this.guideId = guideId;
            
            FinishedTours = new ObservableCollection<TourAppointment>(_tourAppointmentService.GetFinishedAppointmentsByGuide(guideId));
            MostVisitedTour = new TourAppointment();
            MostVisitedTour = _tourAppointmentService.MostAttendedTour(guideId);
            Statistics = new RelayCommand(ShowStatistics);
            TourStatistics = new TourStatisticsDTO();
        }

        private void ShowStatistics()
        {
            TourStatistics = _tourAttendenceService.GetStatistics(SelectedTour);
        }
        



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
