using System.Collections.Generic;
using BookingApp.Domain.Models;

namespace BookingApp.Domain.RepositoryInterfaces;

public interface ILocationRepository
{
    public Location Save(Location location);
    public int GetIdByCountyAndCity(string city, string country);
    public List<Location> GetAll();
}