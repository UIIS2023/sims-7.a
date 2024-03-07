using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Guest2
{
    public class CreateBasicTourRepository
    {
        private const string filePath = "../../../Resources/Data/CustomerTours/BasicTours.csv";
        private readonly Serializer<CreateBasicTour> _serializer;
        private List<CreateBasicTour> _tourRequests;

        public CreateBasicTourRepository()
        {
            _serializer = new Serializer<CreateBasicTour>();
            _tourRequests = new List<CreateBasicTour>();
        }

        public CreateBasicTour Save(CreateBasicTour tour)
        {
            tour.id = nextId();
            _tourRequests = _serializer.FromCSV(filePath);
            _tourRequests.Add(tour);
            _serializer.ToCSV(filePath, _tourRequests);
            return tour;
        }

        public List<CreateBasicTour> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }

        public CreateBasicTour Update(CreateBasicTour tour)
        {
            _tourRequests = _serializer.FromCSV(filePath);

            CreateBasicTour current = _tourRequests.Find(t => t.id == tour.id);
            int index = _tourRequests.IndexOf(current);
            _tourRequests.Remove(current);

            _tourRequests.Insert(index, tour);
            _serializer.ToCSV(filePath, _tourRequests);

            return tour;
        }

        private int nextId()
        {
            _tourRequests = _serializer.FromCSV(filePath);
            if (_tourRequests.Count < 1)
            {
                return 1;
            }

            return _tourRequests.Max(t => t.id) + 1;
        }
    }
}
