using BookingApp.Domain.Models.Guest2;
using BookingApp.Domain.Models.Guide;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases.Guide
{
    public class NotificationService
    {
        private readonly NewTourNotificationRepository _notificationRepository;
        private readonly TourRequestService _requestService;

        public NotificationService()
        {
            _notificationRepository= new NewTourNotificationRepository();
            _requestService = new TourRequestService();
        }

        public void Create(Boolean LanguageOption, TourAppointment tourAppointment)
        {
            List<CreateBasicTour> expiredRequests = new List<CreateBasicTour>(_requestService.GetAllExpired());

            if (LanguageOption)
            {
                foreach(CreateBasicTour request in expiredRequests)
                {
                    if(request.language == tourAppointment.tour.language)
                    {
                        NewTourNotification notification = new NewTourNotification(2, tourAppointment.id, LanguageOption);
                        _notificationRepository.Save(notification);
                    }
                }
            }
            else
            {
                foreach (CreateBasicTour request in expiredRequests)
                {
                    if(request.city == tourAppointment.tour.Location.city)
                    {
                        NewTourNotification notification = new NewTourNotification(2, tourAppointment.id, LanguageOption);
                        _notificationRepository.Save(notification);

                    }
                }
            }


        }


    }
}
