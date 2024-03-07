using System.Collections.Generic;
using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Application.UseCases.Guest1;

public class ReservationService
{
    private readonly AccommodationService accommodationService;

    private readonly IReservationRepository reservationRepository;


    public ReservationService()
    {
        reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
        accommodationService = new AccommodationService();
    }

    public List<AccommodationReservation> GetAll()
    {
        var reservations = reservationRepository.GetAll();

        UpdateAccommodationsInReservations(reservations);


        return reservations;
    }

    public List<AccommodationReservation> GetByUserId(int id)
    {
        var reservations = reservationRepository.GetByUserId(id);

        UpdateAccommodationsInReservations(reservations);

        return reservations;
    }

    public List<AccommodationReservation> GetByAccommodationId(int id)
    {
        var reservations = reservationRepository.GetByAccommodationId(id);

        UpdateAccommodationsInReservations(reservations);


        return reservations;
    }

    public void Save(AccommodationReservation reservation)
    {
        reservationRepository.Save(reservation);
    }

    public void Remove(AccommodationReservation reservation)
    {
        reservationRepository.Remove(reservation);
    }


    private void UpdateAccommodationsInReservations(List<AccommodationReservation> reservations)
    {
        var accomodations = accommodationService.GetAll();

        foreach (var reservation in reservations)
        foreach (var accomodation in accomodations)
            if (reservation.accomodation.id == accomodation.id)
                reservation.accomodation = accomodation;
    }
}