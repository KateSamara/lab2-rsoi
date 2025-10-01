using LibrarySystem.Domain.Models;
using LibrarySystem.Domain.Models.Books;
using LibrarySystem.Domain.Models.Libraries;

namespace LibrarySystem.Domain.Interfaces.Services;

public interface ILibraryService
{
    public Task<LibraryPaged> GetLibrariesPagedAsync(LibraryRequest libraryRequest);
    
    public Task<BookPaged> GetBookPagedByLibraryUuidAsync(BookRequest bookRequest);
    
    public Task<List<Library>> GetLibrariesByIdsAsync(List<Guid> ids);
    
    public Task<LibraryBook> DecreaseBookCountAsync(Guid libraryId, Guid bookId);
}