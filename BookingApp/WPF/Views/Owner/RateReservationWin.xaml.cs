using BookingApp.Domain.Models.Owner;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Repository.Owner;
using System;

namespace BookingApp.WPF.Views.Owner
{
    public partial class RateReservationWin : Window, INotifyPropertyChanged
    {
        private readonly RateReservationGuestRepository rateReservationGuestRepository;  /// ///
        public AccommodationReservation selectedReservation { get; set; }
        public RateReservationWin(AccommodationReservation selected)
        {
            InitializeComponent();
            DataContext = this;
            rateReservationGuestRepository = new RateReservationGuestRepository();
            selectedReservation = new AccommodationReservation();
            selectedReservation = selected;
        }


        private int _tidiness;
        public int Tidiness
        {
            get => _tidiness;
            set
            {
                if (_tidiness != value)
                {
                    _tidiness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _rules;
        public int RulesFollowed
        {
            get => _rules;
            set
            {
                if (_rules != value)
                {
                    _rules = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }


       

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            RateReservation ratedReservation= new RateReservation(selectedReservation, Tidiness, RulesFollowed, Comment);
            RateReservation ratedReservation1 = rateReservationGuestRepository.Save(ratedReservation);

            this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
