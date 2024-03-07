using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Repository.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.Service
{
    public class TourAppointmentService
    {
        private readonly TourAppointmentRepository _tourAppointmentRepository;
        private readonly TourService _tourService;
        private readonly TourAppointmentPointRepository _tourAppointmentPointRepository;
        private readonly ImageRepository imageRepository;
        private readonly TourAttendenceService _tourAttendenceService;
        private readonly TourPointService _tourPointService;
        public TourAppointmentService()
        {
            _tourAppointmentRepository = new TourAppointmentRepository();
            _tourService = new TourService();
            _tourAppointmentPointRepository = new TourAppointmentPointRepository();
            _tourAttendenceService = new TourAttendenceService();
            imageRepository = new ImageRepository();
            _tourPointService = new TourPointService();
        }

        public void Create(Tour tour, DateTime startTime, int guideId)
        {
            _tourAppointmentRepository.Save(new TourAppointment(tour, startTime, guideId));
        }

        public List<TourAppointment> GetAll()
        {
            List<TourAppointment> tourAppointments = new List<TourAppointment>();
            List<Tour> tours = new List<Tour>();
            tourAppointments.AddRange(_tourAppointmentRepository.GetAll());
            tours = _tourService.GetAll();

            foreach (TourAppointment tourAppointment in tourAppointments)
            {
                tourAppointment.tour = GetTourById(tourAppointment.tour.id, tours);
            }

            return tourAppointments;
        }

        private Tour GetTourById(int tourId, List<Tour> tours)
        {
            foreach(Tour tour in tours)
            {
                if(tour.id == tourId)
                {
                    BindImageToTour(tour);
                    return tour;
                }
            }
            return new Tour();
        }

        public List<TourAppointment> GetAllByGuide(int guideId)
        {
            List<TourAppointment> tourAppointments = new List<TourAppointment>();

            foreach(TourAppointment tourAppointment in GetAll())
            {
                if(tourAppointment.tour.guideId == guideId && tourAppointment.startTime.Date >= DateTime.Today.Date 
                    && !tourAppointment.started && !tourAppointment.cancelled)
                {
                    tourAppointments.Add(tourAppointment);
                }
            }

            return tourAppointments;
        }

        public List<TourAppointment> GetTodaysAppointmentsByGuide(int guideId)
        {
            List<TourAppointment> tourAppointments = new List<TourAppointment>();

            foreach(TourAppointment tourAppointment in GetAllByGuide(guideId))
            {
                if(tourAppointment.startTime.Date == DateTime.Today.Date && !tourAppointment.started)
                {
                    tourAppointments.Add(tourAppointment);
                }
            }

            return tourAppointments;
        }
        public List<TourAppointment> GetFinishedAppointmentsByGuide(int guideId)
        {
            List<TourAppointment> finishedAppointments = new List<TourAppointment>();
            foreach(TourAppointment tourAppointment in GetAll())
            {
                if(tourAppointment.ended && tourAppointment.guideId == guideId)
                {
                    finishedAppointments.Add(tourAppointment);
                }
            }

            return finishedAppointments;
        }

        public Boolean HasGuideStartedTour(int guideId)
        {
            foreach(TourAppointment tourAppointment in GetAll())
            {
                if (tourAppointment.started && tourAppointment.guideId == guideId)
                {
                    if (!tourAppointment.ended)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void StartTour(int tourAppointmentId)
        {
            foreach (TourAppointment tourAppointment in GetAll()) 
            {
                if(tourAppointment.id == tourAppointmentId)
                {
                    tourAppointment.started = true;
                    _tourAppointmentRepository.Update(tourAppointment);
                    _tourPointService.CreateTourPointsForAppointment(tourAppointment, _tourService);
                    _tourAttendenceService.CreateAttendencesForTour(tourAppointment);
                    return;
                }
            }
        }
        public Boolean CancelTour(TourAppointment tourAppointment)
        {
            if((tourAppointment.startTime - DateTime.Today).TotalHours <= 48)
            {
                return false;
            }

            tourAppointment.cancelled = true;
            _tourAttendenceService.CreateVouchers(tourAppointment);

            _tourAppointmentRepository.Update(tourAppointment);


            return true;
        }
        public TourAppointment GetLiveTour(int guideId)
        {
            foreach (TourAppointment tourAppointment in GetAll())
            {
                if (tourAppointment.started && tourAppointment.guideId == guideId)
                {
                    if (!tourAppointment.ended)
                    {
                        _tourPointService.BindTourPointsToAppointment(tourAppointment);
                        return tourAppointment;
                    }
                }
            }
            return new TourAppointment();
        }

        public void EndTour(TourAppointment tourAppointment)
        {
            foreach(TourPoint tourPoint in tourAppointment.tourPoints)
            {
                if(tourPoint.pointType == PointType.End)
                {
                    tourPoint.status = true;
                    _tourAppointmentPointRepository.Update(tourPoint);
                }
            }
            tourAppointment.ended = true;
            _tourAppointmentRepository.Update(tourAppointment);
        }

        private void BindImageToTour(Tour tour)
        {
            List<Image> images = new List<Image>();
            tour.tourImages = new List<Image>();

            images = imageRepository.getTourImg();
            foreach (Image image in images)
            {
                if (image.resourceId == tour.id)
                {
                    tour.tourImages.Add(image);
                }
            }
            if (tour.tourImages.Count > 0)
                tour.image = tour.tourImages.First();
        }

        public TourAppointment MostAttendedTour(int guideId)
        {
            return _tourAttendenceService.MostAttendedTour(guideId, GetFinishedAppointmentsByGuide(guideId));
        }
    }
}
