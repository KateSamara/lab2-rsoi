using LibrarySystem.DataAccess.Context;
using LibrarySystem.DataAccess.Models.Converters;
using LibrarySystem.Domain.Exceptions.Repositories;
using LibrarySystem.Domain.Interfaces.Repositories;
using LibrarySystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibrarySystem.DataAccess.Repositories;

public class LibraryRepository(LibrarySystemContext context) : ILibraryRepository
{
    private readonly LibrarySystemContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<int> GetLibrariesCountAsync()
    {
        try
        {
            return await _context.Libraries
                .CountAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new LibraryRepositoryException("There was an error getting the libraries.", e);
        }
    }

    public async Task AddLibraryAsync(Library library)
    {
        try
        {
            var libraryDb = library.ToDb();
            
            _context.Libraries.Add(libraryDb);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new LibraryRepositoryException("There was an error adding the library.", e);
        }
    }
}