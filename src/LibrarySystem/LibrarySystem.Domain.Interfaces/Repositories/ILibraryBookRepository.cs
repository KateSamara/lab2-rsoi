using LibrarySystem.Domain.Models;

namespace LibrarySystem.Domain.Interfaces.Repositories;

public interface ILibraryBookRepository
{
    public Task<int> GetLibraryBooksCountAsync();
    
    public Task AddLibraryBookAsync(LibraryBook libraryBook);
}