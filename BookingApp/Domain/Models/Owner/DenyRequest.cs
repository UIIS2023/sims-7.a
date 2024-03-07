using BookingApp.Domain.Models.Guest1;
using InitialProject.Serializer;
using System.Xml.Linq;

namespace BookingApp.Domain.Models.Owner
{
    public class DenyRequest : ISerializable
    {
        public int id { get; set; } 
        public string reason { get; set; }
        MoveReservationRequest request { get; set; }

        public DenyRequest() 
        {
            request = new MoveReservationRequest();
        }

        public DenyRequest(MoveReservationRequest request, string reason)
        {
            this.request = request;
            this.reason = reason;   
        }

        public string[] ToCSV()
        {
            string[] csValues =
            {
                id.ToString(),
                request.id.ToString(),
                reason
            };
            return csValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            request.id= int.Parse(values[1]);   
            reason = values[2]; 
        }
    }
}
