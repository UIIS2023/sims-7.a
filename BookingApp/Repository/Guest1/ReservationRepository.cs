using BookingApp.Domain.Models.Guest1;
using BookingApp.Repository.Owner;
using BookingApp.Repository;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.Domain.Models.Owner;
using System;
using BookingApp.Repository.Mutual;

namespace BookingApp.Repository.Guest1
{
    public class ReservationRepository:IReservationRepository
    {
        private const string filePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<AccommodationReservation> serializer;

        private List<AccommodationReservation> reservations;


        public ReservationRepository() 
        {
            serializer = new Serializer<AccommodationReservation>();
            reservations = serializer.FromCSV(filePath);
        }

        public AccommodationReservation Save(AccommodationReservation reservation)
        {
            reservation.id = NextId();
            reservations = serializer.FromCSV(filePath);
            reservations.Add(reservation);
            serializer.ToCSV(filePath, reservations);
            
            return reservation; 
        }

        public void Remove(AccommodationReservation reservation)
        {
            reservations = serializer.FromCSV(filePath);
            var deletionReservation = reservations.Find(x=>x.id==reservation.id);
            reservations.Remove(deletionReservation);
            serializer.ToCSV(filePath,reservations);
        }

        private void UpdateAccomodationsInReservations()
        {
            AccommodationRepository accomodationRepository = new AccommodationRepository();
            List<Accommodation> accomodations = accomodationRepository.GetAll();

            foreach (AccommodationReservation reservation in reservations)
            {
                foreach (Accommodation accomodation in accomodations)
                {
                    if (reservation.accomodation.id == accomodation.id)
                        reservation.accomodation = accomodation;
                }
            }
        }

        public List<AccommodationReservation> GetAll()
        {
            reservations = serializer.FromCSV(filePath);
            UpdateAccomodationsInReservations();

            return reservations;
        }
        
        public List<AccommodationReservation> GetByAccommodationId(int id) 
        {
            reservations = serializer.FromCSV(filePath);
            UpdateAccomodationsInReservations();
            var filteredReservations = reservations.Where(x => x.accomodation.id == id);

            return filteredReservations.ToList();
        }

        public List<AccommodationReservation> GetByUserId(int id)
        {
            reservations = serializer.FromCSV(filePath);
            var filteredReservations = reservations.Where(x => x.idGuest == id);

            return filteredReservations.ToList();
        }

        private int NextId()
        {
            reservations = serializer.FromCSV(filePath);
            if (reservations.Count < 1)
                return 1;

            return reservations.Max(x => x.id) + 1;
        }


        public List<AccommodationReservation> ReservationsForRate(int userId)
        {
            List<AccommodationReservation> finished = new List<AccommodationReservation>();

            AccommodationRepository accommodationRepository = new AccommodationRepository();
            UserRepository userRepository = new UserRepository();


            foreach (AccommodationReservation r in reservations)
            {
                if (r.accomodation.idOwner == userId)
                {
                    if (DateTime.Now >= r.endDate && (DateTime.Now - r.endDate.Date).TotalDays <= 5)
                    {
                        r.accomodation = accommodationRepository.GetById(r.accomodation.id);
                        r.guest = userRepository.GetById(r.idGuest);
                        finished.Add(r);

                    }
                }
            }

            return finished; //vracamo rezervacije za ocenjivanje
        }

        //public List<AccommodationReservation> FinishedReservations(int userId)
        //{
        //    reservations = serializer.FromCSV(filePath); //sve rez
        //    var filteredReservations = reservations.Where(x => x.accommodation.idOwner == userId);

        //    List<AccommodationReservation> finished = new List<AccommodationReservation>();
        //    foreach (AccommodationReservation a in filteredReservations)
        //    {
        //        if (DateTime.Now >= a.endDate && (DateTime.Now - a.endDate.Date).TotalDays <= 5)
        //            finished.Add(a);
        //    }
        //    //ovde metoda a.guest = getGuestById koja vraca celog usera a prosledim mu samo gyestId

        //    return finished;
        //}

        public AccommodationReservation Update(AccommodationReservation ar)
        {
            reservations = serializer.FromCSV(filePath);

            AccommodationReservation current = reservations.Find(t => t.id == ar.id);
            int index = reservations.IndexOf(current);
            reservations.Remove(current);

            reservations.Insert(index, ar);
            serializer.ToCSV(filePath, reservations);
            return ar;
        }


    }
}
