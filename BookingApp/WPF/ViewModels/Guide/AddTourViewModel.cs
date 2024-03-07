using BookingApp.Controller;
using BookingApp.Domain.Models;
using BookingApp.Repository.Mutual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    internal class AddTourViewModel : BaseViewModel
    {
        private readonly TourController _tourcontroller;
        private readonly LocationRepository _locationRepository;
        public List<String> Countries;
        public int guideId { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public Boolean LanguageOption { get; set; }
        public string SelectedCountry { get; set; }
        public List<string> Cities { get; set; }
        
        public ICommand ConfirmClick { get; private set; }
        public ICommand AddDateClick { get; private set; }
        public ICommand AddMidPointClick { get; private set; }
        public ICommand AddImageClick { get; private set; }
        public bool IsCitiesComboBoxEnabled { get; set; }
        public Boolean IsTourRequested { get; set; }

        public AddTourViewModel(int guideId)
        {
            _tourcontroller = new TourController();
            _locationRepository = new LocationRepository();
            ConfirmClick = new RelayCommand(Confirm_Click);
            AddDateClick = new RelayCommand(AddDate_Click);
            AddMidPointClick = new RelayCommand(AddMidPoint_Click);
            AddImageClick = new RelayCommand(AddImage_Click);
            Countries = GetCountries();
            IsLanguageBoxEnabled = true;
            this.guideId = guideId;
            this.IsTourRequested = false;
        }

        public AddTourViewModel(int guideId, Boolean languageOption, string language)
        {
            _tourcontroller = new TourController();
            _locationRepository = new LocationRepository();
            ConfirmClick = new RelayCommand(Confirm_Click);
            AddDateClick = new RelayCommand(AddDate_Click);
            AddMidPointClick = new RelayCommand(AddMidPoint_Click);
            AddImageClick = new RelayCommand(AddImage_Click);
            Countries = GetCountries();
            this.guideId = guideId;
            this.LanguageOption = languageOption;
            this.language = language;
            SetFieldValues();
            this.IsTourRequested = true;

        }

        public AddTourViewModel(int guideId, Boolean languageOption, string city, string country)
        {
            _tourcontroller = new TourController();
            _locationRepository = new LocationRepository();
            ConfirmClick = new RelayCommand(Confirm_Click);
            AddDateClick = new RelayCommand(AddDate_Click);
            AddMidPointClick = new RelayCommand(AddMidPoint_Click);
            AddImageClick = new RelayCommand(AddImage_Click);
            Countries = GetCountries();
            this.guideId = guideId;
            this.LanguageOption = languageOption;
            this.city = city;
            this.country = country;
            SetFieldValues();
            this.IsTourRequested = true;

        }

        private void SetFieldValues()
        {
            if (LanguageOption)
            {
                LanguageBind = language;
                IsLanguageBoxEnabled = false;
                IsCountryBoxEnabled = true;
            }
            else
            {
                Country = country;
                City = city;
                IsLanguageBoxEnabled = true;
                IsCountryBoxEnabled = false;

            }
        }
        private bool _isLanguageBoxEnabled;
        public bool IsLanguageBoxEnabled
        {
            get { return _isLanguageBoxEnabled; }
            set
            {
                _isLanguageBoxEnabled = value;
                OnPropertyChanged(nameof(IsLanguageBoxEnabled));
            }
        }

        private bool _isCountryBoxEnabled;
        public bool IsCountryBoxEnabled
        {
            get { return _isCountryBoxEnabled; }
            set
            {
                _isCountryBoxEnabled = value;
                OnPropertyChanged(nameof(IsCountryBoxEnabled));
            }
        }
        private bool _isCityBoxEnabled;
        public bool IsCityBoxEnabled
        {
            get { return _isCityBoxEnabled; }
            set
            {
                _isCityBoxEnabled = value;
                OnPropertyChanged(nameof(IsCityBoxEnabled));
            }
        }

        public int GuideId { get; set; }

        private string _name = string.Empty;
        public string NameBind
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(NameBind));
                }
            }
        }

        private string _language = string.Empty;
        public string LanguageBind
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(LanguageBind));
                }
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        private int _maxguests;
        public int MaxGuests
        {
            get => _maxguests;
            set
            {
                if (value != _maxguests)
                {
                    _maxguests = value;
                    OnPropertyChanged(nameof(MaxGuests));
                }
            }
        }

        private DateTime _starttime;
        public DateTime StartTime
        {
            get => _starttime;
            set
            {
                if (value != _starttime)
                {
                    _starttime = value;
                    OnPropertyChanged(nameof(StartTime));
                }
            }
        }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }
        private string _city = string.Empty;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }
        private string _country = string.Empty;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }
        private string _startpointname = string.Empty;
        public string StartPointName
        {
            get => _startpointname;
            set
            {
                if (value != _startpointname)
                {
                    _startpointname = value;
                    OnPropertyChanged(nameof(StartPointName));
                }
            }
        }
        private string _midpointname = string.Empty;
        public string MidPointName
        {
            get => _midpointname;
            set
            {
                if (value != _midpointname)
                {
                    _midpointname = value;
                    OnPropertyChanged(nameof(MidPointName));
                }
            }
        }
        private string _endpointname = string.Empty;
        public string EndPointName
        {
            get => _endpointname;
            set
            {
                if (value != _endpointname)
                {
                    _endpointname = value;
                    OnPropertyChanged(nameof(EndPointName));
                }
            }
        }
        private string _imageUrl = string.Empty;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged(nameof(ImageUrl));
                }
            }
        }
        private List<DateTime> _dates = new List<DateTime>();
        public List<DateTime> Dates
        {
            get => _dates;
            set
            {
                if (value != _dates)
                {
                    _dates = value;
                    OnPropertyChanged(nameof(Dates));
                }
            }
        }
        private List<String> _midPointNames = new List<String>();
        public List<String> MidPointNames
        {
            get => _midPointNames;
            set
            {
                if (value != _midPointNames)
                {
                    _midPointNames = value;
                    OnPropertyChanged(nameof(MidPointNames));
                }
            }
        }
        private List<String> _images = new List<String>();
        public List<String> Images
        {
            get => _images;
            set
            {
                if (value != _images)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }

        //private void OnCountriesSelectionChanged()
        //{
        //    string selectedCountry = (string)SelectedCountry;

        //    var locations = _locationRepository.GetAll()
        //        .Where(location => location.country == selectedCountry);

        //    var cities = locations.Select(location => location.city);

        //    Cities = cities.ToList();

        //    IsCitiesComboBoxEnabled = true;
        //}

        private List<String> GetCountries()
        {
            List<String> countries = new List<String>();
            foreach (Location location in _locationRepository.GetAll())
            {
                if (!countries.Contains(location.country))
                {
                    countries.Add(location.country);
                }
            }

            return countries;
        }

        private void Confirm_Click()
        {
            //if (NameBind.IsNullOrEmpty() || Description.IsNullOrEmpty() || LanguageBind.IsNullOrEmpty() || Duration == 0 || City.IsNullOrEmpty() || Country.IsNullOrEmpty() || MaxGuests == 0 || StartPointName.IsNullOrEmpty() || EndPointName.IsNullOrEmpty())
            //{
            //    MessageBox.Show("You have to fill everything ! ");
            //    return;
            //}
            _tourcontroller.Create(GuideId, NameBind, Description, LanguageBind, StartTime, Duration, City, Country, MaxGuests, StartPointName, MidPointNames, EndPointName, Dates, Images, IsTourRequested, LanguageOption);

            //Close();
        }
        private void AddDate_Click()
        {
            Dates.Add(StartTime);
        }
        private void AddMidPoint_Click()
        {
            MidPointNames.Add(MidPointName);
        }
        private void AddImage_Click()
        {
            Images.Add(ImageUrl);
        }

    }
}
