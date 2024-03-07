using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.Application.UseCases.Guest1;

public class ValidDatesService
{
    private readonly ReservationService reservationService;

    public ValidDatesService()
    {
        reservationService = new ReservationService();
    }

    public void BlackOutForbiddenEndDates(DatePicker datePicker, DateTime startDate,
        AccommodationReservation reservation)
    {
        CalendarDateRange dateRange;
        BlackOutMinStayDays(datePicker, startDate, reservation);

        BlackoutFromSelectedToNow(datePicker, startDate);

        BlackoutAheadReservationDates(datePicker, startDate, reservation);
    }

    private void BlackoutAheadReservationDates(DatePicker datePicker, DateTime startDate,
        AccommodationReservation reservation)
    {
        var forbiddenDates = GetForbiddenDateSpans(reservation.accomodation.id);
        var aheadReservationDates = forbiddenDates.Where(x => startDate < x.Start);

        if (aheadReservationDates.Count() > 0)
        {
            var lastDate = aheadReservationDates.Min(x => x.Start);
            var dateRange = new CalendarDateRange(lastDate, DateTime.MaxValue);
            datePicker.BlackoutDates.Add(dateRange);
        }
    }

    private static void BlackoutFromSelectedToNow(DatePicker datePicker, DateTime startDate)
    {
        var dateRange = new CalendarDateRange(DateTime.Now, startDate);
        datePicker.BlackoutDates.Add(dateRange);
    }

    private static void BlackOutMinStayDays(DatePicker datePicker, DateTime startDate, AccommodationReservation reservation)
    {
        var dateRange =
            new CalendarDateRange(startDate, startDate.AddDays(reservation.accomodation.minDaysForStay - 1));
        datePicker.BlackoutDates.Add(dateRange);
    }

    public void BlackOutDates(DatePicker datePicker, int accommodationId)
    {
        var forbiddenDates = GetForbiddenDateSpans(accommodationId);

        foreach (var forbiddenDateSpan in forbiddenDates) datePicker.BlackoutDates.Add(forbiddenDateSpan);
        datePicker.BlackoutDates.AddDatesInPast();
    }

    private List<CalendarDateRange> GetForbiddenDateSpans(int id)
    {
        var forbiddenDates = new List<CalendarDateRange>();

        foreach (var reservation in reservationService.GetByAccommodationId(id))
        {
            var dateSpan = new CalendarDateRange
            {
                Start = reservation.startDate,
                End = reservation.endDate
            };
            forbiddenDates.Add(dateSpan);
        }

        return forbiddenDates;
    }
}