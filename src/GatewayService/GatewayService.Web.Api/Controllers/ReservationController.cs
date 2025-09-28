using System.Text.Json;
using GatewayService.Configuration;
using GatewayService.Web.Dto;
using GatewayService.Web.Dto.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GatewayService.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/reservations")]
public class ReservationController : ControllerBase
{
    private readonly ReservationSystemConfiguration _reservationSystemConfiguration;
    private readonly LibrarySystemConfiguration _librarySystemConfiguration;

    public ReservationController(IOptions<ReservationSystemConfiguration> reservationSystemConfiguration,
        IOptions<LibrarySystemConfiguration> librarySystemConfiguration)
    {
        _reservationSystemConfiguration = reservationSystemConfiguration.Value;
        _librarySystemConfiguration = librarySystemConfiguration.Value;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<ReservationFullDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReservationsByUsernameAsync([FromHeader(Name = "X-User-Name")] string username)
    {
        using var client = new HttpClient();

        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_reservationSystemConfiguration.IpAddress}/{_reservationSystemConfiguration.BaseUrl}");
        request.Headers.Add(_reservationSystemConfiguration.UsernameHeader, username);

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        
        var reservations = JsonSerializer.Deserialize<List<ReservationDto>>(json);
        
        var bookUuids = reservations.Select(r => r.BookUuid).Distinct().ToList();
        var libraryUuids = reservations.Select(r => r.LibraryUuid).Distinct().ToList();
        
        // Получение книг по идентификаторам
        using var booksRequest = new HttpRequestMessage(HttpMethod.Get,
            $"{_librarySystemConfiguration.IpAddress}/{_librarySystemConfiguration.BaseBookUrl}/{_librarySystemConfiguration.SearchByIdsSuffix}{BuildPartUrlWithIds(bookUuids)}");
        using var bookResponse = await client.SendAsync(booksRequest);
        bookResponse.EnsureSuccessStatusCode();
        var bookJson = await bookResponse.Content.ReadAsStringAsync();
        
        var books = JsonSerializer.Deserialize<List<BookDto>>(bookJson);
        
        // Получение библиотек по идентификаторам
        using var libraryRequest = new HttpRequestMessage(HttpMethod.Get,
            $"{_librarySystemConfiguration.IpAddress}/{_librarySystemConfiguration.BaseUrl}/{_librarySystemConfiguration.SearchByIdsSuffix}{BuildPartUrlWithIds(libraryUuids)}");
        using var libraryResponse = await client.SendAsync(libraryRequest);
        libraryResponse.EnsureSuccessStatusCode();
        var libraryJson = await libraryResponse.Content.ReadAsStringAsync();
        
        var libraries = JsonSerializer.Deserialize<List<LibraryDto>>(libraryJson);
        
        // Составление полных моделей бронирования
        List<ReservationFullDto> reservationsFull = [];
        foreach (var reservation in reservations)
        {
            reservationsFull.Add(reservation.ToFullDto(books!.First(b => b.BookUuid == reservation.BookUuid),
                libraries!.First(b => b.LibraryUuid == reservation.LibraryUuid)));
        }
        
        return Ok(reservationsFull);
    }

    private string BuildPartUrlWithIds(List<Guid> ids)
    {
        var url = "?";
        foreach (var id in ids)
        {
            url += $"ids={id}&";
        }
        return url;
    }
}