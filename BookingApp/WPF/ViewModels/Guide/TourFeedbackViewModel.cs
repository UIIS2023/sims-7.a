using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guide
{
    internal class TourFeedbackViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TourFeedback> _feedbacks;
        public ObservableCollection<TourFeedback> Feedbacks
        {
            get { return _feedbacks; }
            set
            {
                _feedbacks = value;
                OnPropertyChanged(nameof(Feedbacks));
            }
        }
        public int guideId { get; set; }
        public TourFeedbackViewModel(int guideId)
        {
            Feedbacks = new ObservableCollection<TourFeedback>();
            Feedbacks.Add(new TourFeedback("Peter", "Discover Belgrade", 4.4, 4.7, 4.2, "Very interesting guide !"));
            Feedbacks.Add(new TourFeedback("Anna", "Discover Belgrade", 5.0, 5.0, 5.0, "I loved it !"));
            Feedbacks.Add(new TourFeedback("John", "Discover Belgrade", 3.8, 4.1, 3.3, "Not the best experience !"));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
