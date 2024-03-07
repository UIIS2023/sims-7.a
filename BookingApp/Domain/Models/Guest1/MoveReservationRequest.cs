using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models.Guest1
{
    public enum REQUEST_STATUS{
        Pending,
        Accepted,
        Denied,
    }

    public class MoveReservationRequest: ISerializable
    {
        public int id;
        public int userId;
        public AccommodationReservation oldReservation { get; set; }
        public DateTime newStartDate { get; set; }
        public DateTime newEndDate { get; set; }
        public REQUEST_STATUS status;
        public string ownerComment { get; set; }
        public bool isUserNotified; // false if we want the user to be notified
        public bool isAvaliable; //true if it is
        public string getStatusString
        {
            get
            {
                return status.ToString();
            }

        }

        public MoveReservationRequest()
        {
            oldReservation = new AccommodationReservation();
            status = REQUEST_STATUS.Pending;
            isUserNotified = true;
            isAvaliable = false;
        }

        public MoveReservationRequest(int userId,AccommodationReservation oldReservation,DateTime newStartDate,DateTime newEndDate)
        {   
            this.userId = userId;
            this.oldReservation = oldReservation;
            this.newStartDate = newStartDate;
            this.newEndDate = newEndDate;
            this.status = REQUEST_STATUS.Pending;
            isUserNotified = true;
            isAvaliable = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                userId.ToString(),
                oldReservation.id.ToString(),
                newStartDate.ToString("dd'/'MM'/'yyyy"),
                newEndDate.ToString("dd'/'MM'/'yyyy"),
                status.ToString(),
                isUserNotified.ToString(),
                isAvaliable.ToString(),
                ownerComment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            userId = int.Parse(values[1]);
            oldReservation.id = int.Parse(values[2]);
            newStartDate = DateTime.ParseExact(values[3], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            newEndDate = DateTime.ParseExact(values[4], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            status = Enum.Parse<REQUEST_STATUS>(values[5]);
            isUserNotified = bool.Parse(values[6]);
            isAvaliable = bool.Parse(values[7]);
            ownerComment = values[8];
        }

    }
}
