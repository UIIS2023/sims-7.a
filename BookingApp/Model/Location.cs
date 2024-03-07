using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Location : ISerializable
    {
        public int idLocation { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public Location() { }

        public Location(string city, string country)
        {
            this.city = city;
            this.country = country;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                {
                    idLocation.ToString(),
                    country,
                    city
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idLocation = int.Parse(values[0]);
            country = values[1];
            city = values[2];
        }

        public override string ToString()
        {
            return string.Format(
                "{0},{1}"
                ,country,city);
        }
    }
}
