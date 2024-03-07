using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Application.UseCases.Owner
{
    public class RenovationService
    {
        private readonly IRenovationOwnRepository renovationRepository;
        private readonly ReservationService reservationService;

        public RenovationService()
        {
            renovationRepository = Injector.Injector.CreateInstance<IRenovationOwnRepository>();
            reservationService = new ReservationService();
        }


        public List<Renovation> GetAll()
        {
            return renovationRepository.GetAll();
        }
        public Renovation SaveAccommodationRenovation(Renovation renovation)
        {
            return renovationRepository.Save(renovation);
        }

       

        public void DeleteAccommodationRenovation(Renovation renovation)
        {
            renovationRepository.Delete(renovation);
        }


        public List<Tuple<DateTime, DateTime>> GetAvailablePeriods(Accommodation selectedAccommodation, int daysForRenovation, DateTime startDate, DateTime endDate)
        {
            List<DateTime> reservedDates = GetReservedDates(selectedAccommodation);
            List<Tuple<DateTime, DateTime>> availableDatesCombo = new List<Tuple<DateTime, DateTime>>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!reservedDates.Contains(date))
                {
                    DateTime renovationStartDate = date;
                    DateTime renovationEndDate = date.AddDays(daysForRenovation - 1);

                    if (renovationEndDate <= endDate && !reservedDates.GetRange(reservedDates.IndexOf(date), daysForRenovation).Any(rd => rd != date))
                    {
                        availableDatesCombo.Add(Tuple.Create(renovationStartDate, renovationEndDate));
                    }
                }
            }

            return availableDatesCombo;
        }

        public List<DateTime> GetReservedDates(Accommodation selectedAccommodation)  ////r
        {
            List<DateTime> reservedDates = new List<DateTime>();

            foreach (AccommodationReservation reservation in reservationService.GetAll())
            {
                if (selectedAccommodation.id == reservation.accomodation.id)
                {
                    DateTime start = reservation.startDate;
                    DateTime end = reservation.endDate;

                    for (DateTime currentDate = start; currentDate <= end; currentDate = currentDate.AddDays(1))
                    {
                        reservedDates.Add(currentDate);
                    }
                }
            }
            return reservedDates;
        }
    }
}
