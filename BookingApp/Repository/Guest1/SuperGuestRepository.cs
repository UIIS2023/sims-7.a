using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using InitialProject.Serializer;

namespace BookingApp.Repository.Guest1
{
    public class SuperGuestRepository:ISuperGuestRepository
    {
        private const string filePath = "../../../Resources/Data/superGuestTitles.csv";

        private readonly Serializer<SuperGuestTitle> serializer;

        private List<SuperGuestTitle> guestTitles;

        public SuperGuestRepository()
        {
            serializer = new Serializer<SuperGuestTitle>();
            guestTitles = serializer.FromCSV(filePath);
        }

        public SuperGuestTitle Save(SuperGuestTitle guestTitle)
        {
            guestTitle.id = NextId();
            guestTitles = serializer.FromCSV(filePath);
            guestTitles.Add(guestTitle);
            serializer.ToCSV(filePath,guestTitles);

            return guestTitle;
        }

        public SuperGuestTitle GetById(int id)
        {
            return serializer.FromCSV(filePath).Find(x => x.id == id);
        }

        public SuperGuestTitle Update(SuperGuestTitle guestTitle)
        {
            guestTitles = serializer.FromCSV(filePath);

            SuperGuestTitle current = guestTitles.Find(x => x.id == guestTitle.id);
            int index = guestTitles.IndexOf(current);
            guestTitles.Remove(current);

            guestTitles.Insert(index,guestTitle);
            serializer.ToCSV(filePath,guestTitles);

            return guestTitle;
        }

        private int NextId()
        {
            guestTitles = serializer.FromCSV(filePath);
            if (guestTitles.Count < 1)
                return 1;
            return guestTitles.Max(x => x.id) + 1;
        }

    }
}
