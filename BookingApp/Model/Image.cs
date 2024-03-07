using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum USER_TYPE
    {
        Accomodation,
        Tour,
        AccommodationReview
    }

    public class Image : ISerializable
    {
        public int id { get; set; }
        public string url { get; set; }
        public int resourceId { get; set; }
        public USER_TYPE imageUserType { get; set; }

        public Image() { }

        public Image(string url, int resourceId,USER_TYPE imageUserType)
        {
            this.url = url;
            this.resourceId = resourceId;
            this.imageUserType = imageUserType;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                {
                    id.ToString(),                   
                    resourceId.ToString(),
                    imageUserType.ToString(),
                    url
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            resourceId = int.Parse(values[1]);
            imageUserType = ParseImageUserType(values[2]);
            url = values[3];
        }
    

        private USER_TYPE ParseImageUserType(string inputStr)
        {
            switch (inputStr)
            {
                case "Accomodation":
                    return USER_TYPE.Accomodation;
                case "Tour":
                    return USER_TYPE.Tour;
                default:
                    return USER_TYPE.Accomodation;
            }
        
        }
    }
}