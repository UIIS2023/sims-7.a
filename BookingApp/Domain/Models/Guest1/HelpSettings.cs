using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models.Guest1
{
    public class HelpSettings : ISerializable
    {
        public int id { get; set; }
        public User user { get; set; }
        public bool ttEnabled { get; set; }
        public bool showWizard { get; set; }

        public HelpSettings()
        {
            user = new User();
            ttEnabled = true;
            showWizard = true;
        }

        public HelpSettings(User user,bool ttEnabled,bool showWizard)
        {
            this.user = user;
            this.ttEnabled = ttEnabled;
            this.showWizard = showWizard;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            id.ToString(),
            user.id.ToString(),
            ttEnabled.ToString(),
            showWizard.ToString()
            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            user.id = int.Parse(values[1]);
            ttEnabled = bool.Parse(values[2]);
            showWizard = bool.Parse(values[3]);
        }

    }
}
