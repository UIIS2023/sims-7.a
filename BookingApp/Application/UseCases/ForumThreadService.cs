using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class ForumThreadService
    {
        private IForumThreadRepository forumThreadRepository;
        private ILocationRepository locationRepository;
        private IUserRepository userRepository;

        public ForumThreadService()
        {
            forumThreadRepository = Injector.Injector.CreateInstance<IForumThreadRepository>();
            locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
            userRepository = Injector.Injector.CreateInstance<IUserRepository>();
        }

        public ForumThread Save(ForumThread forumThread)
        {
            return forumThreadRepository.Save(forumThread); 
        }

        public List<ForumThread> GetAll()
        {
            List<ForumThread> forumThreads = forumThreadRepository.GetAll();
            UpdateLocationInThread(forumThreads);
            UpdateUserInThread(forumThreads);

            return forumThreads;
        }

        public ForumThread GetById(int id)
        {
            return GetAll().Find(x => x.id == id);
        }

        public ForumThread UpdateThread(ForumThread forumThread)
        {
            return forumThreadRepository.Update(forumThread);
        }

        private void UpdateLocationInThread(List<ForumThread> forumThreads)
        { 
            foreach(var thread in forumThreads)
            {
                foreach (var location in locationRepository.GetAll()) 
                {
                    if (thread.forLocation.idLocation == location.idLocation) thread.forLocation = location;
                }
            }
            
        }

        private void UpdateUserInThread(List<ForumThread> forumThreads)
        {
            foreach(var thread in forumThreads)
            {
                foreach(var user in userRepository.GetAll())
                {
                    if (thread.opUser.id == user.id) thread.opUser = user;
                }

            }

        }

        private List<Location> GetFreeLocations(List<ForumThread> forumThreads)
        {
            var locations = new List<Location>();
            foreach (var location in locationRepository.GetAll())
            {
                bool isValidLocation = true;
                foreach (var thread in forumThreads)
                {
                    if (thread.forLocation.idLocation == location.idLocation && thread.isOpen) isValidLocation = false;
                }
                if(isValidLocation) locations.Add(location);

            }

            return locations;

        }

        public List<String> GetAvailableCountries()
        {
            var locations = GetFreeLocations(forumThreadRepository.GetAll());
            var countries = new List<String>();

            foreach(var l in locations)
            {
                if(!countries.Contains(l.country)) countries.Add(l.country);
            }

            return countries;
        }

        public List<String> GetAvailableCitiesByCountry(String country)
        {
            var locations = GetFreeLocations(forumThreadRepository.GetAll());
            var cities = new List<String>();

            foreach(var l in locations)
            {
                if (l.country == country) cities.Add(l.city);
            }
            return cities;
        }


    }
}
