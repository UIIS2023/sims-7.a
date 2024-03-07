using System.ComponentModel;
using System.Windows;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.Guest1.HelpWizard;
using BookingApp.WPF.Views.Guest1.MainWinPages;
using BookingApp.Domain.Models.Guest1;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for MainBookingWin.xaml
    /// </summary>
    public partial class MainBookingWin : Window, INotifyPropertyChanged,ICloseable
    {
        
        

        private AccommodationsPage accomodationPage;


        private RequestNotificationService notificationService;

        private SuperGuestTitleService superGuestTitleService;

        private HelpService helpService;

        private Wizard helpWizard;

        private int userId;
        private User user;


        public RelayCommand OpenHelpCommand { get; set; }
        public RelayCommand OpenP1Command { get; set; }
        public RelayCommand OpenP2Command { get; set; }
        public RelayCommand OpenP3Command { get; set; }
        public RelayCommand OpenP4Command { get; set; }
        public RelayCommand OpenP5Command { get; set; }

        public MainBookingWin(User user)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            this.DataContext = this;
            this.userId = user.id;
            this.user = user;

            helpService = new HelpService();

            InitializePages();

            AccommodationsButton.IsEnabled = true;
            MainFrame.Content = accomodationPage;

            notificationService.SendNotifications(userId, this);

            superGuestTitleService = new SuperGuestTitleService();
            superGuestTitleService.AwardTitle(user,superGuestTitleService.GetByUserId(user.id));

            OpenHelpCommand = new RelayCommand(() => { OpenHelp(); });
            OpenP1Command = new RelayCommand(() => { P1(); });
            OpenP2Command = new RelayCommand(() => { P2(); });
            OpenP3Command = new RelayCommand(() => { P3(); });
            OpenP4Command = new RelayCommand(() => { P4(); });
            OpenP5Command = new RelayCommand(() => { P5(); });

            if (!helpService.SettingExitForUser(user.id)) helpService.Save(new HelpSettings(user, true, true));

            helpWizard = new Wizard();
            HelpSettings settings = helpService.GetByUserId(user.id);
            if (settings.showWizard)
            {
                settings.showWizard = false;
                helpService.Update(settings);
                this.Loaded += (s, e) =>
                {
                    helpWizard.ShowDialog();
                };
            }
            
            //this.Closed += (s, e) =>
            //{
            //    App.Current.Shutdown();
            //    Process.GetCurrentProcess().Kill();
            //};

        }

        

        private void InitializePages()
        {
            accomodationPage = new AccommodationsPage(userId);
            notificationService = new RequestNotificationService();
        }
       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      
        private void AccommodationsButton_Click(object sender, RoutedEventArgs e)
        {
            P1();
        }

        private void P1()
        {
            MainFrame.Content = new AccommodationsPage(userId);
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            P2();
        }

        private void P2()
        {
            MainFrame.Content = new ReservationsPage(userId, MainFrame);
        }

        private void Requests_OnClick(object sender, RoutedEventArgs e)
        {
            P3();     
        }

        private void P3()
        {
            MainFrame.Content = new RequestsPage(userId, MainFrame);
        }

        private void MyProfile_OnClick(object sender, RoutedEventArgs e)
        {
            P4();
        }

        private void P4()
        {
            MainFrame.Content = new MyProfilePage(user,this);
        }

        private void Forum_OnClick(object sender, RoutedEventArgs e)
        {
            P5();
        }

        private void P5()
        {
            MainFrame.Content = new ForumPage(userId, MainFrame);
        }


        private void WinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void WinMaximize_Click(object sender, RoutedEventArgs e)
        {
            

            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void WindMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
            
        }

        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWin = new SettingsView(userId);

            settingsWin.Closed += (s, e) =>
            {
                MainFrame.Content = new AccommodationsPage(userId);
            };

            settingsWin.ShowDialog();
        }

        private void OpenHelp_Click(object sender, RoutedEventArgs e)
        {
            OpenHelp();
        }

        private void OpenHelp()
        {
            var helpDoc = new HelpDocument();
            helpDoc.ShowDialog();
        }
    }
}
