using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class TourReservationRepository
    {
        public const string filePath = "../../../Resources/Data/CustomerTours/TourReservations.csv";
        private readonly Serializer<TourReservation> _serializer;
        private List<TourReservation> _tourReservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(filePath);
        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }

    }
}
