using System.Text.Json.Serialization;

namespace GatewayService.Web.Dto;

public class ReservationFullDto
{
    [JsonRequired]
    [JsonPropertyName("reservationUid")]
    public Guid ReservationUuid { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("tillDate")]
    public DateTime TillDate { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("book")]
    public BookDto Book { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("library")]
    public LibraryDto Library { get; set; }

    public ReservationFullDto(Guid reservationUuid, string status, DateTime startDate, DateTime tillDate, BookDto book, LibraryDto library)
    {
        ReservationUuid = reservationUuid;
        Status = status;
        StartDate = startDate;
        TillDate = tillDate;
        Book = book;
        Library = library;
    }
}