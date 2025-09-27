using LibrarySystem.Domain.Models;

namespace LibrarySystem.Domain.Interfaces.Repositories;

public interface ILibraryRepository
{
    public Task<int> GetLibrariesCountAsync();
    
    public Task AddLibraryAsync(Library library);
}