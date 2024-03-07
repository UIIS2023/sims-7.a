using BookingApp.Domain.Models;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository.Mutual
{
    internal class LocationRepository:ILocationRepository
    {
        private const string filePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;


        public LocationRepository() { 
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(filePath);
        }

        public Location Save(Location location) {
            location.idLocation = NextId();
            _locations = _serializer.FromCSV(filePath);
            _locations.Add(location);
            _serializer.ToCSV(filePath, _locations);
            return location;
        }

        public int GetIdByCountyAndCity(string city, string country)
        {
            _locations = _serializer.FromCSV(filePath);
            foreach (Location l in _locations)
            {
                if ((l.city == city) && (l.country == country))
                    return l.idLocation;
            }
            return 1;
        }



        public List<Location> GetAll() {
            return _serializer.FromCSV(filePath);
        }

        private int NextId() {
            _locations = _serializer.FromCSV(filePath);
            if (_locations.Count < 1) {
                return 1;
            }

            return _locations.Max(l => l.idLocation) + 1;
        }
    }
}
