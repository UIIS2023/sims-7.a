using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace BookingApp.View.Guide
{  
    public partial class LiveTourWin : Window, INotifyPropertyChanged
    {
        public TourAppointment LiveTourAppointment { get; set; }
        private TourPointController _tourPointController;
        private TourAttendenceService _tourAttendenceService;
        public TourPoint SelectedTourPoint { get; set; }
        public TourAttendence SelectedAttendence { get; set; }
        public TourPoint CurrentPoint;
        public ObservableCollection<TourPoint> TourPoints { get; set; }
        public ObservableCollection<TourAttendence> Guests { get; set; }
        private string _tourName = "";
        public string TourName
        {
            get { return _tourName; }
            set
            {
                {
                    if (value != _tourName)
                    {
                        _tourName = value;
                        OnPropertyChanged(nameof(TourName));
                    }
                }
            }
        }

        private readonly TourAppointmentController _tourAppointmentController;
       
        public LiveTourWin(int guideId)
        {
            InitializeComponent();
            this.DataContext = this;
           
            _tourAppointmentController = new TourAppointmentController();
            _tourPointController = new TourPointController();
            _tourAttendenceService = new TourAttendenceService();

            LiveTourAppointment = _tourAppointmentController.GetLiveTour(guideId);
            this.TourName = LiveTourAppointment.tour.name;

            TourPoints = new ObservableCollection<TourPoint>(LiveTourAppointment.tourPoints);
            Guests = new ObservableCollection<TourAttendence>(_tourAttendenceService.GetAll(LiveTourAppointment).Where(a=> a.requestSent == false));
            CurrentPoint = new TourPoint();
            CurrentPoint = _tourPointController.FindCurrentPoint(LiveTourAppointment);

            tourPointsDataGrid.CellEditEnding += SetCheckPoint;
        }
        private void SetCheckPoint(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(SelectedTourPoint.pointType == PointType.End)
            {
                _tourAppointmentController.EndTour(LiveTourAppointment);
                return;
            }
            _tourPointController.SetCheckPoint(SelectedTourPoint);
            CurrentPoint = SelectedTourPoint;
        }
        private void EndTour_Click(object sender, RoutedEventArgs e)
        {
            _tourAppointmentController.EndTour(LiveTourAppointment);
            Close();
        }

        private void Present_Click(object sender, RoutedEventArgs e)
        {
            _tourAttendenceService.SendNotification(SelectedAttendence, CurrentPoint);
            Guests.Remove(SelectedAttendence);
        }


        
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
