using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingApp.Repository.Owner
{
   
    public class RenovationOwnRepository : IRenovationOwnRepository
    {
        private const string filePath = "../../../Resources/Data/renovation.csv";

        private readonly Serializer<Renovation> serializer;
        private List<Renovation> renovations;

        public RenovationOwnRepository()
        {
            serializer = new Serializer<Renovation>();
            renovations = serializer.FromCSV(filePath);
        }

        public Renovation Save(Renovation renovation)
        {
            renovation.id = NextId();
            renovations = serializer.FromCSV(filePath);
            renovations.Add(renovation);
            serializer.ToCSV(filePath, renovations);

            return renovation;
        }

        public void Delete(Renovation renovation)
        {
            renovations = serializer.FromCSV(filePath);
            Renovation r = renovations.Find(ac => ac.id == renovation.id);
            renovations.Remove(r);
            serializer.ToCSV(filePath, renovations);
        }

        public List<Renovation> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        private int NextId()
        {
            renovations = serializer.FromCSV(filePath);
            if (renovations.Count < 1)
                return 1;

            return renovations.Max(x => x.id) + 1;
        }
    }
}
