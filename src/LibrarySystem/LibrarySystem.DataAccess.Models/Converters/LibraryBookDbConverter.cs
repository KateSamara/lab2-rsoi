using LibrarySystem.Domain.Models;

namespace LibrarySystem.DataAccess.Models.Converters;

public static class LibraryBookDbConverter
{
    public static LibraryBookDb ToDb(this LibraryBook libraryBook, int id)
    {
        return new LibraryBookDb(id: id,
            bookId: libraryBook.BookId,
            libraryId: libraryBook.LibraryId,
            availableCount: libraryBook.AvailableCount);
    }
}