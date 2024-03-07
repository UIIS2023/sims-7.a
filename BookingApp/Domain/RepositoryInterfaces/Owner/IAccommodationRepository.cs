using BookingApp.Domain.Models.Owner;
using System.Collections.Generic;


namespace BookingApp.Domain.RepositoryInterfaces.Owner;


public interface IAccommodationRepository
{

    public Accommodation GetByName(string name);
    public Accommodation Save(Accommodation accomodation);
    public List<Accommodation> GetAll();



}