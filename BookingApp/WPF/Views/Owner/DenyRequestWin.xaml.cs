using BookingApp.Repository.Owner;
using System.ComponentModel;
using System.Windows;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.WPF.Views.Owner
{
   
    public partial class DenyRequestWin : Window, INotifyPropertyChanged
    {
        private string _reason;
        public string Reason
        {
            get => _reason;
            set
            {
                if (_reason != value)
                {
                    _reason = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly DenyRequestRepository denyRequestRepository;
        public MoveReservationRequest selectedRequest { get; set; }
        public DenyRequestWin(MoveReservationRequest selected)
        {
            InitializeComponent();
            DataContext = this;
            denyRequestRepository = new DenyRequestRepository();
            selectedRequest = new MoveReservationRequest();
            selectedRequest = selected;
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //sacuvava denial-reason uz request tako sto 
            // ownerComment.add(prosledim taj komentar)
            //MoveReservationRequest.status se menja u Denied

            DenyRequest request = new DenyRequest(selectedRequest, Reason);
            DenyRequest request1 = denyRequestRepository.Save(request);
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;    //za INotyfyPropertyChanged
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
