using BookingApp.WPF.Views.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.Guide
{
    class GuideMainMenuViewModel : BaseViewModel
    {
        public int guideId { get; set; }
        public ICommand MyToursClick { get; private set; }
        public ICommand MyProfileClick { get; private set; }
        public ICommand TourRequestClick { get; private set; }
        
        public GuideMainMenuViewModel(int guideId)
        {
            this.guideId = guideId;
            MyToursClick = new RelayCommand(MyTours_Click);
            MyProfileClick = new RelayCommand(MyProfile_Click);
            TourRequestClick = new RelayCommand(TourRequest_Click);
        }

        private void TourRequest_Click()
        {
            TourRequestWin tourRequestWin = new TourRequestWin(guideId);
            tourRequestWin.ShowDialog();
        }

        private void MyProfile_Click()
        {
            MyProfileWin myProfileWin = new MyProfileWin(guideId);
            myProfileWin.ShowDialog();
        }

        private void MyTours_Click()
        {
            MyToursWin myToursWin = new MyToursWin(guideId);
            myToursWin.ShowDialog();
            
        }
    }
}
