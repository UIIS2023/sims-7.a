using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using InitialProject.Serializer;

namespace BookingApp.Repository.Guest1
{
    public class RenovationRepository:IRenovationRepository
    {
        private const string filePath = "../../../Resources/Data/renovationProposals.csv";

        private readonly Serializer<RenovationProposal> serializer;
        private List<RenovationProposal> proposals;

        public RenovationRepository()
        {
            serializer = new Serializer<RenovationProposal>();
            proposals = serializer.FromCSV(filePath);
        }

        public RenovationProposal Save(RenovationProposal proposal)
        {
            proposal.id = NextId();
            proposals = serializer.FromCSV(filePath);
            proposals.Add(proposal);
            serializer.ToCSV(filePath,proposals);

            return proposal;
        }

        public List<RenovationProposal> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        private int NextId()
        {
            proposals = serializer.FromCSV(filePath);
            if (proposals.Count < 1)
                return 1;

            return proposals.Max(x => x.id) + 1;
        }
}
}
