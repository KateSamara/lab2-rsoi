namespace ReservationSystem.DataAccess.Models;

public class ReservationDb
{
    public int Id { get; set; }
    public Guid ReservationUuid { get; set; } 
    public string Username { get; set; }
    public Guid BookUuid { get; set; }
    public Guid LibraryUuid { get; set; }
    public ReservationStatusDb Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime TillDate { get; set; }
}