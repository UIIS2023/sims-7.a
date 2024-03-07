using System.Collections.Generic;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Owner;

namespace BookingApp.Domain.RepositoryInterfaces.Owner;

public interface IRateReservationGuestRepository
{
    public List<RateReservation> GetAll();
}