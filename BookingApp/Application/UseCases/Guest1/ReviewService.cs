using System.Collections.Generic;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Application.UseCases.Guest1;

public class ReviewService
{
    private readonly IReviewRepository reviewRepository;

    public ReviewService()
    {
        reviewRepository = Injector.Injector.CreateInstance<IReviewRepository>();
    }

    public List<AccommodationReview> GetByUserId(int id)
    {
        var reviews = reviewRepository.GetByUserId(id);

        return reviews;
    }

    public List<AccommodationReview> GetByAccommodationId(int id)
    {
        var reviews = reviewRepository.GetByAccommodationId(id);

        return reviews;
    }

    public AccommodationReview Save(AccommodationReview review)
    {
        return reviewRepository.Save(review);
    }
}