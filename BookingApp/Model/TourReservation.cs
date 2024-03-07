using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    internal class TourReservation : ISerializable
    {
        public string tourName { get; set; }
        public int totalReservations { get; set; }
        public DateTime startTime { get; set; }
        public int appointmentId { get; set; }
        public int guestId { get; set; }
        public int numberOfGuests { get; set; }

        public bool usedVoucher { get; set; }
        public int averageAge { get; set; }

        public TourReservation() { }
        public TourReservation(string tourName, int totalReservations, DateTime startTime, int appointmentId, int numberOfGuests, bool usedVoucher, int averageAge)
        {
            this.tourName = tourName;
            this.totalReservations = totalReservations;
            this.startTime = startTime;
            this.appointmentId = appointmentId;
            this.numberOfGuests = numberOfGuests;
            this.usedVoucher = usedVoucher;
            this.averageAge = averageAge;
        }

        public string[] ToCSV()
        {
            throw new NotImplementedException();
        }

        public void FromCSV(string[] values)
        {
            this.tourName = values[0];
            this.totalReservations = Convert.ToInt32(values[1]);
            this.startTime = Convert.ToDateTime(values[2]);
            this.appointmentId = Convert.ToInt32(values[3]);
            this.guestId = Convert.ToInt32(values[4]);
            this.numberOfGuests = Convert.ToInt32(values[5]);
            this.usedVoucher = Convert.ToBoolean(values[6]);
            this.averageAge = Convert.ToInt32(values[7]);
        }
    }
}
