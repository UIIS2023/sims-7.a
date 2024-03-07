using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models.Guest1
{
    public class RenovationProposal:ISerializable
    {
        public int id { get; set; }

        public AccommodationReservation accommodationReservation { get; set; }

        public int renovationUrgency { get; set; }
        public string renovationComment { get; set; }

        public RenovationProposal()
        {
            accommodationReservation = new AccommodationReservation();
        }

        public RenovationProposal(AccommodationReservation reservation,int renovationUrgency,string renovationComment)
        {
            this.accommodationReservation = reservation;
            this.renovationUrgency = renovationUrgency;
            this.renovationComment = renovationComment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                accommodationReservation.id.ToString(),
                renovationUrgency.ToString(),
                renovationComment
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            id = int.Parse(values[0]);
            accommodationReservation.id = int.Parse(values[1]);
            renovationUrgency = int.Parse(values[2]);
            renovationComment = values[3];

        }

    }
}
