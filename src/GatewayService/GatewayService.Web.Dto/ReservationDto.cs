using System.Text.Json.Serialization;

namespace GatewayService.Web.Dto;

public class ReservationDto
{
    [JsonRequired]
    [JsonPropertyName("reservationUid")]
    public Guid ReservationUuid { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("bookUid")]
    public Guid BookUuid { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("libraryUid")]
    public Guid LibraryUuid { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("tillDate")]
    public DateTime TillDate { get; set; }

    public ReservationDto(Guid reservationUuid, Guid bookUuid, Guid libraryUuid, string status, DateTime startDate, DateTime tillDate)
    {
        ReservationUuid = reservationUuid;
        BookUuid = bookUuid;
        LibraryUuid = libraryUuid;
        Status = status;
        StartDate = startDate;
        TillDate = tillDate;
    }
}