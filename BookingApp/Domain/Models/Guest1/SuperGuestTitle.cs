using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models.Guest1
{
    public class SuperGuestTitle:ISerializable
    {

        public int id { get; set; }
        public DateTime activationDate { get; set; }
        public int availablePoints { get; set; }

        public SuperGuestTitle()
        {
            id = -1;
        }

        public SuperGuestTitle(DateTime activationDate,int availablePoints)
        {
            this.activationDate = activationDate;
            this.availablePoints = availablePoints;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                activationDate.ToString("dd'/'MM'/'yyyy"),
                availablePoints.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            activationDate = DateTime.ParseExact(values[1], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            availablePoints = int.Parse(values[2]);

        }

    }
}
