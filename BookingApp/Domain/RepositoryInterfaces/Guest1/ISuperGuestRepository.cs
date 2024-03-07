using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Domain.RepositoryInterfaces.Guest1;

public interface ISuperGuestRepository
{

    public SuperGuestTitle Save(SuperGuestTitle guestTitle);
    public SuperGuestTitle GetById(int id);
    public SuperGuestTitle Update(SuperGuestTitle guestTitle);

}