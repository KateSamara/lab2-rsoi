using ReservationSystem.Domain.Models;

namespace ReservationSystem.DataAccess.Models.Converters;

public static class ReservationStatusDbConverter
{
    public static ReservationStatus ToDomain(this ReservationStatusDb reservationStatusDb)
    {
        return reservationStatusDb switch
        {
            ReservationStatusDb.RENTED => ReservationStatus.RENTED,
            ReservationStatusDb.RETURNED => ReservationStatus.RETURNED,
            ReservationStatusDb.EXPIRED => ReservationStatus.EXPIRED,
            _ => ReservationStatus.RETURNED
        };
    }
}