using BookingApp.Model;
using BookingApp.Repository.Guest2;
using BookingApp.View.Guide;
using BookingApp.WPF.ViewModels.Guide;
using BookingApp.WPF.Views.Guest2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guest2
{
    public class TourFeedbackViewModel: INotifyPropertyChanged
    {
        public int idUser;
        public TourFeedback tourFeedback { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }
      
        TourFeedbackRepository tourFeedbackRepository;
        public TourFeedbackViewModel(int idUser)
        {
          
            tourFeedback = new TourFeedback();
            tourFeedbackRepository = new TourFeedbackRepository(idUser);
            SubmitCommand = new RelayCommand(Submit, CanSubmit);
            CancelCommand = new RelayCommand(CancelExecute);

            this.idUser = idUser;
        }

        private void CancelExecute()
        {
        }

        private bool CanSubmit()
        {
            return !string.IsNullOrEmpty(tourFeedback.comment) && !string.IsNullOrEmpty(tourFeedback.url) && tourFeedback.knowledgeGrade != 0 && tourFeedback.languageGrade != 0 && tourFeedback.tourGrade != 0;
        }

        private void Submit()
        {
            
            tourFeedbackRepository.Add(tourFeedback);       
            MessageBox.Show("Your form is submited. Thank you!");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
