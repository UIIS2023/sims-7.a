using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Application.UseCases;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models.Owner;
using BookingApp.Model;
using CommunityToolkit.Mvvm.Input;

namespace BookingApp.WPF.Views.Guest1.MainWinPages;

/// <summary>
///     Interaction logic for AccommodationsPage.xaml
/// </summary>
public partial class AccommodationsPage : Page, INotifyPropertyChanged
{
    private AccommodationService accommodationService;
    private string searchCity;

    private string searchCountry;

    private readonly int userId;

    public bool ttEnabled { get; set; }
    private HelpService helpService;

    public RelayCommand<Accommodation> reserveCommand { get; set; }

    public AccommodationsPage(int userId)
    {
        InitializeComponent();
        InitializeRepositoriesAndLists();

        DataContext = this;

        this.userId = userId;

        helpService = new HelpService();
        ttEnabled = helpService.GetByUserId(userId).ttEnabled;

        reserveCommand = new RelayCommand<Accommodation>(accommodation =>
        {
            selectedAccommodation = accommodation;
            ReserveClick();
        });

    }

    public ObservableCollection<Accommodation> accommodations { get; set; }


    public Accommodation selectedAccommodation { get; set; }


    public string searchName { get; set; }
    public string searchType { get; set; }
    public int searchGuestNum { get; set; }
    public int searchDays { get; set; }

    public DateTime? startDate { get; set; }
    public DateTime? endDate { get; set; }
    public int searchGuestNumAny { get; set; }
    public int searchDaysAny { get; set; }

    public string SearchCountry
    {
        get => searchCountry;
        set
        {
            searchCountry = value;

            if (value != "All")
            {
                CityComboBox.IsEnabled = true;
                CityComboBox.ItemsSource = locations[searchCountry];
            }
            else
            {
                CityComboBox.IsEnabled = false;
                CityComboBox.SelectedItem = "All";
            }

            OnPropertyChanged();
        }
    }

    public string SearchCity
    {
        get => searchCity;
        set
        {
            searchCity = value;
            OnPropertyChanged();
        }
    }

    public List<string> accomodationTypes { get; set; }
    public Dictionary<string, List<string>> locations { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    private void InitializeRepositoriesAndLists()
    {
        accommodationService = new AccommodationService();

        accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());


        accomodationTypes = new List<string>();
        AddAccommodationTypesToList();

        locations = new Dictionary<string, List<string>>();
        UpdateLocationMap();
    }

    protected virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void AddAccommodationTypesToList()
    {
        accomodationTypes.Add("All");
        accomodationTypes.Add("Apartment");
        accomodationTypes.Add("House");
        accomodationTypes.Add("Cottage");
    }

    private void UpdateLocationMap()
    {
        var locationService = new LocationService();

        var locationsFromFile = locationService.GetAll();
        locations["All"] = new List<string>();

        foreach (var location in locationsFromFile)
        {
            var country = location.country;

            AddNewCountryToMap(country);

            locations[country].Add(location.city);
        }
    }

    private void AddNewCountryToMap(string country)
    {
        if (!locations.ContainsKey(country))
        {
            locations[country] = new List<string>();
            locations[country].Add("All");
        }
    }

    private void ApplyFilter_Click(object sender, RoutedEventArgs e)
    {
        IEnumerable<Accommodation> query = accommodations;

        if (!string.IsNullOrEmpty(searchName))
            query = query.Where(x => x.name.ToLower().Contains(searchName.ToLower()));

        if (searchType != "All" && !string.IsNullOrEmpty(searchType))
            query = query.Where(x => x.type.ToString() == searchType);

        if (searchCountry != "All" && !string.IsNullOrEmpty(searchCountry))
            query = query.Where(x => x.location.country == searchCountry);

        if (searchCity != "All" && !string.IsNullOrEmpty(searchCity))
            query = query.Where(x => x.location.city == searchCity);

        if (searchGuestNum > 0) query = query.Where(x => x.maxGuests >= searchGuestNum);

        if (searchDays > 0) query = query.Where(x => x.minDaysForStay <= searchDays);

        listView.ItemsSource = query;
        OnPropertyChanged();
    }



    private void ReserveClick()
    {
        if (selectedAccommodation != null)
        {
            var reservationWin = new ReservationWin(selectedAccommodation, userId);
            reservationWin.ShowDialog();
        }
    }

    private void SearchAll_Click(object sender, RoutedEventArgs e)
    {
        SearchAllStack.Visibility = Visibility.Visible;
        SearchAllStackBttn.Visibility = Visibility.Hidden;

        AnyWhereStack.Visibility = Visibility.Hidden;
        AnyWhereStackBttn.Visibility = Visibility.Visible;

        FilterLabel.Visibility = Visibility.Visible;
        AnyLabel.Visibility = Visibility.Hidden;



    }

    private void SearchAny_Click(object sender, RoutedEventArgs e)
    {
        SearchAllStack.Visibility = Visibility.Hidden;
        SearchAllStackBttn.Visibility = Visibility.Visible;

        AnyWhereStack.Visibility = Visibility.Visible;
        AnyWhereStackBttn.Visibility = Visibility.Hidden;

        AnyLabel.Visibility = Visibility.Visible;
        FilterLabel.Visibility = Visibility.Hidden;
    }

    private void AnyWhereAnyTimeFilter_Click(object sender, RoutedEventArgs e) {

        IEnumerable<Accommodation> query = accommodations;

        if (searchGuestNumAny > 0) query = query.Where(x => x.maxGuests >= searchGuestNumAny);

        if (searchDaysAny > 0) query = query.Where(x => x.minDaysForStay <= searchDaysAny);

        if(startDate != null && endDate != null) 
        {
            query = GetAccommodationsWithFreeDates(query);
        }

        listView.ItemsSource = query;
        OnPropertyChanged();
    }

    private IEnumerable<Accommodation> GetAccommodationsWithFreeDates(IEnumerable<Accommodation> query) {

        ObservableCollection<Accommodation> newQuery = new ObservableCollection<Accommodation>();

        foreach (var accommodation in query) 
        {
            if (GetNumOfFreeDatesForAccommodation(accommodation.id) > 0) newQuery.Add(accommodation);
        }
        
        return newQuery; 
    }

    private int GetNumOfFreeDatesForAccommodation(int id)
    {

        DateTime potentialDateStart = startDate.Value;
        DateTime potentialDateEnd = startDate.Value.AddDays(searchDaysAny);
        int freeDatesCnt = 0;

        while (endDate.Value.Date >= potentialDateEnd.Date && freeDatesCnt==0)
        {
            bool isDateConflicted = DateConflictExits(potentialDateStart, potentialDateEnd, id);

            if (!isDateConflicted) freeDatesCnt++;

            potentialDateStart = potentialDateEnd.AddDays(1);
            potentialDateEnd = potentialDateStart.AddDays(searchDaysAny);
        }

        return freeDatesCnt;

    }

    private bool DateConflictExits(DateTime potentialStart,DateTime potenitalEnd,int accommodationId)
    {
        ReservationService reservationService = new ReservationService();

        var reservations = reservationService.GetByAccommodationId(accommodationId);

        foreach(var reservation in reservations)
        {
            bool areDatesConfliced = reservation.startDate.Date <= potenitalEnd.Date && potentialStart.Date <= reservation.endDate.Date;

            if (areDatesConfliced)
                return true;

        }

        return false;


    }
   
}