using LibrarySystem.Domain.Models;

namespace LibrarySystem.Domain.Interfaces.Services;

public interface ILibraryService
{
    public Task<LibraryPaged> GetLibrariesPagedAsync(LibraryRequest libraryRequest);
}