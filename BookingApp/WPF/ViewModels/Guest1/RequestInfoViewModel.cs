using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using CommunityToolkit.Mvvm.Input;

namespace BookingApp.WPF.ViewModels.Guest1;

public class RequestInfoViewModel
{
    public bool ttVisibility { get; set; }

    public bool ttEnabled { get; set; }
    private HelpService helpService;

    public RequestInfoViewModel(MoveReservationRequest request)
    {
        this.request = request;

        CloseWindowCommand = new RelayCommand<ICloseable>(o => { CloseWindow(o); });

        helpService = new HelpService();
        ttEnabled = helpService.GetByUserId(request.userId).ttEnabled;

        ttVisibility = false;
        if (request.status == REQUEST_STATUS.Pending) ttVisibility = true;

        ttVisibility = ttVisibility && ttEnabled;


    }

    

    public RelayCommand<ICloseable> CloseWindowCommand { get; set; }

    public MoveReservationRequest request { get; set; }


    private void CloseWindow(ICloseable window)
    {
        window.Close();
    }
}