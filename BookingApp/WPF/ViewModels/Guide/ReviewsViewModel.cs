using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guide
{
    class ReviewsViewModel
    {
        public ObservableCollection<TourFeedback> Reviews { get; set; }
        public ReviewsViewModel(int guideId)
        {
            //string guestName, string tourName, double knowledgeGrade, double tourGrade, double languageGrade, string comment
            Reviews = new ObservableCollection<TourFeedback>();
            Reviews.Add(new TourFeedback("guest", "tour", 9.0, 8.5, 8.8, "very nice"));
            Reviews.Add(new TourFeedback("guest2", "tour", 9.0, 8.5, 8.8, "very nice"));
            Reviews.Add(new TourFeedback("guest3", "tour", 9.0, 8.5, 8.8, "very nice"));
        }
    }
}
