using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class TourAttendenceRepository
    {
        public const string filePath = "../../../Resources/Data/tourattendences.csv";
        private readonly Serializer<TourAttendence> _serializer;
        private List<TourAttendence> _tourAttendences;

        public TourAttendenceRepository()
        {
            _serializer= new Serializer<TourAttendence>();
            _tourAttendences = _serializer.FromCSV(filePath);
        }

        public TourAttendence Save(TourAttendence tourAttendence)
        {
            tourAttendence.id = NextId();
            _tourAttendences = _serializer.FromCSV(filePath);
            _tourAttendences.Add(tourAttendence);
            _serializer.ToCSV(filePath, _tourAttendences);
            return tourAttendence;
        }

        public List<TourAttendence> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }

        public TourAttendence Update(TourAttendence tourAttendence)
        {
            _tourAttendences = _serializer.FromCSV(filePath);
            TourAttendence current = _tourAttendences.Find(t => t.id == tourAttendence.id);
            int index = _tourAttendences.IndexOf(current);
            _tourAttendences.Remove(current);
            _tourAttendences.Insert(index, tourAttendence);
            _serializer.ToCSV(filePath, _tourAttendences);
            return tourAttendence;
        }


        private int NextId()
        {
            _tourAttendences = _serializer.FromCSV(filePath);
            if (_tourAttendences.Count < 1)
            {
                return 1;
            }
            return _tourAttendences.Max(t => t.id) + 1;
        }

    }
}
