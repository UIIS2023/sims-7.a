using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using BookingApp.Domain.Models.Owner;

namespace BookingApp.Application.UseCases.Owner;

public class AccommodationService
{
    private readonly IAccommodationRepository accommodationRepository;
    private readonly IImageRepository imageRepository;
    private readonly ILocationRepository locationRepository;


    public AccommodationService()
    {
        accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
        locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
        imageRepository = Injector.Injector.CreateInstance<IImageRepository>();
    }

    public List<Accommodation> GetAll()
    {
        var accommodations = accommodationRepository.GetAll();

        UpdateAccommodationLocation(accommodations);
        UpdateAccomodationImages(accommodations);

        return accommodations;
    }

    public List<Accommodation> GetByOwnerId(int userId)
    {
        var accommodations = accommodationRepository.GetAll();

        accommodations = accommodations.Where(x => x.idOwner == userId).ToList();

        return accommodations;

    }

    public void Save(Accommodation accommodation)
    {
        accommodationRepository.Save(accommodation);
    }


    private void
        UpdateAccommodationLocation(List<Accommodation> accomodations) //za objekat Location location u Accomodation.cs
    {
        var locations = locationRepository.GetAll();

        foreach (var accomodation in accomodations)
        foreach (var location in locations)
            if (location.idLocation == accomodation.idLocation)
                accomodation.location = location;
    }

    private void UpdateAccomodationImages(List<Accommodation> accomodations)
    {
        var images = imageRepository.getAccomodationImg();


        foreach (var accomodation in accomodations)
        {
            foreach (var image in images)
                if (image.resourceId == accomodation.id)
                    accomodation.accommodationImages.Add(image);
            if (accomodation.accommodationImages.Count > 0)
                accomodation.firstImg = accomodation.accommodationImages.First();
        }
    }

    public bool isOwnerAccommodationAtLocation(int userId,int locationId)
    {
        foreach(var accommodation in GetByOwnerId(userId))
        {
            if (accommodation.idLocation == locationId) return true;
        }
        return false;

    }
}