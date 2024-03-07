using BookingApp.Domain.Models.Owner;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using BookingApp.Domain.Models;


namespace BookingApp.Repository.Owner
{
    public class AccommodationRepository:IAccommodationRepository
    {
        public const string filePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> serializer;

        private List<Accommodation> accommodations;

        public AccommodationRepository()
        {
            serializer = new Serializer<Accommodation>();
            accommodations = serializer.FromCSV(filePath);
            foreach (Accommodation a in accommodations) //?
            {
                Console.WriteLine(a.id);
            }
        }

        public Accommodation GetByName(string name)
        {
            accommodations = serializer.FromCSV(filePath);
            return accommodations.FirstOrDefault(a => a.name == name);
        }

        public Accommodation Save(Accommodation accomodation)
        {
            accomodation.id = NextId();
            accommodations = serializer.FromCSV(filePath);
            accommodations.Add(accomodation);
            serializer.ToCSV(filePath, accommodations);
            return accomodation;
        }

        private int NextId()
        {
            accommodations = serializer.FromCSV(filePath);
            if (accommodations.Count < 1)
            {
                return 1;
            }
            return accommodations.Max(a => a.id) + 1;
        }

        public List<Accommodation> GetAll()
        {
            accommodations = serializer.FromCSV(filePath);

            UpdateAccommodationLocation();
            UpdateAccommodationImages();

            return accommodations;
        }

        private void UpdateAccommodationLocation()  //za objekat Location location u Accomodation.cs
        {
            Mutual.LocationRepository locationRepository = new Mutual.LocationRepository();
            List<Location> locations = locationRepository.GetAll();

            foreach (Accommodation accomodation in accommodations)
            {
                foreach (Location location in locations)
                {

                    if (location.idLocation == accomodation.idLocation)
                        accomodation.location = location;

                }
            }
        }

        private void UpdateAccommodationImages()
        {
            Mutual.ImageRepository imageRepository = new Mutual.ImageRepository();
            List<Image> images = imageRepository.getAccomodationImg();


            foreach (Accommodation accomodation in accommodations)
            {
                foreach (Image image in images)
                {
                    if (image.resourceId == accomodation.id)
                    {
                        accomodation.accommodationImages.Add(image);
                    }
                }
                if (accomodation.accommodationImages.Count > 0)
                    accomodation.firstImg = accomodation.accommodationImages.First();

            }
        }

        public Accommodation GetById(int id)
        {
            accommodations = serializer.FromCSV(filePath);
            return accommodations.FirstOrDefault(a => a.id == id);
            //Accommodation accommodation = new Accommodation();
            ////List<Accommodation> accommodations = new List<Accommodation>();
            //accommodations = accommodationRepository.GetAll();

            //foreach (Accommodation a in accommodations)
            //{
            //    if (a.id == id)
            //    {
            //        return accommodation;
            //    }
            //}
        }
    }
}
