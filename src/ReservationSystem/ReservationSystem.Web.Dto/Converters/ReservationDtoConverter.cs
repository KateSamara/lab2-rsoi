using ReservationSystem.Domain.Models;

namespace ReservationSystem.Web.Dto.Converters;

public static class ReservationDtoConverter
{
    public static ReservationDto ToDto(this Reservation reservation)
    {
        return new ReservationDto(reservationUuid: reservation.ReservationUuid,
            status: reservation.Status.ToString(),
            startDate: reservation.StartDate,
            tillDate: reservation.TillDate,
            bookUuid: reservation.BookUuid,
            libraryUuid: reservation.LibraryUuid);
    }
}