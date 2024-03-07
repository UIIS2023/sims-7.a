using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models
{
    public class ForumThread : ISerializable
    {

        public int id { get; set; }
        public string threadName { get; set; }
        public bool isOpen { get; set; }
        public User opUser { get; set; }
        public Location forLocation { get; set; }
        public DateTime startDate { get; set; }
        public bool isImportant { get; set; }

        public string isOpenText {
            
            get
            {
                if (isOpen) return "Open";
                return "Closed";
            }

        }

        public ForumThread()
        {
            this.opUser = new User();
            this.forLocation = new Location();
        }

        public ForumThread(string threadName,bool isOpen,User opUser,Location forLocation,DateTime startDate)
        {
            this.threadName = threadName;
            this.isOpen = isOpen;
            this.opUser = opUser;
            this.forLocation = forLocation;
            this.startDate = startDate;
            
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                {
                        id.ToString(),
                        threadName,
                        isOpen.ToString(),
                        opUser.id.ToString(),
                        forLocation.idLocation.ToString(),
                        startDate.ToString("dd'/'MM'/'yyyy HH':'mm"),
                        isImportant.ToString()
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            threadName = values[1];
            isOpen = bool.Parse(values[2]);
            opUser.id = int.Parse(values[3]);
            forLocation.idLocation = int.Parse(values[4]);
            startDate = DateTime.ParseExact(values[5], "dd'/'MM'/'yyyy HH':'mm", CultureInfo.InvariantCulture);
            isImportant = bool.Parse(values[6]);
        }
        



    }
}
