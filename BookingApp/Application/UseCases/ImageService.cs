using System.Collections.Generic;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Application.UseCases;

public class ImageService
{
    private readonly IImageRepository imageRepository;

    public ImageService()
    {
        imageRepository = Injector.Injector.CreateInstance<IImageRepository>();
    }

    public Image Save(Image image)
    {
        return imageRepository.Save(image);
    }

    public List<Image> GetAccommodationImgImages()
    {
        return imageRepository.getAccomodationImg();
    }
}