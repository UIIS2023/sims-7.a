using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.Models.Guest1;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models.Owner

{
    public class RateReservation : ISerializable
    {
        public int id { get; set; }

        public AccommodationReservation accommodationReservation { get; set; }
        public int tidiness { get; set; }
        public int rulesFollowed { get; set; }
        public string comment { get; set; }

        public List<bool> list1 { get; set; }
        public List<bool> list2 { get; set; }

        public List<bool> ReviewStarsCleanliness
        {
            get
            {
                for (int i = 1; i < 6; i++)
                {
                    if (i <= tidiness) list1[i - 1] = true;
                }

                return list1;

            }

            set => list1 = value;
        }

        public List<bool> ReviewStarsRulesFollowed
        {
            get
            {
                for (int i = 1; i < 6; i++)
                {
                    if (i <= rulesFollowed) list2[i - 1] = true;
                }

                return list2;

            }

            set => list2 = value;
        }
        
        

        public RateReservation()
        { 
           accommodationReservation = new AccommodationReservation();

           list1 = Enumerable.Repeat(false, 5).ToList();
           list2 = Enumerable.Repeat(false, 5).ToList();
           

        }
        
        public RateReservation(AccommodationReservation accommodationReservation, int tidiness, int rulesFollowed, string comment)
        {
            this.accommodationReservation = accommodationReservation;  
            this.tidiness = tidiness;
            this.rulesFollowed = rulesFollowed;
            this.comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csValues =
            {
                id.ToString(),
                tidiness.ToString(),
                rulesFollowed.ToString(),
                comment,
                accommodationReservation.id.ToString()
            };
            return csValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            tidiness= int.Parse(values[1]);
            rulesFollowed= int.Parse(values[2]);    
            comment = values[3];
            accommodationReservation.id = int.Parse(values[4]);
        }
    }
}