using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.Models;
using BookingApp.Application.UseCases.Guest1;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationWin.xaml
    /// </summary>
    public partial class ReservationWin : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        public Accommodation accommodation { get; set; }

        public struct DateSpan
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public List<DateSpan> freeDates { get; set; }


        public DateTime searchDateStart { get; set; }
        public DateTime searchDateEnd { get; set; }
        public int daysForReservation { get; set; }
        public int guestNumber { get; set; }

        private int userId;

        private List<AccommodationReservation> reservationList;
        private ReservationService reservationService;


        private List<BookingApp.Domain.Models.Image> images;
        public BookingApp.Domain.Models.Image currentImage { get; set; }
        private int currentImageIndex;


        public ReservationWin(Accommodation accommodation, int userId)
        {
            InitializeComponent();
            this.DataContext = this;

            this.accommodation = accommodation;
            this.userId = userId;

            searchDateStart = DateTime.Now;
            searchDateEnd = DateTime.Now;
            reservationService = new ReservationService();


            images = accommodation.accommodationImages;

            if (images.Count > 0)
            currentImage = images.First();

            currentImageIndex = 0;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {

            if (!AreEnteredValuesValid())
            {
                MessageBox.Show("Please input correct values!");
                return;
            }

            freeDates = new List<DateSpan>(); // new initialization every time i search for free dates so if i cancel i get a new list

            CheckFreeDatesForPeriod();

            ShowFreeDatesWindow();




        }

        private bool AreEnteredValuesValid()
        {
            bool isEndDateAhead = searchDateStart < searchDateEnd;
            bool isPeriodValid = searchDateEnd.Subtract(searchDateStart).Days >= daysForReservation; // period = end - start , period >= reservation days
            bool isReservationDaysValid = daysForReservation > 0;
            bool isGuestNumberValid = guestNumber > 0;

            return isEndDateAhead && isPeriodValid && isReservationDaysValid && isGuestNumberValid;

        }


        private void ShowFreeDatesWindow()
        {
            var freeDateWindow = new FreeDatesWin(freeDates, accommodation, userId, guestNumber, this);
            freeDateWindow.ShowDialog();
        }

        private void CheckFreeDatesForPeriod()
        {
            double period = 0;

            while (freeDates.Count == 0) // if there are no free dates in the current date span search the next one 
            {
                UpdateFreeDates(searchDateStart.AddDays(period), searchDateEnd.AddDays(period)); // first iteration period is 0 so it searches the first period
                period = searchDateEnd.Subtract(searchDateStart).Days; // period = DateEnd - DateStart
                if (freeDates.Count == 0) MessageBox.Show("No free dates for the selected period, we recommend the following dates!");
            }


        }

        private void UpdateFreeDates(DateTime startDate, DateTime endDate)
        {


            DateTime potentialDateStart = startDate;
            DateTime potentialDateEnd = startDate.AddDays(daysForReservation);

            while (endDate.Date >= potentialDateEnd.Date) // .Date so it doesnt include time
            {

                bool isDateConflicted = DateConflictExists(potentialDateStart, potentialDateEnd);

                AddFreeDatesToList(potentialDateStart, potentialDateEnd, isDateConflicted);

                potentialDateStart = potentialDateEnd.AddDays(1);
                potentialDateEnd = potentialDateStart.AddDays(daysForReservation);

            }



        }

        private bool DateConflictExists(DateTime potentialDateStart, DateTime potentialDateEnd)
        {

            reservationList = reservationService.GetByAccommodationId(accommodation.id);

            foreach (var reservation in reservationList)
            {
                bool areDatesConflicted = reservation.startDate.Date <= potentialDateEnd.Date && potentialDateStart.Date <= reservation.endDate.Date;

                if (areDatesConflicted)
                    return true;

            }

            return false;

        }

        private void AddFreeDatesToList(DateTime potentialDateStart, DateTime potentialDateEnd, bool isDateConflicted)
        {
            if (isDateConflicted) return;

            var tempDateSpan = new DateSpan();
            tempDateSpan.Start = potentialDateStart;
            tempDateSpan.End = potentialDateEnd;
            freeDates.Add(tempDateSpan);

        }

        private void NextImgRight_Click(object sender, RoutedEventArgs e)
        {

            if (++currentImageIndex >= images.Count)
                currentImageIndex = 0;

            currentImage = images[currentImageIndex];

            OnPropertyChanged();


        }

        private void NextImgLeft_Click(object sender, RoutedEventArgs e)
        {
            if (--currentImageIndex < 0)
                currentImageIndex = images.Count - 1;

            currentImage = images[currentImageIndex];

            OnPropertyChanged();

        }

        public string Error => null;


        public string this[string columnName]
        {
            get
            {
                bool isPeriodValid = searchDateEnd.Subtract(searchDateStart).Days >= daysForReservation;
                string msg = null;
                switch (columnName)
                {
                    case "searchDateStart":         

                        if (!isPeriodValid || searchDateEnd.Date == searchDateStart.Date)
                            msg = "Must add atleast one image!";
                        break;
                    case "searchDateEnd":
                        bool isEndDateAhead = searchDateStart < searchDateEnd;

                        if ((!isEndDateAhead && !isPeriodValid) || searchDateEnd.Date == searchDateStart.Date)
                            msg = "Comment cannot be empty or white space!";
                        break;
                    case "daysForReservation":
                        if (daysForReservation == 0)
                            msg = "You must chose a renovation urgency level!";
                        break;
                    case "guestNumber":
                        if (guestNumber == 0)
                            msg = "You must leave a renovation proposal comment!";
                        break;

                }
                return msg;
            }
        }
    }
}
