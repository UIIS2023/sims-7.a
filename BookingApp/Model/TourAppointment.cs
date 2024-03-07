using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourAppointment : ISerializable
    {
        public int id { get; set; }
        public Tour tour { get; set; } = new Tour();
        public DateTime startTime { get; set; }
        public int guideId { get; set; }
        public bool started { get; set; }
        public bool ended { get; set; }
        public bool cancelled { get; set; }
        public List<TourPoint> tourPoints { get; set; }
        public int numberOfGuests { get; set; }

        public TourAppointment() { }
        public TourAppointment(Tour tour, DateTime startTime, int guideId)
        {
            this.tour = tour;
            this.startTime = startTime;
            this.guideId = guideId;
            this.started = false;
            this.ended = false;
            this.cancelled = false;
            tourPoints = new List<TourPoint>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                tour.id.ToString(),
                startTime.ToString(),
                guideId.ToString(),
                started.ToString(),
                ended.ToString(),
                cancelled.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            tour.id = Convert.ToInt32(values[1]);
            startTime = Convert.ToDateTime(values[2]);
            guideId = Convert.ToInt32(values[3]);
            started = Convert.ToBoolean(values[4]);
            ended = Convert.ToBoolean(values[5]);
            cancelled = Convert.ToBoolean(values[6]);
        }
    }
}
