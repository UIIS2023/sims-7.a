using BookingApp.Domain.Models.Guide;
using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class NewTourNotificationRepository
    {
        private const string filePath = "../../../Resources/Data/tournotifications.csv";
        private readonly Serializer<NewTourNotification> _serializer;
        private List<NewTourNotification> _notifications;

        public NewTourNotificationRepository()
        {
            _serializer = new Serializer<NewTourNotification>();
            _notifications = new List<NewTourNotification>();
        }

        public NewTourNotification Save(NewTourNotification notification)
        {
            notification.id = NextId();
            _notifications = _serializer.FromCSV(filePath);
            _notifications.Add(notification);
            _serializer.ToCSV(filePath, _notifications);
            return notification;
        }

        public int NextId()
        {
            _notifications = _serializer.FromCSV(filePath);
            if (_notifications.Count < 1)
            {
                return 1;
            }

            return _notifications.Max(l => l.id) + 1;
        }
    }
}
