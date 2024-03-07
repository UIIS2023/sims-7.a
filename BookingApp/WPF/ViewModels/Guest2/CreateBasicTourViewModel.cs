using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using BookingApp.WPF.ViewModels.Guide;
using BookingApp.WPF.Views.Guest2;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OxyPlot.Axes;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace BookingApp.WPF.ViewModels.Guest2
{
    public class CreateBasicTourViewModel : INotifyPropertyChanged
    {
        private int idUser;


        private DateTime displayDateStart;
        public DateTime DisplayDateStart
        {
            get { return displayDateStart; }
            set
            {
                displayDateStart = value;
                OnPropertyChanged(nameof(DisplayDateStart));
            }
        }


        private ObservableCollection<CreateBasicTour> _tours = new ObservableCollection<CreateBasicTour>();
        public ObservableCollection<CreateBasicTour> Tours
        {
            get { return _tours; }
            set
            {
                if (_tours != value)
                {
                    _tours = value;
                    OnPropertyChanged(nameof(Tours));
                }
            }
        }

        private StatisticsViewModel statisticsViewModel;
        public CreateBasicTour createBasicTour { get; set; }
        public ICommand FinishBasicTour { get; set; }
        public ICommand ViewStatistics { get; set; }
        public ICommand Cancel { get; set; }

        public CreateBasicTourViewModel()
        {
            DisplayDateStart = DateTime.Today.AddDays(2);

            statisticsViewModel = new StatisticsViewModel();
            createBasicTour = new CreateBasicTour();
            FinishBasicTour = new RelayCommand(CreateBasicTour, CanFinishBasicTour);
            ViewStatistics = new RelayCommand(ViewStatisticsExecute);
            Cancel = new RelayCommand(CancelExecute);
            LoadBasicTour();      
        }

        private void CancelExecute()
        {
            CustomerToursWin customerToursWin = new CustomerToursWin(idUser);
            customerToursWin.Show();
            App.Current.MainWindow.Close();
        }
        private void ViewStatisticsExecute()
        {
            var statusCount = new Dictionary<Status, int>();
            foreach (CreateBasicTour tour in Tours)
            {
                if (statusCount.ContainsKey(tour.status))
                {
                    statusCount[tour.status]++;
                }
                else
                {
                    statusCount[tour.status] = 1;
                }
            }

            var pieChartStatus = new PlotModel { Title = "Status" };
            var seriesStatus = new PieSeries();
            foreach (KeyValuePair<Status, int> pairStatus in statusCount)
            {
                seriesStatus.Slices.Add(new PieSlice(pairStatus.Key.ToString(), pairStatus.Value));
            }
            pieChartStatus.Series.Add(seriesStatus);


            var languageCount = new Dictionary<string, int>();
            foreach (CreateBasicTour tour in Tours)
            {
                if (languageCount.ContainsKey(tour.language))
                {
                    languageCount[tour.language]++;
                }
                else
                {
                    languageCount[tour.language] = 1;
                }
            }

            var barChartLanguage = new PlotModel { Title = "Language" };
            var seriesLanguage = new PieSeries();
            foreach (KeyValuePair<string, int> pairLanguage in languageCount)
            {
                seriesLanguage.Slices.Add(new PieSlice(pairLanguage.Key.ToString(), pairLanguage.Value));
            }
            barChartLanguage.Series.Add(seriesLanguage);


            var locationCount = new Dictionary<string, int>();
            foreach (CreateBasicTour tour in Tours)
            {
                if (locationCount.ContainsKey(tour.city))
                {
                    locationCount[tour.city]++;
                }
                else
                {
                    locationCount[tour.city] = 1;
                }
            }
            var pieChartLocation = new PlotModel { Title = "Location" };
            var seriesLocation = new PieSeries();
            foreach (KeyValuePair<string, int> pairLocation in locationCount)
            {
                seriesLocation.Slices.Add(new PieSlice(pairLocation.Key.ToString(), pairLocation.Value));
            }
            pieChartLocation.Series.Add(seriesLocation);

            var statisticsWin = new StatisticsWin();
            var statisticsViewModel = statisticsWin.DataContext as StatisticsViewModel;

            statisticsViewModel.PieChartStatus = pieChartStatus;
            statisticsViewModel.BarChartLanguage = barChartLanguage;
            statisticsViewModel.PieChartLocation = pieChartLocation;

            var avgGuests = CalculateAverageNumberOfGuests();
            statisticsViewModel.AverageNumberOfGuests = avgGuests.ToString();

            statisticsWin.Show();
            

        }

        private bool CanFinishBasicTour()
        {
            return !string.IsNullOrEmpty(createBasicTour.city) && !string.IsNullOrEmpty(createBasicTour.state) && !string.IsNullOrEmpty(createBasicTour.language) && createBasicTour.numberOfGuests != 0 && createBasicTour.start.HasValue && createBasicTour.end.HasValue && createBasicTour.start < createBasicTour.end && !string.IsNullOrEmpty(createBasicTour.description);
        }

        private void CreateBasicTour()
        {
            string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\BasicTours.csv";

            createBasicTour.status = Status.ON_WAIT;
            int nextId = GetNextBasicTourId();

            string lines = nextId.ToString() + "|" + createBasicTour.city + "|" + createBasicTour.state + "|" + createBasicTour.language + "|" + createBasicTour.numberOfGuests.ToString() + "|" + createBasicTour.description + "|" + createBasicTour.start.ToString() + "|" + createBasicTour.end.ToString()  + "|" + createBasicTour.status + "|" + createBasicTour.appointment.ToString();
            File.AppendAllText(filePath, lines + Environment.NewLine);
            
            Tours.Add(createBasicTour);
            LoadBasicTour();
            MessageBox.Show("Basic tour is created. Thank you!");

        }

        private int GetNextBasicTourId()
        {
            string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\BasicTours.csv";
            int lastId = 0;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1];
                    string[] parts = lastLine.Split('|');
                    if (int.TryParse(parts[0], out lastId))
                    {
                        lastId++;
                    }
                }
            }

            return lastId;
        }

        public void LoadBasicTour()
        {
            string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\BasicTours.csv";
            string[] lines = File.ReadAllLines(filePath);

            Tours.Clear();

            foreach (string line in lines)
            {
                string[] values = line.Split('|');

                CreateBasicTour tour = new CreateBasicTour()
                {
                    id = int.Parse(values[0]),
                    city = values[1],
                    state = values[2],
                    language = values[3],
                    numberOfGuests = int.Parse(values[4]),
                    description = values[5],
                    start = DateTime.Parse(values[6]),
                    end = DateTime.Parse(values[7]),                    
                    status = (Status)Enum.Parse(typeof(Status), values[8]),
                    appointment = string.IsNullOrEmpty(values[9]) ? (DateTime?)null : DateTime.Parse(values[9])
                };

                Tours.Add(tour);
            }
        }

        public double CalculateAverageNumberOfGuests()
        {
            double totalNumberOfGuests = 0;
            int acceptedToursCount = 0;
            foreach (CreateBasicTour tour in Tours)
            {
                if (tour.status == Status.ACCEPTED)
                {
                    acceptedToursCount++;
                    totalNumberOfGuests += tour.numberOfGuests;
                }
            }
            if (acceptedToursCount > 0)
            {
                return totalNumberOfGuests / acceptedToursCount;
            }
            else
            {
                return 0;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
