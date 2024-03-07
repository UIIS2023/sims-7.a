using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.Models.Owner;
using BookingApp.Repository;
using BookingApp.Repository.Guest1;
using BookingApp.Repository.Owner;
using BookingApp.WPF.ViewModels.Guide;
using BookingApp.WPF.Views.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Owner
{


    public class OwnerViewModel : BaseViewModel
    {

        private ObservableCollection<Accommodation> _accommodations; //lista kojom punim 1.tab

        public ObservableCollection<Accommodation> accommodations
        {
            get { return _accommodations;}
            set 
            {
                _accommodations = value;
                OnPropertyChanged();
            }
        }

       
        /////////////////////////////////////////////////////////////////
     
        private ObservableCollection<AccommodationReservation> _reservationsforrate;

        public ObservableCollection<AccommodationReservation> reservationsforrate
        {
            get { return _reservationsforrate; }
            set
            {
                _reservationsforrate = value;
                OnPropertyChanged();
            }
        }

        /////////////////////////////////////////////////////////////////

        private ObservableCollection<MoveReservationRequest> _moveRequests;

        public ObservableCollection<MoveReservationRequest> moveRequests
        {
            get { return _moveRequests; }
            set
            {
                _moveRequests = value;
                OnPropertyChanged();
            }
        }

        /////////////////////////////////////////////////////////////////

       // private readonly AccommodationService accommodationService;

        public AccommodationRepository accomodationRepository;
        public ReservationRepository reservationRepository;
        public RequestRepository requestRepository;
        public ReviewRepository reviewRepository;
        public RateReservationGuestRepository rateReservationGuestRepository;
       public Accommodation selectedAccommodation;

        public AccommodationReservation selectedReservation { get; set; } //2.tab
        public MoveReservationRequest selectedRequest { get; set; } //3.tab  

        public  ICommand AddAccomodationClick { get; private set; }
        public  ICommand RateAGuestClick { get; private set; }
        public ICommand AcceptMoveClick { get; private set; }
        public ICommand DenyMoveClick { get; private set; }
        public ICommand ReviewsClick { get; private set; }
        public ICommand SuperOwnerClick { get; private set; }
        public ICommand StatisticClick { get; private set; }
        public ICommand LogOutClick { get; private set; }

        public ICommand RenovateClick { get; private set; }



        public Dictionary<string, List<string>> locations { get; set; }
        public static List<AccommodationReservation> reservations { get; set; }
        public User loggedUser { get; set; }



        public OwnerViewModel(User user)
        {
            loggedUser = user;

            //accommodationService = new AccommodationService();
            accomodationRepository = new AccommodationRepository();
            reservationRepository = new ReservationRepository();
            requestRepository = new RequestRepository();
            reviewRepository= new ReviewRepository();
            rateReservationGuestRepository= new RateReservationGuestRepository();

            //accommodations = new ObservableCollection<Accommodation>(accomodationService.GetAll());
            accommodations = new ObservableCollection<Accommodation>(accomodationRepository.GetAll());

            reservations = new List<AccommodationReservation>(reservationRepository.GetAll());
            reservationsforrate = new ObservableCollection<AccommodationReservation>(reservationRepository.ReservationsForRate(loggedUser.id));
            moveRequests = new ObservableCollection<MoveReservationRequest>(requestRepository.GetAll());

            rateReservationGuestRepository = new RateReservationGuestRepository();
            reviewRepository = new ReviewRepository();

            locations = new Dictionary<string, List<string>>();
            UpdateLocationMap();


            //DUGMAD//
            AddAccomodationClick = new RelayCommand(Add_Accomodation_Click);
            RateAGuestClick = new RelayCommand(Rate_A_Guest_Click);
            AcceptMoveClick = new RelayCommand(Accept_Move_Click);
            DenyMoveClick = new RelayCommand(Deny_Move_Click);
            ReviewsClick = new RelayCommand(Reviews_Click);
            SuperOwnerClick = new RelayCommand(Super_Owner_Click);
            StatisticClick = new RelayCommand(Statistic_Click);
            LogOutClick = new RelayCommand(Log_Out_Click);
            RenovateClick = new RelayCommand(Renovate_Click);

        }

        private Boolean CanExecute()
        {
            return true;
        }
        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////



        private void Add_Accomodation_Click()    //DUGMAD
        {
            AddAccomodationWin addAccomodationWin = new AddAccomodationWin(loggedUser);
            addAccomodationWin.Show();
        }

        private void Rate_A_Guest_Click()
        {
            RateReservationWin rateReservationWin = new RateReservationWin(selectedReservation);
            rateReservationWin.Show();
        }

        private void Accept_Move_Click()
        {
            //automatski se menjaju datumi rezervacije iz zahteva na datume iz zahteva
            //MoveReservationRequest.status se menja u Accepted
            selectedRequest.status = REQUEST_STATUS.Accepted;
            foreach (AccommodationReservation a in reservationRepository.GetAll())
            {
                if (a.id == selectedRequest.id)
                {
                    a.startDate = selectedRequest.newStartDate;
                    a.endDate = selectedRequest.newEndDate;
                    reservationRepository.Update(a);
                }

            }

        }

        private void Renovate_Click()
        {
            RenovateWin renWin = new RenovateWin(selectedAccommodation);
            renWin.Show();
        }

        private void Deny_Move_Click()
        {
            DenyRequestWin denyRequestWin = new DenyRequestWin(selectedRequest);
            denyRequestWin.Show();
        }


        private void Reviews_Click()
        {
            AccommodationReviewsWin accommodtionReviewsWin = new AccommodationReviewsWin();

            if (rateReservationGuestRepository.GetById(loggedUser.id).Any())
            {
                accommodtionReviewsWin.Show();
            }


        }

        private void Super_Owner_Click()
        {
            if (reviewRepository.isSuperOwner(loggedUser.id))
            {
                SuperOwnerWin superOwnerWin = new SuperOwnerWin();
                superOwnerWin.Show();
            }
        }

        private void Statistic_Click()
        {

        }


        private void Log_Out_Click()
        {
            LoginWin loginWin = new LoginWin();
            loginWin.Show();
            //this.Close();
            App.Current.MainWindow.Close(); //???

           
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
    }
}
