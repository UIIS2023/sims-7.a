using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourAppointmentController
    {
        private readonly TourAppointmentService  _tourAppointmentService;

        public TourAppointmentController()
        {
            _tourAppointmentService= new TourAppointmentService();
        }
        public List<TourAppointment> GetAll() 
        {
            return _tourAppointmentService.GetAll();
        }
        public List<TourAppointment> GetAllByGuide(int guideId)
        {
            return _tourAppointmentService.GetAllByGuide(guideId);
        }
        public List<TourAppointment> GetTodaysAppointmentsByGuide(int guideId)
        {
            return _tourAppointmentService.GetTodaysAppointmentsByGuide(guideId);
        }
        public Boolean HasStartedTour(int guideId)
        {
            return _tourAppointmentService.HasGuideStartedTour(guideId);
        }
        public void StartTour(int tourAppointmentId)
        {
            _tourAppointmentService.StartTour(tourAppointmentId);
        }
        public TourAppointment GetLiveTour(int guideId)
        {
            return _tourAppointmentService.GetLiveTour(guideId);
        }
        public void EndTour(TourAppointment tourAppointment)
        {
            _tourAppointmentService.EndTour(tourAppointment);
        }
        public Boolean CancelTour(TourAppointment tourAppointment)
        {
            return _tourAppointmentService.CancelTour(tourAppointment);
        }
    }
}
