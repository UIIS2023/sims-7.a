using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Repository.Guide
{
    class TourPointRepository
    {
        private const string filePath = "../../../Resources/Data/tourpoints.csv";
        private readonly Serializer<TourPoint> _serializer;
        private List<TourPoint> _tourPoints;
        public TourPointRepository()
        {
            _serializer = new Serializer<TourPoint>();
            _tourPoints = new List<TourPoint>();
        }

        public TourPoint Save(TourPoint tourPoint)
        {
            tourPoint.id = NextId();
            _tourPoints = _serializer.FromCSV(filePath);
            _tourPoints.Add(tourPoint);
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

        public List<TourPoint> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }
    }
}
