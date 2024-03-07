using BookingApp.Domain.Models;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourAttendence : ISerializable
    {
        public int id { get; set; }
        public int guestId { get; set; }
        public int appointmentId { get; set; }
        public InitialProject.Model.User guest { get; set; }
        public int reservedSpaces { get; set; }
        public int averageAge { get; set; }
        public bool usedVoucher { get; set; }
        public TourPoint currentPoint { get; set; }
        public Boolean attended { get; set; }
        public Boolean requestSent { get; set; }

        public TourAttendence() 
        { 
            this.currentPoint = new TourPoint();
        }
        public TourAttendence(int guestId, int appointmentId, int reservedSpaces, int averageAge, bool usedVoucher)
        {
            this.guestId = guestId;
            this.currentPoint = new TourPoint();
            this.attended = false;
            this.requestSent = false;
            this.appointmentId = appointmentId;
            this.reservedSpaces = reservedSpaces;
            this.averageAge = averageAge;
            this.usedVoucher = usedVoucher;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                appointmentId.ToString(),
                guestId.ToString(),
                currentPoint.id.ToString(),
                attended.ToString(),
                requestSent.ToString(),
                reservedSpaces.ToString(),
                averageAge.ToString(),
                usedVoucher.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            appointmentId = Convert.ToInt32(values[1]);
            guestId = Convert.ToInt32(values[2]);
            currentPoint.id= Convert.ToInt32(values[3]);
            attended= Convert.ToBoolean(values[4]);
            requestSent= Convert.ToBoolean(values[5]);
            reservedSpaces = Convert.ToInt32(values[6]);
            averageAge = Convert.ToInt32(values[7]);
            usedVoucher= Convert.ToBoolean(values[8]);
        }
    }
}
