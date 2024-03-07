using BookingApp.Domain.Models.Guest2;
using BookingApp.Functionality;
using BookingApp.Model;
using BookingApp.Repository.Guest2;
using BookingApp.View;
using BookingApp.View.Guide;
using BookingApp.WPF.ViewModels.Guest2;
using BookingApp.WPF.Views.Guest2;
using InitialProject.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.Primitives;
using Image = BookingApp.Model.Image;
using Paragraph = iTextSharp.text.Paragraph;

namespace BookingApp
{
    /// <summary>
    /// Interaction logic for CustomerToursWin.xaml
    /// </summary>
    public partial class CustomerToursWin : Window, INotifyPropertyChanged
    {
        public int idUser;
        TourFunctionality tourFunctionality;
        TourFeedbackWin tourFeedbackWin;
        TourFeedbackRepository tourFeedbackRepository;

        private ObservableCollection<CreateBasicTour> _tours = new ObservableCollection<CreateBasicTour>();
        public ObservableCollection<CreateBasicTour> Tours
        {
            get { return _tours; }
            set
            {
                if (_tours != value)
                {
                    _tours = value;
                    OnPropertyChanged(nameof(Tours));
                }
            }
        }
        public CustomerToursWin(int idUser)
        {
            InitializeComponent();

            tourFunctionality = new TourFunctionality(idUser);
            tourFeedbackWin = new TourFeedbackWin(idUser);
            tourFeedbackRepository = new TourFeedbackRepository(idUser);

            this.DataContext = tourFunctionality;
            this.idUser = idUser;

        }
        private void Tour_Booking(object sender, RoutedEventArgs e)
        {
            
            bool usedVoucher = false;

            Random random = new Random();
            int averageAge = random.Next(18, 78);

            if (dataGrid.SelectedItem != null)
            {
                usedVoucher = true;
                MessageBox.Show("Along with the selected tour, we inform you that you have also selected a voucher!");
               
            }
            else
            {
                usedVoucher = false;
            }


            if (listView.SelectedItem != null)
            {

                TourAppointment selectedTour = (TourAppointment)listView.SelectedItem;

                int idAppointment = selectedTour.id;
                string name = selectedTour.tour.name;
                string locationCity = selectedTour.tour.Location.city;
                string locationState = selectedTour.tour.Location.country;
                DateTime startTime = selectedTour.startTime;
                string description = selectedTour.tour.description;
                string language = selectedTour.tour.language;
                int maxGuests = selectedTour.tour.maxGuests;
                double duration = selectedTour.tour.duration;
                string imageUrl = selectedTour.tour.image.url;

                CustomerToursMakeReservation makeReservation = new CustomerToursMakeReservation(idUser, idAppointment, name, locationCity, locationState, startTime, description, language, maxGuests, duration, selectedTour, usedVoucher, averageAge, imageUrl);
                makeReservation.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Please select the tour from the ListView.");
            }
        }

        private void Home(object sender, RoutedEventArgs e)
        {

            CustomerToursWin customerToursWin = new CustomerToursWin(idUser);
            customerToursWin.Show();
            GetWindow(this).Close();
        }

        private void Live_Tour(object sender, RoutedEventArgs e)
        {
            
            WPF.Views.Guest2.LiveTourWin liveTourWin = new WPF.Views.Guest2.LiveTourWin(idUser);
            liveTourWin.Show();
            GetWindow(this).Close();
        }

        private void Basic_Tour(object sender, RoutedEventArgs e)
        {
            CreateBasicTourWin createBasicTourWin = new CreateBasicTourWin();         
            createBasicTourWin.Show();
            GetWindow(this).Close();
            
        }

        private void Complex_Tour(object sender, RoutedEventArgs e)
        {
            CreateComplexTourWin createComplexTourWin = new CreateComplexTourWin();
            createComplexTourWin.Show();
            GetWindow(this).Close();

        }

        private void Export_PDF(object sender, RoutedEventArgs e)
        {
           
            Document document = new Document();

            string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\Report.pdf";

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                document.Open();

                Paragraph title = new Paragraph("Izvestaj o svim trenutno vazecim vaucerima", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.BLACK));
                title.Alignment = Element.ALIGN_CENTER;

                document.Add(title);

                Paragraph description = new Paragraph("Ovaj izvestaj prikazuje informacije o svim trenutno vazecim vaucerima." +
                    "Vauceri su dodeljivani gostu iz sledecih razloga:\n" +
                    "1. Vodic je otkazao turu\n" +
                    "2. Vodic je dao otkaz\n" +
                    "3.Vodic je osvojio vaucer", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.DARK_GRAY));
                description.SpacingBefore = 30f;
                description.SpacingAfter = 30f;

                document.Add(description);
                
                PdfPTable table = new PdfPTable(4);

                PdfPCell headerCell = new PdfPCell();
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.Padding = 10;

                table.AddCell(new PdfPCell(new Phrase("ID Vaucera", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK))) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("ID Gosta", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK))) { BackgroundColor = BaseColor.LIGHT_GRAY,  HorizontalAlignment = Element.ALIGN_CENTER  });
                table.AddCell(new PdfPCell(new Phrase("Datum isteka vaucera", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK))) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Iskoriscen", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK))) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });

                string filePathVoucher = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\vouchers.csv";
                string[] lines = File.ReadAllLines(filePathVoucher);

                foreach (string line in lines)
                {
                    string[] values = line.Split('|');
                    table.AddCell(new PdfPCell(new Phrase(values[0], new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)))
                    {
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase(values[1], new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)))
                    {
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase(values[2], new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)))
                    {
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase(values[3], new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)))
                    {
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                }

                document.Add(table);
                document.Close();

                MessageBox.Show("Izveštaj je uspešno generisan i sačuvan u PDF formatu.", "Export PDF", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom generisanja izveštaja. Error: " + ex.Message, "Export PDF", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }    
    }
}
