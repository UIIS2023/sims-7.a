using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View
{
    public partial class AddTourWin : Window
    {    
        public AddTourWin(int guideId)
        {
            InitializeComponent();
            this.DataContext = new AddTourViewModel(guideId);
        }

        public AddTourWin(int guideId, Boolean LanguageOption, string language)
        {
            InitializeComponent();
            this.DataContext = new AddTourViewModel(guideId, LanguageOption, language);
        }

        public AddTourWin(int guideId, Boolean LanguageOption, string city, string country)
        {
            InitializeComponent();
            this.DataContext = new AddTourViewModel(guideId, LanguageOption, city, country);
        }


    }
}
