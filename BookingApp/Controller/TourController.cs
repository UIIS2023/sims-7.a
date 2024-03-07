using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Controller
{
    public class TourController
    {
        private readonly TourService _tourService;


        public TourController()
        {
            _tourService = new TourService();
        }

        public void Create(int guideId, string name, string description, string language, DateTime startTime, int duration, string city, string country, int guests, string startPoint, List<string> midPoints, string endPoint, List<DateTime> dates, List<string> images, bool isTourRequested, bool LanguageOption)
        {
            _tourService.Create(new Tour(guideId, name, description, language, duration, city, country, guests), startPoint, endPoint, midPoints, dates, images, isTourRequested, LanguageOption);
        }


    }
}
