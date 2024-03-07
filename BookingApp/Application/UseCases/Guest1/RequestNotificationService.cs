using System.Text;
using System.Windows;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Application.UseCases.Guest1;

public class RequestNotificationService
{
    private readonly ReservationRequestService reservationRequestService;

    public RequestNotificationService()
    {
        reservationRequestService = new ReservationRequestService();
    }

    public void SendNotifications(int id, Window window)
    {
        foreach (var request in reservationRequestService.GetByUserId(id))
        {
            if (request.isUserNotified) continue;


            var title = GetTitle(request);

            var message = GetMessage(request);

            MessageBox.Show(message.ToString(), title.ToString(), MessageBoxButton.OK,
                request.status == REQUEST_STATUS.Accepted ? MessageBoxImage.Information : MessageBoxImage.Error);
        }
    }

    private static StringBuilder GetMessage(MoveReservationRequest request)
    {
        var message = new StringBuilder("");
        if (request.status == REQUEST_STATUS.Accepted)
        {
            message.Append("Reservation date has been moved from: ");
            message.Append(request.oldReservation.startDate.ToString("dd'/'MM'/'yyyy"));
            message.Append("-");
            message.Append(request.oldReservation.endDate.ToString("dd'/'MM'/'yyyy"));
            message.Append("\n to:\n");
            message.Append(request.newStartDate.ToString("dd'/'MM'/'yyyy"));
            message.Append("-");
            message.Append(request.newStartDate.ToString("dd'/'MM'/'yyyy"));
        }
        else
        {
            message.Append("Move reservation request has been DENIED!");
        }

        return message;
    }

    private static StringBuilder GetTitle(MoveReservationRequest request)
    {
        var title = new StringBuilder("Reservation request (");
        title.Append(request.oldReservation.accomodation.name);
        title.Append(") has been: ");
        title.Append(request.getStatusString);
        return title;
    }
}