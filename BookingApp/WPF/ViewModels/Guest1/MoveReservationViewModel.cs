using System;
using System.ComponentModel;
using System.Windows.Controls;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingApp.WPF.ViewModels.Guest1;

public class MoveReservationViewModel : ObservableObject,IDataErrorInfo
{
    private readonly ValidDatesService datesService;

    private readonly ReservationRequestService requestService;


    public MoveReservationViewModel(AccommodationReservation reservation)
    {
        requestService = new ReservationRequestService();
        datesService = new ValidDatesService();
        this.reservation = reservation;

        MoveReservationCommand = new RelayCommand<ICloseable>(o => MoveReservation(o));
        CancelCommand = new RelayCommand<ICloseable>(o => CloseWindow(o));
    }

    public RelayCommand<ICloseable> MoveReservationCommand { get; set; }
    public RelayCommand<ICloseable> CancelCommand { get; set; }

    public AccommodationReservation reservation { get; set; }


    public DateTime? newStartDate { get; set; }
    public DateTime? newEndDate { get; set; }

    public DateTime? NewStartDate
    {
        get => newStartDate;

        set
        {
            newStartDate = value;
            newEndDate = null;
            OnPropertyChanged();
        }
    }


    public void GetValidDates(DatePicker datePicker)
    {
        datesService.BlackOutDates(datePicker, reservation.accomodation.id);
    }

    public void GetValidEndDates(DatePicker datePicker, DateTime startDate)
    {
        datePicker.BlackoutDates.Clear();
        GetValidDates(datePicker);
        datesService.BlackOutForbiddenEndDates(datePicker, startDate, reservation);
    }

    public void MoveReservation(ICloseable window)
    {
        if (newStartDate == null || newEndDate == null) return;

        var request =
            new MoveReservationRequest(reservation.idGuest, reservation, newStartDate.Value, newEndDate.Value);
        requestService.Save(request);

        window.Close();
    }

    public void CloseWindow(ICloseable window)
    {
        window.Close();
    }

    public string Error => null;


    public string this[string columnName]
    {
        get
        {
            string msg = null;
            switch (columnName)
            {
                case "NewStartDate":
                    if (newStartDate == null)
                        msg = "Must pick valid date!";
                    break;
                case "newEndDate":
                    if (newEndDate == null)
                        msg = "Must pick valid date!";
                    break;
                

            }
            return msg;
        }
    }

}