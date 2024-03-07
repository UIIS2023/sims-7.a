using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Functionality
{
    public class TourFunctionality : INotifyPropertyChanged
    {
        private string searchLocation;
        private int searchDuration;
        private string searchLanguage;
        private int searchGuestNumber;
      
        TourAppointmentService tourAppointmentService = new TourAppointmentService();
        VoucherRepository voucherRepository = new VoucherRepository();
        private int userId;

        public ObservableCollection<TourAppointment> tours { get; set; }
        public ObservableCollection<Voucher> vouchers { get; set; }
        public ObservableCollection<TourAppointment> alternativeTours { get; set; } = new ObservableCollection<TourAppointment>();
        public TourFunctionality( int userId)
        {    
            this.userId = userId;
            tours = new ObservableCollection<TourAppointment>(tourAppointmentService.GetAll());
            vouchers = new ObservableCollection<Voucher>(voucherRepository.GetVouchersByUserId(userId));
            

        }

        public void SetAlternativeTours(IEnumerable<TourAppointment> tours)
        {
            alternativeTours.Clear();
            foreach (var tour in tours)
            {
                alternativeTours.Add(tour);
            }
        }

        public string SearchLocation
        {
            get { return searchLocation; }
            set
            {
                searchLocation = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }

        public int SearchDuration
        {
            get { return searchDuration; }
            set
            {
                searchDuration = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        public string SearchLanguage
        {
            get { return searchLanguage; }
            set
            {
                searchLanguage = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        public int SearchGuestNumber
        {
            get { return searchGuestNumber; }
            set
            {
                searchGuestNumber = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }

        public ObservableCollection<TourAppointment> filteredData
        {
            get
            {
                var result = tours;

                if (!string.IsNullOrEmpty(searchLocation))
                {
                    result = new ObservableCollection<TourAppointment>(result.Where(a => a.tour.Location.city.ToLower().Contains(searchLocation) || a.tour.Location.country.ToLower().Contains(searchLocation)));
                }

                if (searchDuration != 0)
                {
                    result = new ObservableCollection<TourAppointment>(result.Where(a => a.tour.duration == searchDuration));
                }

                if (!string.IsNullOrEmpty(searchLanguage))
                {
                    result = new ObservableCollection<TourAppointment>(result.Where(a => a.tour.language.ToLower().Contains(searchLanguage)));
                }

                if (searchGuestNumber != 0)
                {
                    result = new ObservableCollection<TourAppointment>(result.Where(a => a.tour.maxGuests >= searchGuestNumber));
                }

                return result;
            }
        }
   
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
