using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.WPF.Views.Guest2;
using BookingApp.WPF.Views.Guide;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class TourRequestViewModel
    {
        public int guideId { get; set; }
        public ICommand BasicTourRequestClick { get; private set; }
        public ICommand TourRequestStatisticsClick { get; private set; }
        public TourRequestViewModel(int guideId)
        {
            BasicTourRequestClick = new RelayCommand(BasicTourRequest_Click);
            this.guideId = guideId;
            TourRequestStatisticsClick = new RelayCommand(TourRequestStatistics_Click);
        }

        private void TourRequestStatistics_Click()
        {
            TourRequestStatisticsWin tourRequestStatisticsWin = new TourRequestStatisticsWin(guideId);
            tourRequestStatisticsWin.ShowDialog();
        }

        private void BasicTourRequest_Click()
        {
            BasicTourRequestsWin basicTourRequestWin = new BasicTourRequestsWin(guideId);
            basicTourRequestWin.Show();
        }
    }
}
