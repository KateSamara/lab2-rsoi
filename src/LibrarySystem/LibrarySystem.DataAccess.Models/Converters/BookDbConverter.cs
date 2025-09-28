using LibrarySystem.Domain.Models;
using LibrarySystem.Domain.Models.Books;

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

    public static Book ToDomain(this LibraryBookDb libraryBook)
    {
        return new Book
        {
            Id = libraryBook.Book!.Id,
            BookUuid = libraryBook.Book!.BookUuid,
            Name = libraryBook.Book!.Name,
            Author = libraryBook.Book!.Author,
            Genre = libraryBook.Book!.Genre,
            Condition = libraryBook.Book!.Condition.ToDomain(),
            AvailableCount = libraryBook.AvailableCount
        };
    }
}