using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class NewForumThreadViewModel:ObservableObject,IDataErrorInfo
    {
        public string threadName { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string comment { get; set; }

        public string Country
        {
            set
            {
                country = value;
                cities = forumThreadService.GetAvailableCitiesByCountry(value);
                OnPropertyChanged("cities");
            }
            get { return country; }

        }

        public string City
        {
            set
            {
                city = value;
            }
            get { return city; }
        }

        private ForumThreadService forumThreadService;
        private ForumCommentService forumCommentService;
        private LocationService locationService;
        private int userId;

        public RelayCommand<ICloseable> ConfirmCommand { get; set; }
        public RelayCommand<ICloseable> CancelCommand { get; set; }

        public List<string> countries { get; set; }
        public List<string> cities { get; set; }
        


        public NewForumThreadViewModel(int userId)
        {
            forumThreadService = new ForumThreadService();
            forumCommentService = new ForumCommentService();
            locationService = new LocationService();

            this.userId = userId;
            countries = forumThreadService.GetAvailableCountries();

            CancelCommand = new RelayCommand<ICloseable>((window) => { Close(window); });
            ConfirmCommand = new RelayCommand<ICloseable>((window) => { Confirm(window); });
        }

        private void Close(ICloseable window) 
        {
            window.Close();
        }

        private void Confirm(ICloseable window)
        { 
        
            if(ConfirmValues())
            {
                Location location = new Location();
                location.idLocation = locationService.GetIdByCountryAndCity(city, country);

                User user = new User();
                user.id = userId;

                ForumThread forumThread = new ForumThread(threadName,true,user,location,DateTime.Now);
                forumThread = forumThreadService.Save(forumThread);

                ForumComment forumComment = new ForumComment(forumThread, user, comment, DateTime.Now, forumThread.threadName,forumCommentService.WasUserAtLocation(userId,forumThread.forLocation.idLocation));
                forumCommentService.Save(forumComment);

                window.Close();
            }
        
        
        }

        private bool ConfirmValues()
        {
            return !string.IsNullOrEmpty(threadName) &&
                   !string.IsNullOrEmpty(city) &&
                   !string.IsNullOrEmpty(country) &&
                   !string.IsNullOrEmpty(comment);
        }

        public string Error => null;


        public string this[string columnName]
        {
            get
            {
                string msg = null;
                switch (columnName)
                {
                    case "threadName":
                        if (string.IsNullOrWhiteSpace(threadName))
                            msg = "Title cannot be empty or white space!";
                        break;
                    case "comment":
                        if (string.IsNullOrWhiteSpace(comment))
                            msg = "Comment cannot be empty or white space!";
                        break;
                    case "Country":
                        if (string.IsNullOrWhiteSpace(country))
                            msg = "You must chose a country!";
                        break;
                    case "City":
                        if (string.IsNullOrWhiteSpace(city))
                            msg = "You must chose a city!";
                        break;

                }
                return msg;
            }
        }


    }
}
