using BookingApp.Application.UseCases;
using BookingApp.Application.UseCases.Guide;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class CreateTourByStatisticsViewModel : BaseViewModel
    {
        public ICommand CreateTourClick { get; private set; }
        private readonly TourRequestService _tourRequestService;
        private readonly LocationService _locationService;
        public int guideId { get; set; }
        public CreateTourByStatisticsViewModel(int guideId)
        {
            IsLanguageOptionSelected = true;
            IsLocationOptionSelected = false;
            CreateTourClick = new RelayCommand(CreateTour_Click);
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();
            this.guideId = guideId;
        }

        private void CreateTour_Click()
        {
            if(IsLanguageOptionSelected)
            {
                string mostRequestedLanguage = _tourRequestService.MostRequestedLanguage();
                AddTourWin addTourWin = new AddTourWin(guideId, IsLanguageOptionSelected, mostRequestedLanguage);
                addTourWin.Show();
                return;
            }
            else 
            { 
                string mostRequestedCity = _tourRequestService.MostRequestedLocation();
                string mostRequestedCountry = _locationService.GetCountryByCity(mostRequestedCity); 

                AddTourWin addTourWin = new AddTourWin(guideId, _isLanguageOptionSelected, mostRequestedCity, mostRequestedCountry);
                addTourWin.Show();
            }


        }

        private bool _isLanguageOptionSelected;
        public bool IsLanguageOptionSelected
        {
            get { return _isLanguageOptionSelected; }
            set
            {
                _isLanguageOptionSelected = value;
                OnPropertyChanged(nameof(IsLanguageOptionSelected));
            }
        }

        private bool _isLocationOptionSelected;
        public bool IsLocationOptionSelected
        {
            get { return _isLocationOptionSelected; }
            set
            {
                _isLocationOptionSelected = value;
                OnPropertyChanged(nameof(IsLocationOptionSelected));
            }
        }
    }
}
