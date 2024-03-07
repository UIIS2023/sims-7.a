using System.Collections.Generic;
using BookingApp.Domain.Models;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface IImageRepository
{
    public Image Save(Image image);
    public List<Image> getAccomodationImg();




}