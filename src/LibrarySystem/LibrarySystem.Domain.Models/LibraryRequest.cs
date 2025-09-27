namespace LibrarySystem.Domain.Models;

public record LibraryRequest
{
    public required int Page { get; init; }
    public required int Size { get; init; }
    public required string City { get; init; }
}