using BookingApp.WPF.ViewModels.Guide;
using BookingApp.WPF.Views.Guest2;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guest2
{
    public class StatisticsViewModel: INotifyPropertyChanged
    {
        public string AverageNumberOfGuests { get; set; }
        public PlotModel PieChartStatus { get; set; }
        public PlotModel BarChartLanguage { get; set; }
        public PlotModel PieChartLocation { get; set; }

        public ICommand Ok { get; set; }

        public StatisticsViewModel() 
        {
            Ok = new RelayCommand(OkExecute);
        }


        private void OkExecute()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
