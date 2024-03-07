using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View.Guide;
using BookingApp.WPF.ViewModels.Guide;
using BookingApp.WPF.Views.Guest2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guest2
{
    public class LiveTourViewModel: INotifyPropertyChanged
    {
        
        private readonly int idUser;
        private readonly TourAppointmentController tourAppointmentController;

        private ObservableCollection<TourPoint> tourPoints;
        public ObservableCollection<TourPoint> TourPoints
        {
            get => tourPoints;
            set
            {
                tourPoints = value;
                OnPropertyChanged(nameof(TourPoints));
            }
        }

        private TourAppointment liveTour;
        public TourAppointment LiveTour
        {
            get => liveTour;
            set
            {
                liveTour = value;
                OnPropertyChanged(nameof(LiveTour));
            }
        }

        public ICommand BackCommand => new RelayCommand(Back);
        public ICommand FeedbackCommand => new RelayCommand(Feedback);

        public LiveTourViewModel(int idUser)
        {
            this.idUser = idUser;
            tourAppointmentController = new TourAppointmentController();
           
            LoadTourPoints();
           
        }

        private async void LoadTourPoints()
        {
            string[] linesTourReservations = File.ReadAllLines("C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\TourReservations.csv");
            string[] linesTourAppointments = File.ReadAllLines("C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\tourappointments.csv");
            string[] linesTourAttendences = File.ReadAllLines("C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\tourattendences.csv");

            foreach (string lineTourReservations in linesTourReservations)
            {
                string[] valuesTourReservations = lineTourReservations.Split('|');

                int guestId = int.Parse(valuesTourReservations[4]);
                int appointmentId = int.Parse(valuesTourReservations[3]);

                if (guestId == idUser)
                {
                    foreach (string lineTourAppointments in linesTourAppointments)
                    {
                        string[] valuesTourAppointments = lineTourAppointments.Split('|');

                        int appointmentIdNew = int.Parse(valuesTourAppointments[0]);
                        int guideId = int.Parse(valuesTourAppointments[3]);
                        bool started = bool.Parse(valuesTourAppointments[4]);
                        bool ended = bool.Parse(valuesTourAppointments[5]);

                        if (appointmentId == appointmentIdNew && started && !ended)
                        {
                            LiveTour = tourAppointmentController.GetLiveTour(guideId);
                            TourPoints = new ObservableCollection<TourPoint>(LiveTour.tourPoints);
                   
                            await Task.Delay(500);

                            for (int i = 0; i < linesTourAttendences.Length; i++)
                            {
                                string[] valuesTourAttendences = linesTourAttendences[i].Split('|');

                                int appointmentIdFinal = int.Parse(valuesTourAttendences[1]);

                                if (appointmentIdNew == appointmentIdFinal)
                                {
                                    MessageBoxResult result = MessageBox.Show("Are you present?", "Presence", MessageBoxButton.YesNo, MessageBoxImage.Question);

                                    if (result == MessageBoxResult.Yes)
                                    {
                                        valuesTourAttendences[4] = "True";
                                        valuesTourAttendences[5] = "False";
                                    }
                                    else
                                    {
                                        valuesTourAttendences[4] = "False";
                                        valuesTourAttendences[5] = "False";
                                    }

                                    linesTourAttendences[i] = string.Join("|", valuesTourAttendences);
                                }
                            }
                        }
                        File.WriteAllLines("C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\tourattendences.csv", linesTourAttendences);                  
                        break;
                    }
                }
            }
        }

        public bool CheckAllTourPoints()
        {
            int uncheckedCount = 0;
            foreach (TourPoint tp in TourPoints)
            {
                if (!tp.status)
                {
                    uncheckedCount++;
                }
            }
            return uncheckedCount == 1;
        }

        private void Feedback()
        {
            //if (TourPoints != null && CheckAllTourPoints())
            //{
                TourFeedbackWin tourFeedbackWin = new TourFeedbackWin(idUser);
                tourFeedbackWin.Show();
               


            // }
            // else if (TourPoints == null)
            // {
            //    MessageBox.Show("TourPoints collection is not initialized.");
            // }
            // else
            // {
            //     MessageBox.Show("Please check all TourPoints before leaving feedback.");
            // }

        }

        private void Back()
        {
            CustomerToursWin main = new CustomerToursWin(idUser);
            main.Show();
            App.Current.MainWindow.Close();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
