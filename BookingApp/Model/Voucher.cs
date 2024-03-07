using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Voucher : ISerializable
    {
        public int id { get; set; }
        public User guest { get; set; }
        public DateTime expireDate { get; set; }
        public Boolean used { get; set; }
       

        public Voucher() 
        {
            this.guest = new User();
        }
        public Voucher(int guestId, DateTime expireDate) 
        {
            this.guest = new User();
            this.guest.id = guestId;
            this.expireDate = expireDate;
            this.used = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                { 
                    id.ToString(),
                    guest.id.ToString(),
                    expireDate.ToString(),
                    used.ToString(),
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            guest.id = Convert.ToInt32(values[1]);
            expireDate = Convert.ToDateTime(values[2]);
            used = Convert.ToBoolean(values[3]);
        }
    }
}
