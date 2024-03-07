using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using BookingApp.Repository.Owner;


namespace BookingApp.Application.UseCases.Owner
{

    

    public class GuestReviewService
    {

        private readonly IRateReservationGuestRepository guestReviewRepository;
        private readonly IReviewRepository ownerReviewRepository;

        private ReservationService reservationService;

        public GuestReviewService()
        {
            guestReviewRepository = Injector.Injector.CreateInstance<IRateReservationGuestRepository>();
            ownerReviewRepository = Injector.Injector.CreateInstance<IReviewRepository>();
            reservationService = new ReservationService();
        }

        public List<RateReservation> GetReviewsByUserId(int id)
        {
            var reviews = guestReviewRepository.GetAll();

            UpdateReservationsInReviews(reviews);

            return GetVisibleReviews(id,reviews);
        }

        private void UpdateReservationsInReviews(List<RateReservation> reviews)
        {

            foreach (var review in reviews)
            {
                foreach (var reservation in reservationService.GetAll())
                {
                    if (review.accommodationReservation.id == reservation.id)
                        review.accommodationReservation = reservation;
                }
            }
        }



        private List<RateReservation> GetVisibleReviews(int id,List<RateReservation> reviews)
        {

            var visibleReviews = new List<RateReservation>();

            foreach (var guestReview in reviews)
            {
                foreach (var ownerReview in ownerReviewRepository.GetByUserId(id))
                {
                    if(ownerReview.accommodationId == guestReview.accommodationReservation.accomodation.id && guestReview.accommodationReservation.idGuest == id)
                        visibleReviews.Add(guestReview);
                }
            }

            return visibleReviews;

        }

    }
}
