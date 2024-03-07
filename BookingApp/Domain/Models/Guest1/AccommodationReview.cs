using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models.Guest1
{
    public class AccommodationReview:ISerializable
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int accommodationId { get; set; }
        public int cleanlinessScore { get; set; } // score 1-5
        public int ownerScore { get; set; }
        public string comment { get; set; }
        public List<Image> images { get; set; }

        public AccommodationReview()
        {
            images = new List<Image>();
        }

        public AccommodationReview(int userId, int accommodationId, int cleanlinessScore, int ownerScore,
            string comment)
        {
            images = new List<Image>();

            this.userId = userId;
            this.accommodationId = accommodationId;
            this.cleanlinessScore = cleanlinessScore;
            this.ownerScore = ownerScore;
            this.comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                userId.ToString(),
                accommodationId.ToString(),
                cleanlinessScore.ToString(),
                ownerScore.ToString(),
                comment
            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            userId = int.Parse(values[1]);
            accommodationId = int.Parse(values[2]);
            cleanlinessScore = int.Parse(values[3]);
            ownerScore = int.Parse(values[4]);
            comment = values[5];
        }

    }
}
