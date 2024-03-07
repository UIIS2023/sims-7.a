using System.Collections.Generic;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Domain.RepositoryInterfaces.Guest1;

public interface IRequestRepository
{
    public MoveReservationRequest Save(MoveReservationRequest request);
    public List<MoveReservationRequest> GetByUserId(int id);
    public void Remove(MoveReservationRequest request);
}