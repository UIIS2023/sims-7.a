using InitialProject.Serializer;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;

namespace BookingApp.Repository.Guide
{
    class TourRepository
    {
        public const string filePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;


        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(filePath);
        }

        public Tour Save(Tour tour)
        {
            tour.id = NextId();
            _tours = _serializer.FromCSV(filePath);
            _tours.Add(tour);
            _serializer.ToCSV(filePath, _tours);
            return tour;
        }

        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }

        public void Delete(Tour tour)
        {
            _tours = _serializer.FromCSV(filePath);
            Tour founded = _tours.Find(t => t.id == tour.id);
            _tours.Remove(tour);
            _serializer.ToCSV(filePath, _tours);
        }

        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(filePath);
            Tour current = _tours.Find(t => t.id == tour.id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(filePath, _tours);
            return tour;
        }

        private int NextId()
        {
            _tours = _serializer.FromCSV(filePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.id) + 1;
        }

    }
}
