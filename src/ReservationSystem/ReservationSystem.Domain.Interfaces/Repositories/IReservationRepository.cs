using ReservationSystem.Domain.Models;

namespace ReservationSystem.Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    public Task<List<Reservation>> GetReservationsByUsernameAsync(string username);
}