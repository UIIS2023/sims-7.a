using BookingApp.Application.UseCases.Guide;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Repository.Guide;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourService
    {
        private readonly TourRepository _tourRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly TourAppointmentRepository _tourAppointmentRepository;
        private readonly ImageRepository _imageRepository;
        private readonly NotificationService _notificationService;

        public TourService() 
        { 
            _tourRepository= new TourRepository();
            _tourPointRepository = new TourPointRepository();
            _tourAppointmentRepository= new TourAppointmentRepository();
            _imageRepository = new ImageRepository();
            _notificationService = new NotificationService();
        }

        public void Create(Tour tour, string startPointName, string endPointName, List<string> midPointName, List<DateTime> dates, List<string> images, Boolean isTourRequested, Boolean languageOption)
        {
            List<TourPoint> points = GroupTourPoints(tour, startPointName, endPointName, midPointName);
            tour.TourPoints.AddRange(points);
            _tourRepository.Save(tour);
            TourAppointment tourAppointment = new TourAppointment();


            foreach (TourPoint tourPoint in points)
            {
                tourPoint.tour = tour;
                _tourPointRepository.Save(tourPoint);
            }
            foreach(DateTime date in dates)
            {
                tourAppointment = _tourAppointmentRepository.Save(new TourAppointment(tour, date, tour.guideId));
            }
            foreach(String imageURL in images)
            {
                _imageRepository.Save(new Image(imageURL, tour.id, USER_TYPE.Tour));
            }
            if (isTourRequested)
            {
               _notificationService.Create(languageOption, tourAppointment);
            }
        }
        internal List<TourPoint> GroupTourPoints(Tour tour, string startPointName, string endPointName, List<string> midPointNames) 
        {
            List<TourPoint> points = new List<TourPoint>();
            int order = 1;
            
            points.Add(new TourPoint(startPointName, tour, order++, false, PointType.Start));
            
            foreach(String midPointName in midPointNames)
            {
                points.Add(new TourPoint(midPointName, tour, order++, false, PointType.Mid));
            }
            
            points.Add(new TourPoint(endPointName, tour, order++, false, PointType.End));

            return points;
        }
        public List<Tour> GetAll() { 
            List<Tour> tours= new List<Tour>();
            tours = _tourRepository.GetAll();
            
            foreach(Tour tour in tours )
            {
                tour.TourPoints = GetTourPointsByTourId(tour.id);
            }

            return tours;
        }
        public List<TourPoint> GetTourPointsByTourId(int tourId)
        {
            List<TourPoint> allTourPoints = new List<TourPoint>();
            List<TourPoint> filteredTourPoints = new List<TourPoint>();

            allTourPoints = _tourPointRepository.GetAll();

            foreach (TourPoint point in allTourPoints)
            {
                if (point.tour.id == tourId)
                {
                    filteredTourPoints.Add(point);
                }
            }

            return filteredTourPoints;
        }

    }
}
