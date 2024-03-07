using BookingApp.Domain.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class TourAttendenceService
    {
        private readonly TourAttendenceRepository _tourAttendenceRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly UserRepository _userRepository;
        private readonly VoucherRepository _voucherRepository;


        public TourAttendenceService()
        {
            _tourAttendenceRepository = new TourAttendenceRepository();
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            _voucherRepository = new VoucherRepository();
        }
        public List<TourAttendence> GetAll(TourAppointment tourAppointment)
        {
            List<TourAttendence> tourAttendences = new List<TourAttendence>();
            foreach (TourAttendence tourAttendence in _tourAttendenceRepository.GetAll())
            {
                if (tourAttendence.appointmentId == tourAppointment.id)
                {
                    BindUserToAttendence(tourAttendence);
                    tourAttendences.Add(tourAttendence);
                }
            }
            return tourAttendences;
        }
        public void CreateAttendencesForTour(TourAppointment tourAppointment)
        {
            List<TourReservation> reservations = new List<TourReservation>(_tourReservationRepository.GetAll());
            foreach (TourReservation reservation in reservations)
            {
                if (reservation.appointmentId == tourAppointment.id)
                {
                    Create(reservation.guestId, tourAppointment.id, reservation.numberOfGuests, reservation.averageAge, reservation.usedVoucher);
                }
            }
        }

        private void BindUserToAttendence(TourAttendence tourAttendence)
        {
            foreach (User user in _userRepository.GetAll())
            {
                if (user.id == tourAttendence.guestId)
                {
                    tourAttendence.guest = user;
                }
            }
        }

        private TourAttendence Create(int guestId, int appointmentId, int requestedSpaces, int averageAge, bool usedVoucher)
        {
            return _tourAttendenceRepository.Save(new TourAttendence(guestId, appointmentId, requestedSpaces, averageAge, usedVoucher));
        }

        public void SendNotification(TourAttendence tourAttendence, TourPoint currentPoint) //TODO: Current Checkpoint
        {
            tourAttendence.requestSent = true;
            tourAttendence.currentPoint = currentPoint;
            _tourAttendenceRepository.Update(tourAttendence);
        }

        public void CreateVouchers(TourAppointment tourAppointment)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.appointmentId == tourAppointment.id)
                {
                    DateTime expireDate = DateTime.Now.AddYears(1);
                    _voucherRepository.Save(new Voucher(tourReservation.guestId, expireDate));
                }
            }
        }

        public TourAppointment MostAttendedTour(int guideId, List<TourAppointment> finishedAppointments)
        {
            TourAppointment mostVisited = finishedAppointments.FirstOrDefault();
            int numberOfGuests = TotalGuests(mostVisited);
            foreach (TourAppointment tourAppointment in finishedAppointments)
            {
                if (numberOfGuests < TotalGuests(tourAppointment))
                {
                    numberOfGuests = TotalGuests(tourAppointment);
                    mostVisited = tourAppointment;
                }
            }
            mostVisited.numberOfGuests = TotalGuests(mostVisited);
            return mostVisited;
        }

        private int TotalGuests(TourAppointment tourAppointment)
        {
            int numberOfGuests = 0;
            foreach (TourAttendence tourAttendence in GetAll(tourAppointment))
            {
                if (tourAttendence.attended)
                {
                    numberOfGuests += tourAttendence.reservedSpaces;
                }
            }
            return numberOfGuests;
        }

        public TourStatisticsDTO GetStatistics(TourAppointment tourAppointment)
        {
            TourStatisticsDTO statistics = new TourStatisticsDTO();
            statistics.totalGuests = TotalGuests(tourAppointment);
            foreach (TourAttendence tourAttendence in GetAll(tourAppointment))
            {
                if (tourAttendence.attended) {
                    VoucherStatistics(tourAttendence, statistics);

                    AgeStatistics(tourAttendence, statistics);
                }
            }
            CalculatePercentages(statistics);

            return statistics;
        }

        private int VoucherStatistics(TourAttendence tourAttendence, TourStatisticsDTO statistics)
        {
            if (tourAttendence.usedVoucher)
            {
                return statistics.withVoucher += tourAttendence.reservedSpaces;
            }
            return statistics.withoutVoucher += tourAttendence.reservedSpaces;
        }

        private int AgeStatistics(TourAttendence tourAttendence, TourStatisticsDTO statistics)
        {
            if (tourAttendence.averageAge < 18)
            {
                return statistics.underAgeGuests += tourAttendence.reservedSpaces;
            }
            else if (tourAttendence.averageAge >= 18 && tourAttendence.averageAge <= 50)
            {
                return statistics.adultGuests += tourAttendence.reservedSpaces;
            }
            else
            {
                return statistics.elderlyGuests += tourAttendence.reservedSpaces;
            }
        }

        private void CalculatePercentages(TourStatisticsDTO statistics)
        {
            if (statistics.totalGuests != 0)
            {
                statistics.voucherPercentage = Math.Round(((double)statistics.withVoucher / (double)statistics.totalGuests) * 100.0, 2);
                statistics.withoutVoucherPercentage = Math.Round((double)100.0 - statistics.voucherPercentage, 2);
                statistics.underAgePercentage = Math.Round((double)statistics.underAgeGuests / statistics.totalGuests * 100.0, 2);
                statistics.adultPercentage = Math.Round((double)statistics.adultGuests / statistics.totalGuests * 100.0, 2);
                statistics.elderlyPercentage = Math.Round((double)statistics.elderlyGuests / statistics.totalGuests * 100.0, 2);
            }
        }


    }
}
