using LibrarySystem.Domain.Models;

namespace LibrarySystem.Web.Dto.Converters;

public static class LibraryDtoConverter
{
    public static LibraryDto ToDto(this Library library)
    {
        return new LibraryDto(libraryUuid: library.LibraryUuid, 
            name: library.Name, 
            address: library.Address,
            city: library.City);
    }

    public static LibraryPagedDto ToDto(this LibraryPaged libraryPagedDto)
    {
        return new LibraryPagedDto(page: libraryPagedDto.Page, 
            pageSize: libraryPagedDto.PageSize,
            totalItems: libraryPagedDto.TotalItems,
            items: libraryPagedDto.Items.ConvertAll(l => l.ToDto()));
    }
}