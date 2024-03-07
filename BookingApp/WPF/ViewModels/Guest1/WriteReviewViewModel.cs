using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using BookingApp.Application.UseCases;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingApp.WPF.ViewModels.Guest1;

public class WriteReviewViewModel : ObservableObject, IDataErrorInfo
{
    private readonly ImageService imageService;
    private readonly RenovationProposalService renovationProposalService;
    private readonly ReviewService reviewService;
    private int cleanlinessScore;
    private int ownerScore;

    public bool ttEnabled { get; set; }
    private HelpService helpService;

    public WriteReviewViewModel(AccommodationReservation reservation)
    {
        this.reservation = reservation;
        images = new ObservableCollection<Image>();
        imageService = new ImageService();
        reviewService = new ReviewService();
        renovationProposalService = new RenovationProposalService();

        AddImageUrlCommand = new RelayCommand(() => { AddImageUrl(); });
        DeleteImageUrlCommand = new RelayCommand(() => { DeleteImageUrl(); });
        SaveReviewCommand = new RelayCommand<ICloseable>(o => { SaveReview(o); });
        CloseWindowCommand = new RelayCommand<ICloseable>(o => { CloseWindow(o); });

        isProposalChecked = false;
        urgencyLevelList = renovationProposalService.GetUrgencyLevelsList();

        helpService = new HelpService();
        ttEnabled = helpService.GetByUserId(reservation.idGuest).ttEnabled;

    }

    public bool isProposalChecked { get; set; }
    public List<string> urgencyLevelList { get; set; }

    public RelayCommand AddImageUrlCommand { get; set; }
    public RelayCommand DeleteImageUrlCommand { get; set; }
    public RelayCommand<ICloseable> SaveReviewCommand { get; set; }
    public RelayCommand<ICloseable> CloseWindowCommand { get; set; }

    public AccommodationReservation reservation { get; set; }

    public string comment { get; set; }
    public ObservableCollection<Image> images { get; set; }
    public string imageUrl { get; set; }

    public int renovationUrgency { get; set; }
    public string renovationComment { get; set; }

    public string RenovationUrgency
    {
        get => renovationUrgency.ToString();

        set => renovationUrgency = int.Parse(value.Split("-")[0]);
    }


    public void AddImageUrl()
    {
        if (string.IsNullOrEmpty(imageUrl)) return;

        var image = new Image(imageUrl, -1, IMG_USER_TYPE.AccommodationReview);
        images.Add(image);
        OnPropertyChanged(nameof(images));
    }

    public void DeleteImageUrl()
    {
        if (images.Count == 0) return;

        images.Remove(images[^1]);
        OnPropertyChanged(nameof(images));
    }


    public void SaveReview(ICloseable window)
    {
        if (!AreInputsValid())
        {
            MessageBox.Show("Please fill the fields with correct values!");
            return;
        }

        var review = new AccommodationReview(reservation.idGuest, reservation.accomodation.id, cleanlinessScore,
            ownerScore, comment);
        var id = reviewService.Save(review).id;

        SaveImages(id);

        SaveRenovationProposal();

        window.Close();
    }

    public void SaveRenovationProposal()
    {
        if (!isProposalChecked) return;

        var proposal = new RenovationProposal(reservation, renovationUrgency, renovationComment);
        renovationProposalService.Save(proposal);
    }

    public void CloseWindow(ICloseable window)
    {
        window.Close();
    }


    private void SaveImages(int id)
    {
        foreach (var image in images)
        {
            image.resourceId = id;
            imageService.Save(image);
        }
    }

    private bool AreInputsValid()
    {
        var isScoreValid = cleanlinessScore != 0 && ownerScore != 0;
        var isCommentValid = !string.IsNullOrEmpty(comment);
        var isUrlValid = images.Count > 0;
        var isRenovationUrgencyValid = (renovationUrgency >= 1 && renovationUrgency <= 5) || !isProposalChecked;
        var isRenovationCommentValid = !string.IsNullOrEmpty(renovationComment) || !isProposalChecked;

        return isScoreValid && isCommentValid && isUrlValid && isRenovationCommentValid && isRenovationUrgencyValid;
    }

    public void AddCleanlinessScore(string buttonName)
    {
        cleanlinessScore = AddReviewScore(buttonName);
    }

    public void AddOwnerScore(string buttonName)
    {
        ownerScore = AddReviewScore(buttonName);
    }

    private int AddReviewScore(string buttonName)
    {
        var score = buttonName.Split("_")[1];
        return int.Parse(score);
    }

    public string Error => null;


    public string this[string columnName]
    {
        get
        {
            string msg = null;
            switch (columnName)
            {
                case "images":
                    if (images.Count == 0)
                        msg = "Must add atleast one image!";
                    break;
                case "comment":
                    if (string.IsNullOrWhiteSpace(comment))
                        msg = "Comment cannot be empty or white space!";
                    break;
                case "RenovationUrgency":
                    if (renovationUrgency == 0 && isProposalChecked)
                        msg = "You must chose a renovation urgency level!";
                    break;
                case "renovationComment":
                    if (string.IsNullOrWhiteSpace(renovationComment) && isProposalChecked)
                        msg = "You must leave a renovation proposal comment!";
                    break;

            }
            return msg;
        }
    }
}