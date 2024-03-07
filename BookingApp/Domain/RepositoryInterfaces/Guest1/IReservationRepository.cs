using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Domain.RepositoryInterfaces.Guest1
{
    public interface IReservationRepository
    {
        public AccommodationReservation Save(AccommodationReservation reservation);
        public List<AccommodationReservation> GetAll();
        public List<AccommodationReservation> GetByAccommodationId(int id);
        public List<AccommodationReservation> GetByUserId(int id);
        public void Remove(AccommodationReservation reservation);


    }
}
