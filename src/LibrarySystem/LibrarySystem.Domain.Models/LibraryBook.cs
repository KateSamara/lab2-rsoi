namespace LibrarySystem.Domain.Models;

public record LibraryBook
{
    public required int BookId { get; init; }
    public required int LibraryId { get; init; }
    public required int AvailableCount { get; init; }
}