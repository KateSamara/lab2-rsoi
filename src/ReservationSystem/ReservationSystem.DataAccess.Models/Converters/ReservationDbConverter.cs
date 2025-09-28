using ReservationSystem.Domain.Models;

namespace ReservationSystem.DataAccess.Models.Converters;

public static class ReservationDbConverter
{
    public static Reservation ToDomain(this ReservationDb reservationDb)
    {
        return new Reservation
        {
            Id = reservationDb.Id,
            ReservationUuid = reservationDb.ReservationUuid,
            Username = reservationDb.Username,
            BookUuid = reservationDb.BookUuid,
            LibraryUuid = reservationDb.LibraryUuid,
            Status = reservationDb.Status.ToDomain(),
            StartDate = reservationDb.StartDate,
            TillDate = reservationDb.TillDate
        };
    }
}