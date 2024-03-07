using InitialProject.Serializer;
using System;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Domain.Models
{

    public enum USER_TYPE
    {
        Customer_Booking,
        Customer_Tours,
        Owner,
        Guide
    }

    public class User : ISerializable
    {

        public int id { get; set; }
        public string username { get; set; } 
        public string password { get; set; } 
        public USER_TYPE userType { get; set; }
        public SuperGuestTitle superGuestTitle { get; set; }

        public User()
        {
            superGuestTitle = new SuperGuestTitle();
        }

        public User(string username, string password, USER_TYPE usertype)
        {
            this.username = username; 
            this.password = password;
            this.userType = usertype;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                id.ToString(),
                username,
                password,
                userType.ToString(),
                superGuestTitle.id.ToString()
                
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            username = values[1];
            password = values[2];
            userType = Enum.Parse<USER_TYPE>(values[3]);
            superGuestTitle.id = int.Parse(values[4]);
        }

        
    }
}
