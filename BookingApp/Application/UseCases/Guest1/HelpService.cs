using BookingApp.Domain.Models.Guest1;
using BookingApp.Repository.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases.Guest1
{
    public class HelpService
    {
        private HelpSettingsRepository repository;

        public HelpService()
        {
            repository = new HelpSettingsRepository();
        }

        public HelpSettings Save(HelpSettings setting)
        {
            return repository.Save(setting);
        }

        public HelpSettings Update(HelpSettings setting)
        {
            return repository.Update(setting);
        }

        public HelpSettings GetByUserId(int userId)
        {
            return repository.GetSettingByUserId(userId);
        }

        public bool SettingExitForUser(int userId)
        {
            return repository.UserSettingsExist(userId);
        }

    }
}
