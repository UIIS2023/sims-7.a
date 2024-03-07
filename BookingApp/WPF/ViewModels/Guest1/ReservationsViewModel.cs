using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.WPF.Views.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingApp.WPF.ViewModels.Guest1;

public class ReservationsViewModel : ObservableObject
{
    private readonly int userId;
    private ReservationService reservationService;
    private ReviewService reviewService;

    public bool ttEnabled { get; set; }
    private HelpService helpService;

    public ReservationsViewModel(int userId, Frame frame)
    {
        this.userId = userId;

        InitializeRepositoriesAndLists();

        helpService = new HelpService();
        ttEnabled = helpService.GetByUserId(userId).ttEnabled;

        CancelReservationCommand = new RelayCommand<AccommodationReservation>(reservation =>
        {
            selectedReservation = reservation;
            CancelReservation();
        });
        MoveReservationCommand = new RelayCommand<AccommodationReservation>(reservation =>
        {
            selectedReservation = reservation;
            MoveReservation();
        });
        WriteReviewCommand = new RelayCommand<AccommodationReservation>(reservation =>
        {
            selectedReservation = reservation;
            WriteReview();
        },reservation => !ReviewExists(reservation));
        ShowInfoCommand = new RelayCommand<AccommodationReservation>(reservation => 
        {
            selectedReservation = reservation;
            ShowInfo();
        });
    }

    public RelayCommand<AccommodationReservation> CancelReservationCommand { get; set; }
    public RelayCommand<AccommodationReservation> MoveReservationCommand { get; set; }
    public RelayCommand<AccommodationReservation> WriteReviewCommand { get; set; }
    public RelayCommand<AccommodationReservation> ShowInfoCommand { get; set; }

    public List<AccommodationReservation> reservations { get; set; }


    public AccommodationReservation selectedReservation { get; set; }


    private void InitializeRepositoriesAndLists()
    {
        reservationService = new ReservationService();
        reservations = reservationService.GetByUserId(userId);
        reviewService = new ReviewService();
    }


    private void CancelReservation()
    {
        if (selectedReservation == null) return;

        var result = MessageBox.Show("Confirm reservation cancellation", "Cancel reservation!", MessageBoxButton.YesNo);
        switch (result)
        {
            case MessageBoxResult.Yes:
                reservationService.Remove(selectedReservation);
                reservations = reservationService.GetByUserId(userId);
                OnPropertyChanged();
                MessageBox.Show("Reservation canceled!");
                break;
            case MessageBoxResult.No:
                break;
        }
    }

    private void MoveReservation()
    {
        if (selectedReservation == null) return;

        var moveReservationWin = new MoveReservationWin(selectedReservation);
        moveReservationWin.ShowDialog();
    }

    private void WriteReview()
    {
        if (selectedReservation == null) return;
        if (ReviewExists(selectedReservation)) return;

        var writeReviewWin = new WriteReviewWin(selectedReservation);

        writeReviewWin.Closed += (s, e) =>
        {
            WriteReviewCommand.NotifyCanExecuteChanged();
            OnPropertyChanged();
        };

        writeReviewWin.ShowDialog();


    }

    private void ShowInfo()
    {
        if (selectedReservation == null) return;

        var showInfoWin = new ReservationInfoView(selectedReservation);
        showInfoWin.ShowDialog();

    }

    private bool ReviewExists(AccommodationReservation reservation)
    {
        foreach (var review in reviewService.GetByUserId(userId))
            if (review.accommodationId == reservation.accomodation.id)
                return true;

        return false;
    }
}