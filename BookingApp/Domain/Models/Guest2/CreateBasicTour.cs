using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models.Guest2
{
    public enum Status
    {
        ACCEPTED,
        ON_WAIT,
        INVALID
    }
    public class CreateBasicTour : ISerializable
    {
        public int id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string language { get; set; }
        public int numberOfGuests { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string description { get; set; }
        public Status status { get; set; }
        public DateTime? appointment { get; set; }   

        public CreateBasicTour() { }
        public CreateBasicTour(int id, string city, string state, string language, int numberOfGuests, string description, DateTime start, DateTime end, Status status, DateTime? appointment)
        {
            this.id = id;
            this.city = city;
            this.state = state;
            this.language = language;
            this.numberOfGuests = numberOfGuests;
            this.description = description;
            this.start = start;
            this.end = end;
            this.status = status;
            this.appointment = appointment; 
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                city,
                state,
                language,
                numberOfGuests.ToString(),
                description,
                start.ToString(),
                end.ToString(),
                status.ToString(),
                appointment.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            city = Convert.ToString(values[1]);
            state = Convert.ToString(values[2]);
            language = Convert.ToString(values[3]);
            numberOfGuests = Convert.ToInt32(values[4]);
            description = Convert.ToString(values[5]);
            start = Convert.ToDateTime(values[6]);
            end = Convert.ToDateTime(values[7]);
            status = Enum.Parse<Status>(values[8]);
            appointment = Convert.ToDateTime(values[9]);
        }
    }
}
