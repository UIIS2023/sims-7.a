using BookingApp.Application.UseCases.Guide;
using BookingApp.Controller;
using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class AcceptBasicTourViewModel : BaseViewModel
    {
        private string _name = string.Empty;
        public string NameBind
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(NameBind));
                }
            }
        }
        private DateTime _starttime;
        public DateTime StartTime
        {
            get => _starttime;
            set
            {
                if (value != _starttime)
                {
                    _starttime = value;
                    OnPropertyChanged(nameof(StartTime));
                }
            }
        }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }
        private string _startpointname = string.Empty;
        public string StartPointName
        {
            get => _startpointname;
            set
            {
                if (value != _startpointname)
                {
                    _startpointname = value;
                    OnPropertyChanged(nameof(StartPointName));
                }
            }
        }
        private string _midpointname = string.Empty;
        public string MidPointName
        {
            get => _midpointname;
            set
            {
                if (value != _midpointname)
                {
                    _midpointname = value;
                    OnPropertyChanged(nameof(MidPointName));
                }
            }
        }
        private string _endpointname = string.Empty;
        public string EndPointName
        {
            get => _endpointname;
            set
            {
                if (value != _endpointname)
                {
                    _endpointname = value;
                    OnPropertyChanged(nameof(EndPointName));
                }
            }
        }
        private List<String> _midPointNames = new List<String>();
        public List<String> MidPointNames
        {
            get => _midPointNames;
            set
            {
                if (value != _midPointNames)
                {
                    _midPointNames = value;
                    OnPropertyChanged(nameof(MidPointNames));
                }
            }
        }
        
        private List<String> _images = new List<String>();
        public List<String> Images
        {
            get => _images;
            set
            {
                if (value != _images)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }
        private List<DateTime> _dates = new List<DateTime>();
        public List<DateTime> Dates
        {
            get => _dates;
            set
            {
                if (value != _dates)
                {
                    _dates = value;
                    OnPropertyChanged(nameof(Dates));
                }
            }
        }
        public CreateBasicTour SelectedTour { get; set; }
        private readonly TourController _tourController;
        private readonly TourRequestService _tourRequestService;
        private readonly TourAppointmentService _tourAppointmentService;
        public ICommand ConfirmClick { get; private set; }
        public int guideId { get; set; }


        public AcceptBasicTourViewModel(int guideId, CreateBasicTour requestedTour) 
        {
            SelectedTour = new CreateBasicTour();
            SelectedTour = requestedTour;
            _tourController = new TourController();
            ConfirmClick = new RelayCommand(Confirm_Click);
            this.guideId = guideId;
            _tourRequestService = new TourRequestService();
            _tourAppointmentService = new TourAppointmentService();
        }
        private void Confirm_Click()
        {
            if(_tourRequestService.CheckDateAvailability(StartTime, guideId, Duration, _tourAppointmentService.GetAllByGuide(guideId)))
            {
                Dates.Add(StartTime);
                _tourController.Create(guideId, NameBind, SelectedTour.description, SelectedTour.language, StartTime, Duration,
                    SelectedTour.city, SelectedTour.state, 20, StartPointName, MidPointNames, EndPointName, Dates, Images, false, false);

                SelectedTour.status = Status.ACCEPTED;
                _tourRequestService.Update(SelectedTour);
                return;
            }
            MessageBox.Show("You already have tour at this time ! ");
            

        }
    }
}
