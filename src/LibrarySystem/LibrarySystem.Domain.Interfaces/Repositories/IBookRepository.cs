using LibrarySystem.Domain.Models;

namespace LibrarySystem.Domain.Interfaces.Repositories;

public interface IBookRepository
{
    public Task<int> GetBooksCountAsync();
    
    public Task AddBookAsync(Book book);
}