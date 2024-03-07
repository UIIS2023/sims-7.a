using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.WPF.ViewModels.Guest1;

namespace BookingApp.WPF.Views.Guest1.MainWinPages
{
    /// <summary>
    /// Interaction logic for MyProfilePage.xaml
    /// </summary>
    public partial class MyProfilePage : Page
    {
        public MyProfilePage(User user,ICloseable window)
        {
            InitializeComponent();
            this.DataContext = new MyProfileViewModel(user,window);
        }
    }
}
