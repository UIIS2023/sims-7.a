using BookingApp.Application.UseCases;
using BookingApp.Application.UseCases.Guide;
using BookingApp.Domain.Models.Guest2;
using BookingApp.WPF.Views.Guide;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class TourRequestStatisticsViewModel : BaseViewModel
    {
        private readonly TourRequestService _tourRequestService;
        private readonly LocationService _locationService;
        public ICommand ApplyFiltersClick { get; private set; }
        public ICommand CreateTourByStatisticsClick { get; private set; }
        public List<CreateBasicTour> TourRequests { get; set; }
        public int guideId { get; set; }
        public TourRequestStatisticsViewModel(int guideId) 
        {
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();
            ApplyFiltersClick = new RelayCommand(ApplyFilters_Click);
            CreateTourByStatisticsClick = new RelayCommand(CreateTourByStatistics_Click);
            TourRequests = new List<CreateBasicTour>(_tourRequestService.GetAll());
            FilteredNumber = 0;
            this.guideId = guideId;
            FillYearsMonths();
            FillCountries();
        }

        private void CreateTourByStatistics_Click()
        {
            CreateTourByStatisticsWin createTourByStatisticsWin = new CreateTourByStatisticsWin(guideId);
            createTourByStatisticsWin.ShowDialog();
        }

        private void FillCountries()
        {
            Countries = new List<string>(_locationService.GetCountries());
        }

        private void FillYearsMonths()
        {
            Years = new List<string>();
            Months = new List<string>();
            Years.Add("23");
            Years.Add("22");
            Months.Add("Jan");
            Months.Add("Feb");
            Months.Add("Mar");
            Months.Add("May");
            Months.Add("Jun");
            Months.Add("Jul");
            Months.Add("Aug");
            Months.Add("Sep");
            Months.Add("Oct");
            Months.Add("Nov");
            Months.Add("Dec");
        }


        public List<string> Countries { get; set; }
        private List<string> _cities;
        public List<string> Cities 
        { 
            get => _cities;
            set 
            {
                if (value != _cities) 
                {
                    _cities = value;
                    OnPropertyChanged(nameof(Cities));
                }
            } 
        }
        public List<string> Years { get; set; }
        public List<string> Months { get; set; }

        private string _selectedYear;
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    IsMonthComboBoxEnabled = SelectedYear != null;
                }
            }
        }
        private string _selectedMonth;
        public string SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    OnPropertyChanged(nameof(SelectedMonth));
                }
            }
        }
        private string _languageInput;
        public string LanguageInput
        {
            get => _languageInput;
            set
            {
                if (value != _languageInput)
                {
                    _languageInput = value;
                    OnPropertyChanged(nameof(LanguageInput));
                }
            }
        }
        private int _filteredNumber;
        public int FilteredNumber
        {
            get => _filteredNumber;
            set
            {
                if(value != _filteredNumber)
                {
                    _filteredNumber = value;
                    OnPropertyChanged(nameof(FilteredNumber));
                }
            }
        }
        private bool _isMonthComboBoxEnabled;
        public bool IsMonthComboBoxEnabled
        {
            get { return _isMonthComboBoxEnabled; }
            set
            {
                _isMonthComboBoxEnabled = value;
                OnPropertyChanged(nameof(IsMonthComboBoxEnabled));
            }
        }
        private bool _isCityComboBoxEnabled;
        public bool IsCityComboBoxEnabled
        {
            get { return _isCityComboBoxEnabled; }
            set
            {
                _isCityComboBoxEnabled = value;
                OnPropertyChanged(nameof(IsCityComboBoxEnabled));
            }
        }

        private string _city = string.Empty;
        public string SelectedCity
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }
        }
        private string _country = string.Empty;
        public string SelectedCountry
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged(nameof(SelectedCountry));
                    IsCityComboBoxEnabled = SelectedCountry != null;
                    Cities = new List<string>(_locationService.GetCitiesByCountry(SelectedCountry));
                }
            }
        }

        private void ApplyFilters_Click()
        {
            List<CreateBasicTour> query = TourRequests;

            if (!string.IsNullOrEmpty(SelectedCity))
                query = new List<CreateBasicTour>(query.Where(x => x.city.ToLower().Contains(SelectedCity.ToLower())));
            if (!string.IsNullOrEmpty(SelectedCountry))
                query = new List<CreateBasicTour>(query.Where(x => x.state.ToLower().Contains(SelectedCountry.ToLower())));
            if (!string.IsNullOrEmpty(LanguageInput))
                query = new List<CreateBasicTour>(query.Where(x => x.language.ToLower().Contains(LanguageInput.ToLower())));
            if (!string.IsNullOrEmpty(SelectedYear))
                query = new List<CreateBasicTour>(query.Where(x => x.start.ToString().ToLower().Contains(SelectedYear.ToLower())));
            if (!string.IsNullOrEmpty(SelectedMonth))
                query = new List<CreateBasicTour>(query.Where(x => x.start.ToString().ToLower().Contains(SelectedMonth.ToLower())));


            FilteredNumber = query.Count();
            OnPropertyChanged();
        }



    }
}
