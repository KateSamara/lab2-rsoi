using LibrarySystem.Domain.Models;

namespace LibrarySystem.DataAccess.Models.Converters;

public static class LibraryDbConverter
{
    public static LibraryDb ToDb(this Library library)
    {
        return new LibraryDb(id: library.Id,
            libraryUuid: library.LibraryUuid,
            name: library.Name,
            city: library.City,
            address: library.Address);
    }
}