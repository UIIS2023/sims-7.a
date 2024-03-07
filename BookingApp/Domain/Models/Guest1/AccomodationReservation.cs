using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.Models;     
using InitialProject.Serializer;
using System;
using System.Globalization;

namespace BookingApp.Domain.Models.Guest1
{
    public class AccommodationReservation : ISerializable
    {
        public int idGuest { get; set; }
        public User guest { get; set; } 
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }  
        public Accommodation accomodation { get; set; }
        public int guestNumber { get; set; }

        public bool isRated { get; set; }


        public int zIndex //sets the z index of the buttons based on the fact that the reservation is currently active or is a past reservation
        {
            get
            {
                if (startDate.Date >= DateTime.Now.Date) return 1;
                return -1;
            }
        }

        public string currentOrPast
        {
            get
            {
                if (startDate.Date >= DateTime.Now.Date) return "Current";
                return "Past";
            }
        }

        public bool isCancelable
        {
            get
            {
                if (startDate.Subtract(DateTime.Now).Days > accomodation.minDaysForCancelation) return true;
                return false;
            }

        }

        public bool isReviewable
        {

            get
            {
                if (DateTime.Now.Date > endDate.Date && DateTime.Now.Date.Subtract(endDate.Date).Days <= 5) return true;
                return false;
            }

        }


        public AccommodationReservation() 
        {
            accomodation = new Accommodation();
            guest = new User();
        }

        public AccommodationReservation(DateTime startDate, DateTime endDate, Accommodation accomodation, int idGuest,int guestNumber, bool isRated) 
        {
            this.accomodation = accomodation;
            this.startDate = startDate;
            this.endDate = endDate;
            this.idGuest = idGuest;
            this.guestNumber = guestNumber;
            this.isRated = isRated;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            id.ToString(),
            startDate.ToString("dd'/'MM'/'yyyy"),
            endDate.ToString("dd'/'MM'/'yyyy"),
            accomodation.id.ToString(),
            idGuest.ToString(),
            guestNumber.ToString(),
            isRated.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            startDate = DateTime.ParseExact(values[1], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            endDate = DateTime.ParseExact(values[2], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            accomodation.id = int.Parse(values[3]);
            idGuest = int.Parse(values[4]);
            guestNumber = int.Parse(values[5]);
            isRated = bool.Parse(values[6]);
        }

    }
}
