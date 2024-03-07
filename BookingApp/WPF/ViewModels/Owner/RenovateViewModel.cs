using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Owner;
using BookingApp.WPF.ViewModels.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Owner
{
    public class RenovateViewModel : BaseViewModel
    {

        //private int _renovationDurationInput;
        //public int RenovationDurationInput
        //{
        //    get => _renovationDurationInput;
        //    set
        //    {
        //        if (value != _renovationDurationInput)
        //        {
        //            _renovationDurationInput = value;
        //            OnPropertyChanged(nameof(RenovationDurationInput));
        //        }
        //    }
        //}

        private DateTime _fromInput;
        public DateTime StartInput
        {
            get => _fromInput;
            set
            {
                if (value != _fromInput)
                {
                    _fromInput = value;
                    OnPropertyChanged(nameof(StartInput));
                }
            }
        }

        private DateTime _endInput;
        public DateTime EndInput
        {
            get => _endInput;
            set
            {
                if (value != _endInput)
                {
                    _endInput = value;
                    OnPropertyChanged(nameof(EndInput));
                }
            }
        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        private readonly RenovationService renovationService;
        public Accommodation SelectedAccommodation { get; set; }

        //DUGMAD//
        public ICommand SaveRenovationClick { get; private set; }
        public ICommand CheckAvailableDates { get; private set; }
        public string RenovationDurationInput { get; set; }


       


        public Renovation selectedAvaliableDates { get;  set; }
        public ObservableCollection<Tuple<DateTime, DateTime>> avaliableDates { get; set; }
        public string comment { get; set; }
        

        public RenovateViewModel(Accommodation selectedAccommodation)
        {
            SelectedAccommodation = selectedAccommodation;

            renovationService = new RenovationService();

           

            avaliableDates = new ObservableCollection<Tuple<DateTime, DateTime>>();

            SaveRenovationClick = new RelayCommand(Save_Renovation_Click);
            CheckAvailableDates = new RelayCommand(Check_Available_Dates);
        }

        //DUGMAD//
        private void Check_Available_Dates()
        {
            int duration = int.Parse(RenovationDurationInput);
            DateTime startDate = StartInput;
            DateTime endDate = EndInput;

            if (startDate > endDate)
            {
                if (avaliableDates != null)
                {
                    avaliableDates.Clear();
                }
                return;
            }
            avaliableDates.Clear();

            List<Tuple<DateTime, DateTime>> availablePeriods = new List<Tuple<DateTime, DateTime>>();

            availablePeriods = renovationService.GetAvailablePeriods(SelectedAccommodation, duration, startDate, endDate);

            foreach (var period in availablePeriods)
            {
                avaliableDates.Add(period);
            }
        }

        private void Save_Renovation_Click()
        {
            //if(selectedAvaliableDates != null)
            //{
            //    Renovation  renovation = new Renovation(selectedAvaliableDates);
            //}
           
        }

    }
}

