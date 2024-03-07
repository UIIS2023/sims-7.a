using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using BookingApp.Domain.Models.Owner;

namespace BookingApp.Repository.Owner
{
    public class RateReservationGuestRepository : IRateReservationGuestRepository
    {
        public const string filePath = "../../../Resources/Data/ratesReservations.csv";

        private readonly Serializer<RateReservation> serializer;

        private List<RateReservation> rates;

        
        public RateReservationGuestRepository()
        {
            serializer = new Serializer<RateReservation>();
            rates = serializer.FromCSV(filePath);
        }

        private int NextId()
        {
            rates = serializer.FromCSV(filePath);
            if (rates.Count < 1)
            {
                return 1;
            }
            return rates.Max(c => c.id) + 1;
        }

        public RateReservation Save(RateReservation rate)
        {
            rate.id = NextId();
            rates = serializer.FromCSV(filePath);
            rates.Add(rate);
            serializer.ToCSV(filePath, rates);

            return rate;
        }

        
        public List<RateReservation> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public List<RateReservation> GetById(int id)
        {
            rates = serializer.FromCSV(filePath);

            List<RateReservation> filteredRates = new List<RateReservation>();
            filteredRates = rates.Where(x => x.accommodationReservation.isRated == false).ToList();

            return filteredRates;
        }

        //public void Delete(RateReservatiounGuest rate)
        //{
        //    rates = serializer.FromCSV(filePath);
        //    RateReservatiounGuest founded = rates.Find(t => t.id == rate.id);
        //    rates.Remove(rate);
        //    serializer.ToCSV(filePath, rates);
        //}

        //public RateReservatiounGuest Update(RateReservatiounGuest rate)
        //{
        //    rates = serializer.FromCSV(filePath);
        //    RateReservatiounGuest current = rates.Find(r => r.id == rate.id);
        //    int index = rates.IndexOf(current);
        //    rates.Remove(current);
        //    rates.Insert(index, rate);
        //    serializer.ToCSV(filePath, rates);
        //    return rate;
        //}

    }
}
