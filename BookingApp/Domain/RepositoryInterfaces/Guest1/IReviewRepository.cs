using System.Collections.Generic;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Domain.RepositoryInterfaces.Guest1;

public interface IReviewRepository
{
    public AccommodationReview Save(AccommodationReview review);
    public List<AccommodationReview> GetByAccommodationId(int id);
    public List<AccommodationReview> GetByUserId(int id);

}