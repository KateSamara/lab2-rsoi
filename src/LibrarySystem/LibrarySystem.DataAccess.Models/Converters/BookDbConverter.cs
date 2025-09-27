using LibrarySystem.Domain.Models;

namespace LibrarySystem.DataAccess.Models.Converters;

public static class BookDbConverter
{
    public static BookDb ToDb(this Book book)
    {
        return new BookDb(id: book.Id,
            bookUuid: book.BookUuid,
            name: book.Name,
            author: book.Author,
            genre: book.Genre,
            condition: book.Condition.ToDb());
    }
}