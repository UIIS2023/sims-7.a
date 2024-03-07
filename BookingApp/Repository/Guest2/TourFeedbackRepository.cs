using BookingApp.Domain.RepositoryInterfaces.Guest2;
using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Guest2
{
    public class TourFeedbackRepository:ITourFeedbackRepository
    {
        public int idUser;

        private const string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\TourFeedback.csv";
        private readonly Serializer<TourFeedback> serializer;

        public TourFeedbackRepository(int idUser)
        {
            serializer = new Serializer<TourFeedback>();
            this.idUser = idUser;
        }

        public TourFeedback Add(TourFeedback feedback)
        {
            feedback.id = NextId();
            string line = feedback.url + "|" + feedback.knowledgeGrade.ToString() + "|" + feedback.languageGrade.ToString() + "|" + feedback.tourGrade.ToString() + "|" + feedback.comment + "|" + idUser;
            File.AppendAllText(filePath, line + Environment.NewLine);
            return feedback;
        }

        private int NextId()
        {
            var feedbacks = serializer.FromCSV(filePath);
            if (feedbacks.Count < 1)
                return 1;

            return feedbacks.Max(f => f.id) + 1;
        }
    }
}
