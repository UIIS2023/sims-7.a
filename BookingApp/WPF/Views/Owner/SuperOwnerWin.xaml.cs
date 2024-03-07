using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
namespace BookingApp.WPF.Views.Owner
{
  
    public partial class SuperOwnerWin : Window, INotifyPropertyChanged
    {
        public SuperOwnerWin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
