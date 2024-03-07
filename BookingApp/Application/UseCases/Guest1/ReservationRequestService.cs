using System.Collections.Generic;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Application.UseCases.Guest1;

public class ReservationRequestService
{
    private readonly IRequestRepository requestRepository;
    private readonly ReservationService reservationService;


    public ReservationRequestService()
    {
        requestRepository = Injector.Injector.CreateInstance<IRequestRepository>();
        reservationService = new ReservationService();
    }

    public List<MoveReservationRequest> GetByUserId(int id)
    {
        var requests = requestRepository.GetByUserId(id);

        UpdateReservationsInRequests(requests);

        return requests;
    }

    public void Save(MoveReservationRequest request)
    {
        requestRepository.Save(request);
    }

    private void UpdateReservationsInRequests(List<MoveReservationRequest> requests)
    {
        var reservations = reservationService.GetAll();

        foreach (var request in requests)
        {
            GetOldReservation(reservations, request);
            DeleteNonExistentRequest(request);
        }
    }

    private void DeleteNonExistentRequest(MoveReservationRequest request)
    {
        if (request.oldReservation.accomodation.id == 0) requestRepository.Remove(request);
    }

    private static void GetOldReservation(List<AccommodationReservation> reservations, MoveReservationRequest request)
    {
        foreach (var reservation in reservations)
            if (request.oldReservation.id == reservation.id)
                request.oldReservation = reservation;
    }
}