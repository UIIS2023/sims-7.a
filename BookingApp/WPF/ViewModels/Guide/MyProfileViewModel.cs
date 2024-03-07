using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guide
{
    internal class MyProfileViewModel
    {
        public int guideId { get; set; }
        public User guide { get; set; }
        private InitialProject.Repository.UserRepository _userRepository;
        public MyProfileViewModel(int guideId) 
        {
            _userRepository = new InitialProject.Repository.UserRepository();
            this.guideId = guideId;
            guide = new User();
            FindUser();
        }

        public void FindUser()
        {
            foreach(User user in _userRepository.GetAll())
            {
                if(user.id == guideId)
                {
                    guide = user;
                }
            }
        }
    }
}
