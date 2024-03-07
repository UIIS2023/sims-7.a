using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models.Owner
{
    public class Renovation: ISerializable
    {
        public int id { get; set; }
        public Accommodation accommodation { get; set; }   
        public string renovationComment { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public Renovation() 
        { 
            accommodation= new Accommodation();
        }

        public Renovation(int id, Accommodation accommodation, string renovationComment, DateTime startDate, DateTime endDate)
        {
            this.id = id;
            this.accommodation = accommodation;
            this.renovationComment = renovationComment;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                accommodation.id.ToString(),
                renovationComment,
                startDate.ToString(),
                endDate.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            accommodation.id= Convert.ToInt32(values[1]);
            renovationComment = values[2];
            startDate= Convert.ToDateTime(values[3]);
            endDate= Convert.ToDateTime(values[4]);

        }
    }
}
