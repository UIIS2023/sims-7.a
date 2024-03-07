using System.Collections.Generic;
using System.Windows.Controls;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.WPF.Views.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingApp.WPF.ViewModels.Guest1;

public class ReservationRequestsViewModel : ObservableObject
{
    private readonly ReservationRequestService requestService;
    private readonly int userId;

    public bool ttEnabled { get; set; }
    private HelpService helpService;

    public ReservationRequestsViewModel(int userId, Frame frame)
    {
        this.userId = userId;

        requestService = new ReservationRequestService();
        requests = requestService.GetByUserId(userId);
        selectedRequest = new MoveReservationRequest();

        helpService = new HelpService();
        ttEnabled = helpService.GetByUserId(userId).ttEnabled;

        frame.ContentRendered += (s, e) =>
        {
            requests = requestService.GetByUserId(userId);
            OnPropertyChanged();
        };

        showRequestInfoCommand = new RelayCommand<MoveReservationRequest>(request =>
        {
            selectedRequest = request;
            ShowRequestInfo();
        });
    }

    public RelayCommand<MoveReservationRequest> showRequestInfoCommand { get; set; }

    public List<MoveReservationRequest> requests { get; set; }
    public MoveReservationRequest selectedRequest { get; set; }


    public void ShowRequestInfo()
    {
        if (selectedRequest == null) return;

        var requestInfoWin = new RequestInfoWin(selectedRequest);
        requestInfoWin.ShowDialog();
    }
}