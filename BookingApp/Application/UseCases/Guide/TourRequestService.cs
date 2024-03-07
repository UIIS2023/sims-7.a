using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using BookingApp.Repository.Guest2;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases.Guide
{
    public class TourRequestService
    {
        //private readonly TourAppointmentService _tourAppointmentService;
        private readonly CreateBasicTourRepository _createBasicTourRepository;

        public TourRequestService() 
        {
            //_tourAppointmentService = new TourAppointmentService();
            _createBasicTourRepository = new CreateBasicTourRepository();
            
        }

        public Boolean CheckDateAvailability(DateTime desiredTime, int guideId, int duration, List<TourAppointment> tourAppointments)
        {
            List<TourAppointment> appointments = new List<TourAppointment>(tourAppointments);
            DateTime requestStartTime = desiredTime;
            DateTime requestEndTime = requestStartTime.AddHours(duration);

            foreach (TourAppointment appointment in appointments)
            {
                DateTime appointmentStartTime = appointment.startTime;
                DateTime appointmentEndTime = appointment.startTime.AddHours(appointment.tour.duration);

                if (appointmentStartTime <= requestEndTime && appointmentEndTime >= requestStartTime)
                {
                    return false;
                }
            }

            return true;
        }

        public List<CreateBasicTour> GetAll()
        {
            return _createBasicTourRepository.GetAll();
        }

        public CreateBasicTour Update(CreateBasicTour tour)
        {
            return _createBasicTourRepository.Update(tour);
        }

        public string MostRequestedLanguage()
        {
            List<CreateBasicTour> tours = GetAll();

            var mostCommonLanguage = tours
                .GroupBy(request => request.language)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault();


            return mostCommonLanguage;
        }

        public string MostRequestedLocation()
        {
            List<CreateBasicTour> tours = GetAll();

            var mostCommonLocation = tours
                .GroupBy(request => request.city)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault();

            return mostCommonLocation;
        }

        public List<CreateBasicTour> GetAllExpired()
        {
            return GetAll().Where(x => x.status == Status.INVALID).ToList(); 
        }
    }
}
