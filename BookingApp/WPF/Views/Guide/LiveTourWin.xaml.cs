using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Service;
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

namespace BookingApp.WPF.ViewModels.Guide
{
    /// <summary>
    /// Interaction logic for LiveTourWin.xaml
    /// </summary>
    public partial class LiveTourWin : Window
    {
        public TourAppointment LiveTourAppointment { get; set; }
        private TourPointController _tourPointController;
        private TourAttendenceService _tourAttendenceService;
        public TourPoint SelectedTourPoint { get; set; }
        public TourAttendence SelectedAttendence { get; set; }
        public TourPoint CurrentPoint;
        public ObservableCollection<TourPoint> TourPoints { get; set; }
        public ObservableCollection<TourAttendence> Guests { get; set; }
        private readonly TourAppointmentController _tourAppointmentController;

        public LiveTourWin(int guideId)
        {
            InitializeComponent();
            DataContext = this;

            InitializeComponent();
            this.DataContext = this;

            _tourAppointmentController = new TourAppointmentController();
            _tourPointController = new TourPointController();
            _tourAttendenceService = new TourAttendenceService();

            LiveTourAppointment = _tourAppointmentController.GetLiveTour(guideId);

            TourPoints = new ObservableCollection<TourPoint>(LiveTourAppointment.tourPoints);
            Guests = new ObservableCollection<TourAttendence>(_tourAttendenceService.GetAll(LiveTourAppointment).Where(a => a.requestSent == false));
            CurrentPoint = new TourPoint();
            CurrentPoint = _tourPointController.FindCurrentPoint(LiveTourAppointment);

            tourPointsDataGrid.CellEditEnding += SetCheckPoint;
        }

        private void SetCheckPoint(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (SelectedTourPoint.pointType == PointType.End)
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
