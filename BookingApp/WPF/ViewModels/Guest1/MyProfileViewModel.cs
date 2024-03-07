using System.Collections.Generic;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.WPF.ViewModels.Guide;

namespace BookingApp.WPF.ViewModels.Guest1;

public class MyProfileViewModel
{
    private readonly GuestReviewService guestReviewService;
    private readonly SuperGuestTitleService superGuestTitleService;

    public bool ttEnabled { get; set; }
    private HelpService helpService;

    public string username { get; set; }

    public RelayCommand LogOutCommand { get; set; }

    public MyProfileViewModel(User user,ICloseable window)
    {
        guestReviewService = new GuestReviewService();
        reviews = guestReviewService.GetReviewsByUserId(user.id);
        superGuestTitleService = new SuperGuestTitleService();
        bonusPoints = GetBonusPoints(user.id);
        username = user.username;

        helpService = new HelpService();
        ttEnabled = helpService.GetByUserId(user.id).ttEnabled;

        LogOutCommand = new RelayCommand(() => { LogOut(window); });
    }

    public List<RateReservation> reviews { get; set; }
    public int bonusPoints { get; set; }

    private int GetBonusPoints(int userId)
    {
        var superGuestTitle = superGuestTitleService.GetByUserId(userId);
        if (superGuestTitle == null) return 0;

        return superGuestTitle.availablePoints;
    }

    private void LogOut(ICloseable window)
    {
        var loginscreen = new LoginWin();
        loginscreen.Show();
        window.Close();

    }

}