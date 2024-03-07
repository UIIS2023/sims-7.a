using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.WPF.ViewModels.Guest1;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for WriteReviewWin.xaml
    /// </summary>
    public partial class WriteReviewWin : Window,ICloseable
    {
        private WriteReviewViewModel writeReviewViewModel;

        public WriteReviewWin(AccommodationReservation reservation)
        {
            InitializeComponent();
            writeReviewViewModel = new WriteReviewViewModel(reservation);
            this.DataContext = writeReviewViewModel;
            
        }

        

       

        private void Star_OnClick(object sender, RoutedEventArgs e)
        {
            
            ResetStars();
            (sender as CheckBox).IsChecked = true;
            writeReviewViewModel.AddCleanlinessScore((sender as CheckBox).Name);


        }

        private void Star1_OnClick(object sender, RoutedEventArgs e)
        {

            ResetStars1();
            (sender as CheckBox).IsChecked = true;
            writeReviewViewModel.AddOwnerScore((sender as CheckBox).Name);

        }


        private void ResetStars()
        {
            Star_5.IsChecked = false;
            Star_4.IsChecked = false;
            Star_3.IsChecked = false;
            Star_2.IsChecked = false;
            Star_1.IsChecked = false;
        }

        private void ResetStars1()
        {
            Star_5_1.IsChecked = false;
            Star_4_1.IsChecked = false;
            Star_3_1.IsChecked = false;
            Star_2_1.IsChecked = false;
            Star_1_1.IsChecked = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
