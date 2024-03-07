using BookingApp.View;
using BookingApp.Domain.Models;
using BookingApp.Repository.Mutual;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.Views.Guest1;
using BookingApp.WPF.Views.Guide;
using BookingApp.WPF.Views.Owner;


namespace BookingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWin : Window, IDataErrorInfo
    {

        private readonly UserRepository userRepository;
        private readonly IUserRepository userRepository2;

        public string username { get; set; }


        // data for the register screen 
        public string usernameRegister { get; set; }
        public USER_TYPE userType { get; set; }



        public LoginWin()
        {
            InitializeComponent();

            this.DataContext = this;
            userRepository = new UserRepository();
            userRepository2 = Injector.Injector.CreateInstance<IUserRepository>();

        }



        private void Register_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = 1;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            User user = userRepository.GetByUsername(username);

            if (user != null)
            {
                if (user.password == Password.Password)
                {
                    switch (user.userType)
                    {
                        case Domain.Models.USER_TYPE.Customer_Booking:
                            MainBookingWin bookingWin = new MainBookingWin(userRepository2.GetByUsername(username));
                            bookingWin.Show();
                            this.Close();
                            break;

                        case Domain.Models.USER_TYPE.Customer_Tours:                          
                            CustomerToursWin customerToursWin = new CustomerToursWin(user.id);
                            customerToursWin.Show();
                            this.Close();
                            break;

                        case Domain.Models.USER_TYPE.Owner:
                            OwnerWin ownerWin= new OwnerWin(user);
                            ownerWin.Show();
                            this.Close();
                            break;

                        case Domain.Models.USER_TYPE.Guide:
                            GuideMainMenuWin guideWin = new GuideMainMenuWin(user.id);
                            guideWin.Show();
                            this.Close();
                            break;

                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }

            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }
        public string Error => null;

        private readonly string[] _validatedProperties = { "usernameRegister" };

        private Regex usernameRegex = new Regex("^[a-z0-9_-]{3,15}$");


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {

                    case "usernameRegister":
                        if (string.IsNullOrEmpty(usernameRegister))
                            return "Field cannot be empty!";

                        Match match = usernameRegex.Match(usernameRegister);

                        if (!match.Success)
                            return "Min 3 and Max 15 Chars";

                        if (userRepository.GetByUsername(usernameRegister) != null)
                            return "Username already exists!";


                        break;
                }

                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        private void RegisterAccount_Click(object sender, RoutedEventArgs e)
        {

            if (IsValid)
            {
                if (!String.IsNullOrEmpty(PasswordRegister.Password) && PasswordRegister.Password == PasswordRegisterConfirmation.Password)
                {

                    //User newUser = new User(usernameRegister, PasswordRegister.Password, userType);
                    //userRepository.Save(newUser);
                    MessageBox.Show("New account successfuly registered!");


                    PasswordRegister.Clear();
                    PasswordRegisterConfirmation.Clear();
                    UserTypeRegister.SelectedIndex = -1;
                    UsernameRegister.Clear();

                    TabControl.SelectedIndex = 0;

                }
                else
                {
                    MessageBox.Show("Enter valid confirmation password!");
                }
            }
        }
    }
}
