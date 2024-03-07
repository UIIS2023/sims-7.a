using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.Models.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Owner
{
    public interface IRenovationOwnRepository
    {
        public Renovation Save(Renovation renovation);
        public List<Renovation> GetAll();

        public void Delete(Renovation renovation);

    }
}

