using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.ViewModels.Guest1;

namespace BookingApp.WPF.Views.Guest1.MainWinPages;

/// <summary>
///     Interaction logic for RequestsPage.xaml
/// </summary>
public partial class RequestsPage : Page
{
    private readonly ReservationRequestsViewModel requestsViewModel;

    public RequestsPage(int userId, Frame frame)
    {
        InitializeComponent();
        requestsViewModel = new ReservationRequestsViewModel(userId, frame);
        DataContext = requestsViewModel;
    }


}