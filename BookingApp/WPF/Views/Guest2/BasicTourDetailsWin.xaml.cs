using BookingApp.Domain.Models.Guest2;
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
using System.Xml.Linq;

namespace BookingApp.WPF.Views.Guest2
{
    /// <summary>
    /// Interaction logic for BasicTourDetailsWin.xaml
    /// </summary>
    public partial class BasicTourDetailsWin : Window
    {
        private int id;
        private string city;
        private string state;
        private string language;
        private int numberOfGuests;
        private DateTime? start;
        private DateTime? end;
        private string description;
        private Status status;
        private DateTime? appointment;
        public BasicTourDetailsWin(int id, string city, string state, string language, int numberOfGuests, DateTime? start, DateTime? end, string description, Status status, DateTime? appointment)
        {
            InitializeComponent();
            this.id = id;
            this.city = city;
            this.state = state;
            this.language = language;
            this.numberOfGuests = numberOfGuests;
            this.start = start;
            this.end = end;
            this.description = description;
            this.status = status;
            this.appointment = appointment;

            BoxId.Text = id.ToString();
            BoxId.IsReadOnly = true;
            BoxLocationCity.Text = city;
            BoxLocationCity.IsReadOnly = true;
            BoxLocationState.Text = state;
            BoxLocationState.IsReadOnly = true;
            BoxLanguage.Text = language;
            BoxLanguage.IsReadOnly = true;
            BoxNumOfGuests.Text = numberOfGuests.ToString();
            BoxNumOfGuests.IsReadOnly = true;
            BoxDescription.Text = description;
            BoxDescription.IsReadOnly = true;
            BoxStatus.Text = status.ToString();
            BoxStatus.IsReadOnly = true;
            BoxFrom.Text = start.ToString();
            BoxFrom.IsReadOnly = true;
            BoxTo.Text = end.ToString();  
            BoxTo.IsReadOnly = true;
            BoxAppointment.Text = appointment.ToString();
            BoxAppointment.IsReadOnly = true;

        }
    }
}
