using ReservationStatus.Domain.Exceptions.Services;
using ReservationSystem.Domain.Interfaces.Repositories;
using ReservationSystem.Domain.Interfaces.Services;
using ReservationSystem.Domain.Models;

namespace ReservationSystem.Application.Services;

public class ReservationService(IReservationRepository reservationRepository) : IReservationService
{
    private readonly IReservationRepository _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    
    public async Task<List<Reservation>> GetReservationsByUsernameAsync(string username)
    {
        try
        {
            return await _reservationRepository.GetReservationsByUsernameAsync(username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ReservationServiceException($"Error while getting reservations by username = {username}.", e);
        }
    }
}