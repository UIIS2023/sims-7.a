using BookingApp.Domain.Models.Guest1;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Guest1
{
    public class HelpSettingsRepository
    {
        private const string filePath = "../../../Resources/Data/helpSettings.csv";

        private readonly Serializer<HelpSettings> serializer;
        private List<HelpSettings> settings;

        public HelpSettingsRepository()
        {
            serializer = new Serializer<HelpSettings>();
            settings = serializer.FromCSV(filePath); 
        }

        public HelpSettings Save(HelpSettings setting)
        {
            setting.id = NextId();
            settings = serializer.FromCSV(filePath);
            settings.Add(setting);
            serializer.ToCSV(filePath, settings);

            return setting;
        }

        public bool UserSettingsExist(int userId)
        {
            return serializer.FromCSV(filePath).Any(x => x.user.id == userId);
        }

        public HelpSettings GetSettingByUserId(int userId)
        {
            settings = serializer.FromCSV(filePath);
            return settings.Find(x => x.user.id == userId);

        }

        public HelpSettings Update(HelpSettings setting)
        {
            settings = serializer.FromCSV(filePath);
            HelpSettings current = settings.Find(x => x.id == setting.id);
            int index = settings.IndexOf(current);
            settings.Remove(current);
            settings.Insert(index, setting);
            serializer.ToCSV(filePath, settings);
            return setting;
        }

        private int NextId()
        {
            settings = serializer.FromCSV(filePath);
            if (settings.Count < 1)
                return 1;

            return settings.Max(x => x.id) + 1;
        }

    }
}
