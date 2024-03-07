using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class TourAppointmentPointRepository
    {
        public const string filePath = "../../../Resources/Data/tourappointmentpoints.csv";
        private readonly Serializer<TourPoint> _serializer;
        private List<TourPoint> _tourPoints;

        public TourAppointmentPointRepository()
        {
            _serializer = new Serializer<TourPoint>();
            _tourPoints = _serializer.FromCSV(filePath);
        }

        public TourPoint Save(TourPoint tourPoint)
        {
            tourPoint.id = NextId();
            _tourPoints = _serializer.FromCSV(filePath);
            _tourPoints.Add(tourPoint);
            _serializer.ToCSV(filePath, _tourPoints);
            return tourPoint;
        }

        public List<TourPoint> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }

        public TourPoint Update(TourPoint tourPoint)
        {
            _tourPoints = _serializer.FromCSV(filePath);
            TourPoint current = _tourPoints.Find(t => t.id == tourPoint.id);
            int index = _tourPoints.IndexOf(current);
            _tourPoints.Remove(current);
            _tourPoints.Insert(index, tourPoint);
            _serializer.ToCSV(filePath, _tourPoints);
            return tourPoint;
        }
        private int NextId()
        {
            _tourPoints = _serializer.FromCSV(filePath);
            if (_tourPoints.Count < 1)
            {
                return 1;
            }
            return _tourPoints.Max(c => c.id) + 1;
        }

    }
}
