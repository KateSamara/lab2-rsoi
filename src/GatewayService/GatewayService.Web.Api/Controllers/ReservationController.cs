using System.Text;
using System.Text.Json;
using GatewayService.Configuration;
using GatewayService.Web.Dto;
using GatewayService.Web.Dto.Books;
using GatewayService.Web.Dto.Converters;
using GatewayService.Web.Dto.Libraries;
using GatewayService.Web.Dto.Ratings;
using GatewayService.Web.Dto.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GatewayService.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/reservations")]
[ServiceFilter(typeof(ValidationFilterAttribute))]
public class ReservationController : ControllerBase
{
    private readonly ReservationSystemConfiguration _reservationSystemConfiguration;
    private readonly LibrarySystemConfiguration _librarySystemConfiguration;
    private readonly RatingSystemConfiguration _ratingSystemConfiguration;

    public ReservationController(IOptions<ReservationSystemConfiguration> reservationSystemConfiguration,
        IOptions<LibrarySystemConfiguration> librarySystemConfiguration,
        IOptions<RatingSystemConfiguration> ratingSystemConfiguration)
    {
        _reservationSystemConfiguration = reservationSystemConfiguration.Value;
        _librarySystemConfiguration = librarySystemConfiguration.Value;
        _ratingSystemConfiguration = ratingSystemConfiguration.Value;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<ReservationFullDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReservationsByUsernameAsync([FromHeader(Name = "X-User-Name")] string username)
    {
        using var client = new HttpClient();

        // Получение брони по пользователю
        var reservations = await GetReservationsByUsernameAsync(client, username);
        
        var bookUuids = reservations.Select(r => r.BookUuid).Distinct().ToList();
        var libraryUuids = reservations.Select(r => r.LibraryUuid).Distinct().ToList();
        
        // Получение книг по идентификаторам
        var books = await GetBooksByIdsAsync(client, bookUuids);
        
        // Получение библиотек по идентификаторам
        var libraries = await GetLibrariesByIdsAsync(client, libraryUuids);
        
        // Составление полных моделей бронирования
        List<ReservationFullDto> reservationsFull = [];
        foreach (var reservation in reservations)
        {
            reservationsFull.Add(reservation.ToFullDto(books!.First(b => b.BookUuid == reservation.BookUuid),
                libraries!.First(b => b.LibraryUuid == reservation.LibraryUuid), null));
        }
        
        return Ok(reservationsFull);
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<ReservationFullDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateReservationAsync([FromHeader(Name = "X-User-Name")] string username,
        [FromBody] ReservationCreateDto reservationCreate)
    {
        using var client = new HttpClient();
        
        // Получение количества книг на руках
        var currentReservationsCount = await GetCurrentReservationsCountAsync(client, username);
        
        // Получаем количество звёздочек
        var rating = await GetRatingAsync(client, username);
        
        // Проверка
        if (currentReservationsCount >= rating.Stars)
            return StatusCode(StatusCodes.Status403Forbidden);
        
        // Создание бронирования
        var newReservation = await AddReservationAsync(client, username, reservationCreate);
        
        // Меняем инфу в библиотеке и получаем книжку (СПАСИТЕ, Я ЗАДОЛБАЛАСЬ ЭТО ДЕЛАТЬ)
        var libraryBook = await UpdateAvailableBooksCount(client, 
            reservationCreate.BookUuid, 
            reservationCreate.LibraryUuid,
            false);
         
        return Ok(newReservation.ToFullDto(libraryBook.Book, libraryBook.Library, rating));
    }

    private async Task<List<LibraryDto>> GetLibrariesByIdsAsync(HttpClient client, List<Guid> libraryUuids)
    {
        using var libraryRequest = new HttpRequestMessage(HttpMethod.Get,
            $"{_librarySystemConfiguration.IpAddress}/{_librarySystemConfiguration.BaseUrl}/{_librarySystemConfiguration.SearchByIdsSuffix}{BuildPartUrlWithIds(libraryUuids)}");
        using var libraryResponse = await client.SendAsync(libraryRequest);
        libraryResponse.EnsureSuccessStatusCode();
        var libraryJson = await libraryResponse.Content.ReadAsStringAsync();
        
        var libraries = JsonSerializer.Deserialize<List<LibraryDto>>(libraryJson);

        return libraries;
    }

    private async Task<List<BookDto>> GetBooksByIdsAsync(HttpClient client, List<Guid> bookUuids)
    {
        using var booksRequest = new HttpRequestMessage(HttpMethod.Get,
            $"{_librarySystemConfiguration.IpAddress}/{_librarySystemConfiguration.BaseBookUrl}/{_librarySystemConfiguration.SearchByIdsSuffix}{BuildPartUrlWithIds(bookUuids)}");
        using var bookResponse = await client.SendAsync(booksRequest);
        bookResponse.EnsureSuccessStatusCode();
        var bookJson = await bookResponse.Content.ReadAsStringAsync();
        
        var books = JsonSerializer.Deserialize<List<BookDto>>(bookJson);
        
        return books;
    }

    private async Task<List<ReservationDto>> GetReservationsByUsernameAsync(HttpClient client, string username)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_reservationSystemConfiguration.IpAddress}/{_reservationSystemConfiguration.BaseUrl}");
        request.Headers.Add(_reservationSystemConfiguration.UsernameHeader, username);

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        
        var reservations = JsonSerializer.Deserialize<List<ReservationDto>>(json);

        return reservations;
    }

    private async Task<LibraryBookDto> UpdateAvailableBooksCount(HttpClient client, 
        Guid bookUuid, 
        Guid libraryUuid,
        bool isIncrease)
    {
        using var request = new HttpRequestMessage(HttpMethod.Patch,
            $"{_librarySystemConfiguration.IpAddress}/{_librarySystemConfiguration.BaseUrl}/" +
            $"{libraryUuid}/{_librarySystemConfiguration.GetBooksSuffix}/{bookUuid}?isIncrease={isIncrease}");

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        
        var libraryBook = JsonSerializer.Deserialize<LibraryBookDto>(json);
        
        return libraryBook;
    }

    private async Task<ReservationDto> AddReservationAsync(HttpClient client, string username, ReservationCreateDto reservationCreate)
    {
        var content = JsonSerializer.Serialize(reservationCreate);
        
        using var request = new HttpRequestMessage(HttpMethod.Post,
            $"{_reservationSystemConfiguration.IpAddress}/{_reservationSystemConfiguration.BaseUrl}");
        request.Headers.Add(_reservationSystemConfiguration.UsernameHeader, username);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var newReservationJson = await response.Content.ReadAsStringAsync();
        
        var newReservation = JsonSerializer.Deserialize<ReservationDto>(newReservationJson);
        
        return newReservation;
    }

    private async Task<RatingDto> GetRatingAsync(HttpClient client, string username)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_ratingSystemConfiguration.IpAddress}/{_ratingSystemConfiguration.BaseUrl}");
        request.Headers.Add(_ratingSystemConfiguration.UsernameHeader, username);

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        
        var rating = JsonSerializer.Deserialize<RatingDto>(json);
        
        return rating;
    }

    private async Task<int> GetCurrentReservationsCountAsync(HttpClient client, string username)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"{_reservationSystemConfiguration.IpAddress}/{_reservationSystemConfiguration.BaseUrl}/RENTED");
        request.Headers.Add(_reservationSystemConfiguration.UsernameHeader, username);

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var reservationCountResponseString = await response.Content.ReadAsStringAsync();
        
        var currentReservationsCount = int.Parse(reservationCountResponseString);
        
        return currentReservationsCount;
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