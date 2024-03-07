using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Application.UseCases.Guest1
{

    public class RenovationProposalService
    {
        private readonly IRenovationRepository proposalRepository;

        public RenovationProposalService()
        {
            proposalRepository = Injector.Injector.CreateInstance<IRenovationRepository>();
        }

        public RenovationProposal Save(RenovationProposal proposal)
        {
            return proposalRepository.Save(proposal);
        }

        public List<RenovationProposal> GetAll()
        {
            return proposalRepository.GetAll();
        }

        public List<string> GetUrgencyLevelsList()
        {
            var urgencyLevelList = new List<string>
            {
                "1-It would be nice to renovate some small things, but everything works as it should.",
                "2-Small gripes with the accommodation that if removed would make it perfect.",
                "3-A few things that really bothered me should be renovated.",
                "4-There are a lot of bad things and a renovation is really necessary.",
                "5-The accommodation is in a very bad condition and is not worth renting at all unless it is renovated."
            };

            return urgencyLevelList;

        }


    }
}
