using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;


namespace BookingApp.Model
{

    public class Tour : ISerializable
    {

        public int id { get; set; } = -1;
        public string name { get; set; }
        public string language { get; set; }
        public string description { get; set; }
        public int maxGuests { get; set; }     
        public double duration { get; set; }
        public Location Location { get; internal set; }
        public List<TourPoint> TourPoints { get; set; }
        public int guideId { get; set; } = -1;
        public List<Image> tourImages { get; set; }
        public Image image { get; set; }


        public Tour() 
        {
            tourImages = new List<Image>();
            image = new Image();
        }

        public Tour(int guideId, string name, string description, string language, int duration, string city, string country, int maxGuests)
        {
            this.name = name;
            this.description = description;
            this.language = language;         
            this.duration = duration;
            this.Location = new Location(city, country);
            this.maxGuests = maxGuests;
            TourPoints = new List<TourPoint>();
            this.guideId = guideId;
          
           
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                description,
                language.ToString(),             
                duration.ToString(),
                Location.city,
                Location.country,
                maxGuests.ToString(),
                guideId.ToString(),
              
                
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = Convert.ToString(values[1]);
            description = Convert.ToString(values[2]);
            language = Convert.ToString(values[3]);           
            duration = Convert.ToInt32(values[4]);
            Location = new Location(values[5], values[6]);
            maxGuests = Convert.ToInt32(values[7]);
            guideId = Convert.ToInt32(values[8]);
            
        }
    }
}
