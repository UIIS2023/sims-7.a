using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.ViewModels.Guest1;

namespace BookingApp.WPF.Views.Guest1.MainWinPages;

/// <summary>
///     Interaction logic for ReservationsPage.xaml
/// </summary>
public partial class ReservationsPage : Page
{
    private readonly ReservationsViewModel reservationsViewModel;

    public ReservationsPage(int userId, Frame frame)
    {
        InitializeComponent();


        reservationsViewModel = new ReservationsViewModel(userId, frame);
        DataContext = reservationsViewModel;
    }


}