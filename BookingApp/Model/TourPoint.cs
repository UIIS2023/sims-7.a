using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum PointType
    {
        Start,
        Mid,
        End
    }
    public class TourPoint : ISerializable
    {
        public int id { get; set; }
        public string name { get; set; }
        public Tour tour { get; set; } = new Tour();
        public int order { get; set; }
        public Boolean status { get; set; }
        public PointType pointType { get; set; }
        public TourPoint() { }
        public TourPoint(string name, Tour tour, int order, Boolean status, PointType pointType)
        {
            this.pointType = pointType;
            this.name = name;
            this.tour = tour;
            this.order = order;
            this.status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                tour.id.ToString(),
                order.ToString(),
                status.ToString(),
                pointType.ToString(),
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = Convert.ToString(values[1]);
            tour.id = Convert.ToInt32(values[2]);
            order = Convert.ToInt32(values[3]);
            status = Convert.ToBoolean(values[4]);
            pointType = ParsePointType(values[5]);
        }

        private PointType ParsePointType(string pointType)
        {
            switch (pointType)
            {

                case "Start":
                    return PointType.Start;

                case "Mid":
                    return PointType.Mid;

                case "End":
                    return PointType.End;

                default:
                    return PointType.End;

            }
        }
    }
}
