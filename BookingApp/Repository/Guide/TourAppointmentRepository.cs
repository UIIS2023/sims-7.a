using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Guide
{

    class TourAppointmentRepository { 
        private const string filePath = "../../../Resources/Data/tourappointments.csv";
        private readonly Serializer<TourAppointment> _serializer;
        private List<TourAppointment> _tourappointments;

        public TourAppointmentRepository()
        {
            _serializer = new Serializer<TourAppointment>();
            _tourappointments = new List<TourAppointment>();
        }

        public TourAppointment Save(TourAppointment tourAppointment)
        {
            tourAppointment.id = NextId();
            _tourappointments = _serializer.FromCSV(filePath);
            _tourappointments.Add(tourAppointment);
            _serializer.ToCSV(filePath, _tourappointments);
            return tourAppointment;
        }

        public List<TourAppointment> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }

        public TourAppointment Update(TourAppointment tourAppointment)
        {
            _tourappointments = _serializer.FromCSV(filePath);

            TourAppointment current = _tourappointments.Find(t => t.id == tourAppointment.id);
            int index = _tourappointments.IndexOf(current);
            _tourappointments.Remove(current);

            _tourappointments.Insert(index, tourAppointment);
            _serializer.ToCSV(filePath, _tourappointments);

            return tourAppointment;
        }
        public int NextId()
        {
            _tourappointments = _serializer.FromCSV(filePath);
            if (_tourappointments.Count < 1)
            {
                return 1;
            }

            return _tourappointments.Max(l => l.id) + 1;
        }

    }
}
