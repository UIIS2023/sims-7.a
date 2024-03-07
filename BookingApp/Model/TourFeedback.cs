using InitialProject.Model;
using InitialProject.Serializer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourFeedback: ISerializable
    {
        public int id { get; set; }
        public string guestName { get; set; }
        public string tourName { get; set; }
        public string url { get; set; }
        public double knowledgeGrade { get; set; }
        public double tourGrade { get; set; }
        public double languageGrade { get; set; }
        public string comment { get; set; }
        public List<Image> images { get; set; }

        public TourFeedback() { }
        public TourFeedback (string guestName, string tourName, double knowledgeGrade, double tourGrade, double languageGrade, string comment)
        {
            
            this.guestName= guestName;  
            this.tourName= tourName;          
            this.knowledgeGrade= knowledgeGrade;
            this.tourGrade= tourGrade;
            this.languageGrade= languageGrade;
            this.comment= comment;
           
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                {                  
                    url,
                    knowledgeGrade.ToString(),
                    languageGrade.ToString(),
                    tourGrade.ToString(),               
                    comment  
                    
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {           
            url = values[0];
            knowledgeGrade = double.Parse(values[1]);
            languageGrade = double.Parse(values[2]);
            tourGrade = double.Parse(values[3]);           
            comment = values[4];
        }
    }
}
