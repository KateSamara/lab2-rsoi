using LibrarySystem.Domain.Exceptions.Services;
using LibrarySystem.Domain.Interfaces.Repositories;
using LibrarySystem.Domain.Interfaces.Services;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Services;

public class LibraryService(ILibraryRepository libraryRepository) : ILibraryService
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));

    public async Task<LibraryPaged> GetLibrariesPagedAsync(LibraryRequest libraryRequest)
    {
        try
        {
            return await _libraryRepository.GetLibrariesPagedAsync(libraryRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new LibraryServiceException("An error occured while getting all libraries.", e);
        }
    }
}