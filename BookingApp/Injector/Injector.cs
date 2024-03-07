using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository.Guest1;
using BookingApp.Repository.Owner;
using BookingApp.Repository.Mutual;
using BookingApp.Repository.Guest2;
using BookingApp.Domain.RepositoryInterfaces.Guest2;

namespace BookingApp.Injector
{
    public class Injector
    {
        private static int idUser;

        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IReservationRepository), new ReservationRepository() },
            { typeof(IAccommodationRepository), new AccommodationRepository() },
            { typeof(IImageRepository), new ImageRepository() },
            { typeof(ILocationRepository), new LocationRepository()},
            { typeof(IRequestRepository),new RequestRepository()},
            { typeof(IReviewRepository),new ReviewRepository() },
            { typeof(IRateReservationGuestRepository),new RateReservationGuestRepository()},
            { typeof(IRenovationRepository),new RenovationRepository()},
            { typeof(ISuperGuestRepository), new SuperGuestRepository()},
            { typeof(IUserRepository),new UserRepository()},
            { typeof(ITourFeedbackRepository),new TourFeedbackRepository(idUser)},
            { typeof(IForumThreadRepository),new ForumThreadRepository() },
            { typeof(IForumCommentRepository),new ForumCommentRepository()}
            // Add more implementations here
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }

    }



}
