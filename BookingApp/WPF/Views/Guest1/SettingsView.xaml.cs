using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        private HelpService helpService;
        public bool ttEnabled { get; set; }
        private HelpSettings settings;

        public SettingsView(int userId)
        {
            InitializeComponent();
            this.DataContext = this;
            helpService = new HelpService();
            settings = helpService.GetByUserId(userId);
            ttEnabled = settings.ttEnabled;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            settings.ttEnabled = ttEnabled;
            helpService.Update(settings);
            this.Close();
        }
    }
}
