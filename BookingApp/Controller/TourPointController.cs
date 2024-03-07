using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    internal class TourPointController
    {
        private readonly TourPointService _tourPointService;
        public TourPointController() 
        {
            _tourPointService= new TourPointService();
        }

        public TourPoint FindCurrentPoint(TourAppointment tourAppointment)
        {
            return _tourPointService.FindCurrentPoint(tourAppointment);
        }

        public void SetCheckPoint(TourPoint tourPoint)
        {
            _tourPointService.SetCheckPoint(tourPoint);
        }
    }
}
