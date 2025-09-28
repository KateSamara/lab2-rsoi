using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Domain.Interfaces.Services;
using ReservationSystem.Web.Dto;
using ReservationSystem.Web.Dto.Converters;

namespace ReservationSystem.Web.Controllers;

[ApiController]
[Route("/api/v1/reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ReservationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReservationsByUsernameAsync([FromHeader(Name = "X-User-Name")] string username)
    {
        var reservations = await _reservationService.GetReservationsByUsernameAsync(username);
        
        return Ok(reservations.ConvertAll(r => r.ToDto()));
    }
}