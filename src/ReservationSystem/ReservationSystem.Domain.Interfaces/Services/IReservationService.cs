using ReservationSystem.Domain.Models;

namespace ReservationSystem.Domain.Interfaces.Services;

public interface IReservationService
{
    public Task<List<Reservation>> GetReservationsByUsernameAsync(string username);
}