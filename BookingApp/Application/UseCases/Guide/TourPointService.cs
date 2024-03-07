using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class TourPointService
    {
        private readonly TourAppointmentPointRepository _tourAppointmentPointRepository;

        public TourPointService()
        {
            _tourAppointmentPointRepository= new TourAppointmentPointRepository();
        }

        public void SetCheckPoint(TourPoint tourPoint)
        {
            tourPoint.status = true;
            _tourAppointmentPointRepository.Update(tourPoint);
        }

        public TourPoint FindCurrentPoint(TourAppointment tourAppointment)
        {
            TourPoint currentPoint = new TourPoint();
            foreach(TourPoint tourPoint in tourAppointment.tourPoints)
            {
                if(tourPoint.status == true)
                {
                    currentPoint = tourPoint;
                }
            }

            return currentPoint;
        }
        public void CreateTourPointsForAppointment(TourAppointment tourAppointment, TourService tourService) 
        {
            List<TourPoint> tourPoints = new List<TourPoint>();
            tourPoints = tourService.GetTourPointsByTourId(tourAppointment.tour.id);

            foreach (TourPoint point in tourPoints)
            {
                point.tour.id = tourAppointment.id;
                if (point.order == 1)
                {
                    point.status = true;
                }
                _tourAppointmentPointRepository.Save(point);
            }
        }

        public void BindTourPointsToAppointment(TourAppointment tourAppointment) 
        {
            List<TourPoint> tourPoints = new List<TourPoint>();
            tourAppointment.tourPoints = new List<TourPoint>();

            tourPoints = _tourAppointmentPointRepository.GetAll();
            foreach (TourPoint tourPoint in tourPoints)
            {
                if (tourPoint.tour.id == tourAppointment.id)
                {
                    tourAppointment.tourPoints.Add(tourPoint);
                }
            }
        }

    }
}
