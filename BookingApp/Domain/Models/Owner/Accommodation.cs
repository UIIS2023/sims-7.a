using System;
using System.Collections.Generic;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models.Owner
{
    public enum ACCOMMODATION_TYPE
    {
        Apartment,
        House,
        Cottage
    }
    public class Accommodation : ISerializable
    {
        public int id { get; set; }

        public int idOwner { get; set; }
        public string name { get; set; }
        public int idLocation { get; set; }
        public Location location { get; set; }
        public ACCOMMODATION_TYPE type { get; set; }
        public int maxGuests { get; set; }
        public int minDaysForStay { get; set; }
        public int minDaysForCancelation { get; set; }

        public List<Image> accommodationImages { get; set; }
        public Image firstImg { get; set; }

        public Accommodation()
        {
            location = new Location();
            accommodationImages = new List<Image>();
            firstImg = new Image();
        }

        public Accommodation(int idOwner, string name, int idLocation, Location location, ACCOMMODATION_TYPE type, int maxguests, int mindaysforreservation, int mindaysforcancelation)
        {
            this.idOwner = idOwner;
            this.name = name;
            this.idLocation = idLocation;
            this.location = location;
            this.location = location;
            this.type = type;
            this.maxGuests = maxguests;
            this.minDaysForStay = mindaysforreservation;
            this.minDaysForCancelation = mindaysforcancelation;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                idLocation.ToString(),
                type.ToString(),
                maxGuests.ToString(),
                minDaysForStay.ToString(),
                minDaysForCancelation.ToString(),
                idOwner.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = values[1];
            idLocation = Convert.ToInt32(values[2]);
            type = (ACCOMMODATION_TYPE)Enum.Parse(typeof(ACCOMMODATION_TYPE), values[3]);
            maxGuests = Convert.ToInt32(values[4]);
            minDaysForStay = Convert.ToInt32(values[5]);
            minDaysForCancelation = Convert.ToInt32(values[6]);
            idOwner = Convert.ToInt32(values[7]);
        }
    }
}
