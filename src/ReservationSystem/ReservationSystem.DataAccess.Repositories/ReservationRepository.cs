using Microsoft.EntityFrameworkCore;
using ReservationStatus.Domain.Exceptions.Repositories;
using ReservationSystem.DataAccess.Context;
using ReservationSystem.DataAccess.Models.Converters;
using ReservationSystem.Domain.Interfaces.Repositories;
using ReservationSystem.Domain.Models;

namespace ReservationSystem.DataAccess.Repositories;

public class ReservationRepository(ReservationSystemContext reservationSystemContext) : IReservationRepository
{
    private readonly ReservationSystemContext _reservationSystemContext = reservationSystemContext ?? throw new ArgumentNullException(nameof(reservationSystemContext));

    public async Task<List<Reservation>> GetReservationsByUsernameAsync(string username)
    {
        try
        {
            var reservationsDb = await _reservationSystemContext.Reservations
                .AsNoTracking()
                .Where(r => r.Username == username)
                .ToListAsync();

            return reservationsDb.ConvertAll(r => r.ToDomain());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ReservationRepositoryException($"Error while getting reservations by username = {username}.", e);
        }
    }
}