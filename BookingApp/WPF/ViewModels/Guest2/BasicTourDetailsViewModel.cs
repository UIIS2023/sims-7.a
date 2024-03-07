using BookingApp.Controller;
using BookingApp.WPF.ViewModels.Guide;
using BookingApp.WPF.Views.Guest2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guest2
{
    public class BasicTourDetailsViewModel
    {
        public ICommand Ok { get; set; }
        public BasicTourDetailsViewModel()
        {
            Ok = new RelayCommand(OkExecute);
        }

        private void OkExecute()
        {  
            App.Current.MainWindow.Close();
        }

    }  
}
