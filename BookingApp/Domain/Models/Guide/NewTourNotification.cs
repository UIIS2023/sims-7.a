using BookingApp.View;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Models.Guide
{
    public class NewTourNotification : ISerializable
    {
        public int id { get; set; }
        public int guestId { get; set; }
        public int appointmentId { get; set; }
        public Boolean isSeen { get; set; }
        public Boolean isLanguageRequested { get; set; }


        public NewTourNotification() { }
        public NewTourNotification(int guestId, int appointmentId, Boolean isLanguageRequested)
        {
            this.guestId = guestId;
            this.appointmentId = appointmentId;
            this.isSeen = false;
            this.isLanguageRequested = isLanguageRequested;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                guestId.ToString(),
                appointmentId.ToString(),
                isSeen.ToString(),
                isLanguageRequested.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            guestId = Convert.ToInt32(values[1]);
            appointmentId = Convert.ToInt32(values[2]);
            isSeen = Convert.ToBoolean(values[3]);
            isLanguageRequested= Convert.ToBoolean(values[4]);
        }
    }
}
