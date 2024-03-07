using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models.Guest2
{
    public enum StatusComplex
    {
        ON_WAIT,
        ACCEPTED,
        INVALID
    }

    public class CreateComplexTour
    {
     
        public int idComplex { get; set; }       
        public List<CreateComplexTour> Stations { get; set; }
        public int id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string language { get; set; }
        public int numberOfGuests { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string description { get; set; }
        public StatusComplex status { get; set; }
        public DateTime? appointment { get; set; }
      

        public CreateComplexTour()
        {
            Stations = new List<CreateComplexTour>();
            
        }

        public CreateComplexTour( int idComplex, int id, string city, string state, string language, int numberOfGuests, string description, DateTime start, DateTime end, StatusComplex status, DateTime? appointment)
        {
            this.idComplex = idComplex;
            this.id = id;
            this.city = city;
            this.state = state;
            this.language = language;
            this.numberOfGuests = numberOfGuests;
            this.description = description;
            this.start = start;
            this.end = end;
            this.status = status;
            
        }
    }
}

