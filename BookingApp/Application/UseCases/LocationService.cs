using System.Collections.Generic;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Application.UseCases;

public class LocationService
{
    private readonly ILocationRepository locationRepository;

    public LocationService()
    {
        locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
    }

    public Location Save(Location location)
    {
        return locationRepository.Save(location);
    }

    public int GetIdByCountryAndCity(string city, string country)
    {
        return locationRepository.GetIdByCountyAndCity(city, country);
    }

    public List<Location> GetAll()
    {
        return locationRepository.GetAll();
    }

    public List<string> GetCitiesByCountry(string country)
    {
        List<string> cities = new List<string>();
        List<Location> locations = new List<Location>(locationRepository.GetAll());

        foreach (Location location in locations)
        {
            if(location.country == country)
            {
                cities.Add(location.city);
            }
        }

        return cities;
    }

    public List<string> GetCountries()
    {
        List<string> countries = new List<string>();
        List<Location> locations = new List<Location>(locationRepository.GetAll());

        foreach (Location location in locations)
        {
            if (!countries.Contains(location.country))
            {
                countries.Add(location.country);
            }
        }

        return countries;
    }

    public string GetCountryByCity(string city)
    {
        string country = "";

        foreach(Location location in locationRepository.GetAll())
        {
            if (location.city == city) 
            {
                country = location.country;
                break;
            }
        }

        return country;
    }
}