using InitialProject.Serializer;


namespace BookingApp.Domain.Models
{
    public enum IMG_USER_TYPE
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
        public IMG_USER_TYPE imageUserType { get; set; }

        public Image() { }

        public Image(string url, int resourceId,IMG_USER_TYPE imageUserType)
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
    

        private IMG_USER_TYPE ParseImageUserType(string inputStr)
        {
            switch (inputStr)
            {
                case "Accomodation":
                    return IMG_USER_TYPE.Accomodation;
                case "Tour":
                    return IMG_USER_TYPE.Tour;
                default:
                    return IMG_USER_TYPE.Accomodation;
            }
        
        }
    }
}