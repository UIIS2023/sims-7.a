using BookingApp.Domain.Models.Guest2;
using BookingApp.Model;
using BookingApp.Repository.Guest2;
using BookingApp.WPF.Views.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class BasicTourRequestsViewModel : BaseViewModel
    {
        private string _countryInput;
        public string CountryInput 
        { 
            get => _countryInput;
            set
            {
                if(value != _countryInput)
                {
                    _countryInput = value;
                    OnPropertyChanged(nameof(CountryInput));
                }
            }
        }

        private DateTime _fromInput;
        public DateTime FromInput
        {
            get => _fromInput;
            set
            {
                if(value != _fromInput)
                {
                    _fromInput = value;
                    OnPropertyChanged(nameof(FromInput));
                }
            }
        }

        private DateTime _toInput;
        public DateTime ToInput
        {
            get => _toInput;
            set
            {
                if (value != _toInput)
                {
                    _toInput = value;
                    OnPropertyChanged(nameof(ToInput));
                }
            }
        }
        private string _cityInput;
        public string CityInput
        {
            get => _cityInput;
            set
            {
                if (value != _cityInput)
                {
                    _cityInput = value;
                    OnPropertyChanged(nameof(CityInput));
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
        private int _numberOfGuestsInput;
        public int NumberOfGuestsInput
        {
            get => _numberOfGuestsInput;
            set
            {
                if (value != _numberOfGuestsInput)
                {
                    _numberOfGuestsInput = value;
                    OnPropertyChanged(nameof(NumberOfGuestsInput));
                }
            }
        }
        private ObservableCollection<CreateBasicTour> _tourRequests;
        public ObservableCollection<CreateBasicTour> TourRequests
        {
            get { return _tourRequests; }
            set
            {
                _tourRequests = value;
                OnPropertyChanged();
            }
        }
        private CreateBasicTourRepository _basicTourRepository;
        public ICommand DetailsClick { get; private set; }
        public ICommand ApplyFiltersClick { get; private set; }
        public CreateBasicTour SelectedTourRequest { get; set; }
        public int guideId { get; set; }

        public BasicTourRequestsViewModel(int guideId) 
        {
            _basicTourRepository = new CreateBasicTourRepository();
            TourRequests = new ObservableCollection<CreateBasicTour>(_basicTourRepository.GetAll());
            DetailsClick = new RelayCommand(Details_Click);
            ApplyFiltersClick = new RelayCommand(ApplyFilters_Click, CanExecute);
            this.guideId = guideId;
        }

        private Boolean CanExecute()
        {
            return true;
        }

        private void ApplyFilters_Click()
        {
            ObservableCollection<CreateBasicTour> query = TourRequests;

            if (!string.IsNullOrEmpty(CityInput))
                query = new ObservableCollection<CreateBasicTour>(query.Where(x => x.city.ToLower().Contains(CityInput.ToLower())));
            if (!string.IsNullOrEmpty(CountryInput))
                query = new ObservableCollection<CreateBasicTour>(query.Where(x => x.state.ToLower().Contains(CountryInput.ToLower())));
            if (!string.IsNullOrEmpty(LanguageInput))
                query = new ObservableCollection<CreateBasicTour>(query.Where(x => x.language.ToLower().Contains(LanguageInput.ToLower())));
            if (NumberOfGuestsInput != 0)
                query = new ObservableCollection<CreateBasicTour>(query.Where(x => x.numberOfGuests >= NumberOfGuestsInput));
            if (FromInput.Date != DateTime.Today.Date)
                query = new ObservableCollection<CreateBasicTour>(query.Where(x => x.start >= FromInput && x.end <= ToInput));

            TourRequests = query;
            OnPropertyChanged();
        }

        private void Details_Click()
        {
            if(SelectedTourRequest != null)
            {
                AcceptBasicTourWin acceptBasicTourWin = new AcceptBasicTourWin(guideId, SelectedTourRequest);
                acceptBasicTourWin.ShowDialog();
            }
        }
    }
}
