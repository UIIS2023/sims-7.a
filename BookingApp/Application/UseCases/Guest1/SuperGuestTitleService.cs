using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Application.UseCases.Guest1
{
    public class SuperGuestTitleService
    {
        private readonly ISuperGuestRepository superGuestRepository;
        private readonly IUserRepository userRepository;
        private readonly ReservationService reservationService;


        public SuperGuestTitleService()
        {
            superGuestRepository = Injector.Injector.CreateInstance<ISuperGuestRepository>();
            userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            reservationService = new ReservationService();
        }

        public SuperGuestTitle Save(SuperGuestTitle guestTitle)
        {
            return superGuestRepository.Save(guestTitle);
        }

        public SuperGuestTitle GetById(int id)
        {
            return superGuestRepository.GetById(id);
        }

        public SuperGuestTitle GetByUserId(int id)
        {
            var superGuestTitle = 
            superGuestRepository.GetById(userRepository.GetById(id).superGuestTitle.id);

            if (superGuestTitle == null) return new SuperGuestTitle();
            return superGuestTitle;

        }

        public void UseBonusPoint(int id)
        {
            SuperGuestTitle title = GetByUserId(id);

            if (title.availablePoints <= 0) return;

            title.availablePoints--;
            superGuestRepository.Update(title);
        }

        public void AwardTitle(User user,SuperGuestTitle title)
        {
            if (!IsTitleConditionMet(user.id,title)) return;

            title.activationDate = DateTime.Now;
            title.availablePoints = 5;


            if (title.id == -1)
            {
                int titleId = Save(title).id;
                title.id = titleId;
            }
            else
            {
                superGuestRepository.Update(title);
            }

            user.superGuestTitle = title;
            userRepository.Update(user);

        }

        public bool IsTitleConditionMet(int userId,SuperGuestTitle title)
        {

            return reservationService.GetByUserId
            (
                userId 
            ).Count(x => x.endDate.Date < DateTime.Now.Date && // is past reservation
                         DateTime.Now.Subtract(x.endDate).Days <= 365) >= 10 // is within a year
                         && DateTime.Now.Subtract(title.activationDate).Days > 365
                         ;

        }


    }
}
