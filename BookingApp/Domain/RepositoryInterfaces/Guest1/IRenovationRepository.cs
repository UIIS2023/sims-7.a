using System.Collections.Generic;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Domain.RepositoryInterfaces.Guest1;

public interface IRenovationRepository
{
    public RenovationProposal Save(RenovationProposal proposal);
    public List<RenovationProposal> GetAll();

}