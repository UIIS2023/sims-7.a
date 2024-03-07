using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guide;
using BookingApp.WPF.Views.Guide;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;


namespace BookingApp.View
{
    
    public partial class GuideWin : Window, INotifyPropertyChanged
    {
        private readonly TourAppointmentController _tourAppointmentController;
        private readonly TourController _tourController;
        private readonly TourAppointmentService _tourAppointmentService;
        public TourAppointment SelectedAppointment { get; set; } 
        public ObservableCollection<TourAppointment> GuidesAppointments { get; set; }
        public int guideId { get; set; }
        
        public GuideWin(int guideId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.guideId = guideId;

            _tourAppointmentController = new TourAppointmentController();
            _tourController = new TourController();
            _tourAppointmentService = new TourAppointmentService();

            GuidesAppointments = new ObservableCollection<TourAppointment>(_tourAppointmentController.GetAllByGuide(guideId));
        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            AddTourWin addTourWin = new AddTourWin(guideId);
            addTourWin.Owner= this;
            addTourWin.ShowDialog();
        }

        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            if (_tourAppointmentController.HasStartedTour(guideId))
            {
                MessageBox.Show("You already have a tour started ! ");
                return;
            }
            if(SelectedAppointment.startTime.Date != DateTime.Today.Date)
            {
                MessageBox.Show("You can't start this tour ! ");
                return;
            }
            if(SelectedAppointment != null)
            {
                _tourAppointmentController.StartTour(SelectedAppointment.id);
                LiveTourWin liveTourWin = new LiveTourWin(guideId);
                liveTourWin.ShowDialog();
            }
        }
        private void LiveTour_Click(object sender, RoutedEventArgs e)
        {
            if (!_tourAppointmentController.HasStartedTour(guideId))
            {
                MessageBox.Show("You don't have a started tour ! ");
                return;
            }
            LiveTourWin liveTourWin = new LiveTourWin(guideId);
            liveTourWin.ShowDialog();
        }

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (!_tourAppointmentController.CancelTour(SelectedAppointment))
            {
                MessageBox.Show("You can't cancel this tour !");
                return;
            }
            GuidesAppointments.Remove(SelectedAppointment);
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {
            TourStatisticsView tourStatisticsView = new TourStatisticsView(guideId);
            tourStatisticsView.ShowDialog();
        }

        private void TourFeedbacks_Click(object sender, RoutedEventArgs e)
        {
            TourFeedbackView tourFeedbackView = new TourFeedbackView(guideId);
            tourFeedbackView.ShowDialog();
        }
    }
}
