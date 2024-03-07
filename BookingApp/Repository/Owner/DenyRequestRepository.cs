using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Domain.Models.Owner;


namespace BookingApp.Repository.Owner
{
    public class DenyRequestRepository   //fali interfejs
    {
        public const string filePath = "../../../Resources/Data/denyRequests.csv";
        private readonly Serializer<DenyRequest> serializer;
        private List<DenyRequest> denyRequests;


        public DenyRequestRepository()
        {
            serializer = new Serializer<DenyRequest>();
            denyRequests = serializer.FromCSV(filePath);
        }

        public int NextId()
        {
            denyRequests = serializer.FromCSV(filePath);
            if (denyRequests.Count < 1)
            {
                return 1;
            }
            return denyRequests.Max(c => c.id) + 1;


        }

    

        public DenyRequest Save(DenyRequest d)
        {
            d.id = NextId();
            denyRequests = serializer.FromCSV(filePath);
            denyRequests.Add(d);
            serializer.ToCSV(filePath, denyRequests);

            return d;
        }

    }
}
