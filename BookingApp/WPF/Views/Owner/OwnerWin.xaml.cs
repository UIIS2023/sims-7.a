using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.Models;
using BookingApp.Repository;
using BookingApp.Repository.Owner;
using BookingApp.Repository.Guest1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace BookingApp.WPF.Views.Owner
{
    public partial class OwnerWin : Window, INotifyPropertyChanged
    {

        public static ObservableCollection<Accommodation> accommodations { get; set; } //lista kojom punim 1.tab
        public static ObservableCollection<AccommodationReservation> reservationsforrate { get; set; } //lista kojom punim 2.tab

        public static ObservableCollection<MoveReservationRequest> moveRequests { get; set; } //lista kojom punim 3.tab
        
        public static List<AccommodationReservation> reservations { get; set; }



        public AccommodationRepository accomodationRepository;
        public ReservationRepository reservationRepository;
        public RequestRepository requestRepository;
        public ReviewRepository reviewRepository;
        public RateReservationGuestRepository rateReservationGuestRepository;

        public AccommodationReservation selectedReservation { get; set; } //2.tab
        public MoveReservationRequest selectedRequest { get; set; } //3.tab  

      

        public Dictionary<string, List<string>> locations { get; set; }  

        public static User loggedUser { get; set; }


        public OwnerWin(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            loggedUser = user;

            accomodationRepository = new AccommodationRepository();
            reservationRepository = new ReservationRepository();
            requestRepository = new RequestRepository();

            accommodations = new ObservableCollection<Accommodation>(accomodationRepository.GetAll());
            reservations = new List<AccommodationReservation>(reservationRepository.GetAll());
            reservationsforrate = new ObservableCollection<AccommodationReservation>(reservationRepository.ReservationsForRate(loggedUser.id));
            moveRequests = new ObservableCollection<MoveReservationRequest>(requestRepository.GetAll());

            rateReservationGuestRepository = new RateReservationGuestRepository();
            reviewRepository= new ReviewRepository();

            locations = new Dictionary<string, List<string>>();
            UpdateLocationMap();
        }




        private void UpdateLocationMap()  //puni mapu 
        {

            LocationRepository locationRepository = new LocationRepository();

            var locationsFromFile = locationRepository.GetAll();


            foreach (var location in locationsFromFile)
            {
                String country = location.country;

                if (!locations.ContainsKey(country))
                {
                    locations[country] = new List<string>();

                }

                locations[country].Add(location.city);


            }


        }

        private void Add_Accomodation_Click(object sender, RoutedEventArgs e)    //DUGMAD
        {
            AddAccomodationWin addAccomodationWin = new AddAccomodationWin(loggedUser);
            addAccomodationWin.Show();
        }

        private void Rate_A_Guest_Click(object sender, RoutedEventArgs e)
        {
            RateReservationWin rateReservationWin = new RateReservationWin(selectedReservation);
            rateReservationWin.Show();
        }

        private void Accept_Move_Click(object sender, RoutedEventArgs e)
        {
            //automatski se menjaju datumi rezervacije iz zahteva na datume iz zahteva
            //MoveReservationRequest.status se menja u Accepted
            selectedRequest.status = REQUEST_STATUS.Accepted;
            foreach(AccommodationReservation a in reservationRepository.GetAll())
            {
                if(a.id == selectedRequest.id)
                {
                    a.startDate = selectedRequest.newStartDate;
                    a.endDate = selectedRequest.newEndDate;
                    reservationRepository.Update(a);
                }

            }
           
            

        }

        private void Deny_Move_Click(object sender, RoutedEventArgs e)
        {
            DenyRequestWin denyRequestWin = new DenyRequestWin(selectedRequest);
            denyRequestWin.Show();
        }


        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReviewsWin accommodtionReviewsWin = new AccommodationReviewsWin();

            if (rateReservationGuestRepository.GetById(loggedUser.id).Any())
            {
                accommodtionReviewsWin.Show();
            }

           
        }

        private void superOwner_Click(object sender, RoutedEventArgs e)
        {
            if (reviewRepository.isSuperOwner(loggedUser.id))
            {
                SuperOwnerWin superOwnerWin = new SuperOwnerWin();
                superOwnerWin.Show();
            }
        }

        

        private void Log_Out_Click(object sender, RoutedEventArgs e)
        {
            LoginWin loginWin = new LoginWin();
            loginWin.Show();
            this.Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;    //za INotyfyPropertyChanged
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}

