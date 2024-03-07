using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class ReservationInfoViewModel:ObservableObject
    {
        public AccommodationReservation reservation { get; set; }

        private List<Image> images;
        public Image currentImage { get; set; }
        private int currentImageIndex;

        public RelayCommand<ICloseable> CloseWindowCommand { get; set; }
        public RelayCommand NextImageRightCommand { get; set; }
        public RelayCommand NextImageLeftCommand { get; set; }

        public bool ttEnabled { get; set; }
        private HelpService helpService;


        private PDFService pdfService;
        public RelayCommand SavePDFCommand { get; set; }

        public ReservationInfoViewModel(AccommodationReservation reservation)
        {
            this.reservation = reservation;
            images = reservation.accomodation.accommodationImages;
            if (images.Count > 0)
                currentImage = images.First();
            currentImageIndex = 0;

            helpService = new HelpService();
            pdfService = new PDFService();
            ttEnabled = helpService.GetByUserId(reservation.idGuest).ttEnabled;

            CloseWindowCommand = new RelayCommand<ICloseable>(window => { Close(window); });
            NextImageLeftCommand = new RelayCommand(() => { NextImageLeft(); });
            NextImageRightCommand = new RelayCommand(() => { NextImageRight(); });
            SavePDFCommand = new RelayCommand(() => { SavePDF(); });
        }

        private void SavePDF()
        {
            pdfService.saveToPDF(reservation);
        }

        private void Close(ICloseable window)
        {
            window.Close();
        }

        private void NextImageRight()
        {
            if (++currentImageIndex >= images.Count)
                currentImageIndex = 0;

            currentImage = images[currentImageIndex];

            OnPropertyChanged(nameof(currentImage));
        }

        private void NextImageLeft()
        {
            if (--currentImageIndex < 0)
                currentImageIndex = images.Count - 1;

            currentImage = images[currentImageIndex];

            OnPropertyChanged(nameof(currentImage));
        }

        private void GeneratePDF()
        {

        }



    }
}
