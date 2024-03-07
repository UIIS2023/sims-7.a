using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using BookingApp.WPF.ViewModels.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModels.Guest2
{
    public class CreateComplexTourViewModel : INotifyPropertyChanged
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

        private ObservableCollection<CreateComplexTour> _tours = new ObservableCollection<CreateComplexTour>();
        public ObservableCollection<CreateComplexTour> Tours
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

        public List<CreateComplexTour> Stations { get; set; }
        public CreateComplexTour createComplexTour { get; set; }  
        public ICommand AddStation { get; set; }
        public ICommand FinishComplexTour { get; set; }
        public ICommand Cancel { get; set; }

        public CreateComplexTourViewModel()
            
        {           
            DisplayDateStart = DateTime.Today.AddDays(2);

            Stations = new List<CreateComplexTour>();
            createComplexTour = new CreateComplexTour();              
            FinishComplexTour = new RelayCommand(FinishComplex);
            AddStation = new RelayCommand(CreateStation, CanFinishStation);
            Cancel = new RelayCommand(CancelExecute);
            LoadComplexTours();
        }

        private void CancelExecute()
        {
            CustomerToursWin customerToursWin = new CustomerToursWin(idUser);
            customerToursWin.Show();
            App.Current.MainWindow.Close();
        }

        private bool CanFinishStation()
        {
            return !string.IsNullOrEmpty(createComplexTour.city) && !string.IsNullOrEmpty(createComplexTour.state) && !string.IsNullOrEmpty(createComplexTour.language) && createComplexTour.numberOfGuests != 0 && createComplexTour.start.HasValue && createComplexTour.end.HasValue && createComplexTour.start < createComplexTour.end && !string.IsNullOrEmpty(createComplexTour.description);
        }

        private void CreateStation()
        {
            Stations.Add(createComplexTour);                     
            MessageBox.Show("Your station is created. Thank you!");

        }

        public void FinishComplex()
        {
            if (Stations.Count == 0)
            {
                MessageBox.Show("No stations added. Please add stations before finishing the complex tour.");
                return;
            }
            //createComplexTour.status = StatusComplex.ON_WAIT;           
            createComplexTour.idComplex = GetNextComplexTourId();
            createComplexTour.Stations.AddRange(Stations);
            
            string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\ComplexTours.csv";
            string line = createComplexTour.idComplex.ToString() + "|" + GetStationsData(createComplexTour.Stations);
            File.AppendAllText(filePath, line + Environment.NewLine);

            MessageBox.Show("Complex tour created successfully. Thank you!");
            Stations.Clear();
            
            Tours.Add(createComplexTour);
        }
      
        public void LoadComplexTours()
        {
            string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\ComplexTours.csv";
            string[] lines = File.ReadAllLines(filePath);

            Tours.Clear();

            foreach (string line in lines)
            {
                string[] values = line.Split('|');

                CreateComplexTour complexTour = new CreateComplexTour()
                {
                    idComplex = int.Parse(values[0]),
                    Stations = new List<CreateComplexTour>()
                   
                };
              
                for (int i = 1; i < values.Length - 1; i += 9)
                {
                    CreateComplexTour station = new CreateComplexTour()
                    {
                        id = int.Parse(values[i]),
                        city = values[i + 1],
                        state = values[i + 2],
                        language = values[i + 3],
                        numberOfGuests = int.Parse(values[i + 4]),
                        description = values[i + 5],
                        start = DateTime.Parse(values[i + 6]),
                        end = DateTime.Parse(values[i + 7]),
                        status = (StatusComplex)Enum.Parse(typeof(StatusComplex), values[i + 8])                      
                    };

                   complexTour.Stations.Add(station);
                   
                }

                Tours.Add(complexTour);   
                
            }
        }

        private string GetStationsData(List<CreateComplexTour> stations)
        {
             StringBuilder sb = new StringBuilder();
             int nextId = GetNextComplexTourId();
             foreach (CreateComplexTour station in stations)
             {

                string line = nextId.ToString() + "|" + station.city + "|" + station.state + "|" + station.language + "|" + station.numberOfGuests.ToString() + "|" + station.description + "|" + station.start.ToString() + "|" + station.end.ToString() + "|" + station.status + "|";// + "|" + station.appointment.ToString() + "|";
                 sb.Append(line);
                 nextId++;
             }

             return sb.ToString();
                     
        }


        private int GetNextComplexTourId()
        {
            string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\ComplexTours.csv";
            int lastId = 0;
            int lineCount = 0;

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] parts = line.Split('|');
                            if (int.TryParse(parts[0], out lastId))
                            {
                                lastId++;
                            }
                        }

                        lineCount++;

                        if (lineCount >= 100)
                            break;
                    }
                }
            }

            return lastId;
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
