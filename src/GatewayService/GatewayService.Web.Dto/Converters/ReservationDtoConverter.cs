namespace GatewayService.Web.Dto.Converters;

public static class ReservationDtoConverter
{
    public static ReservationFullDto ToFullDto(this ReservationDto reservationDto, 
        BookDto bookDto,
        LibraryDto libraryDto)
    {
        return new ReservationFullDto(reservationUuid: reservationDto.ReservationUuid,
            status: reservationDto.Status,
            startDate: reservationDto.StartDate.Date,
            tillDate: reservationDto.TillDate.Date,
            book: bookDto,
            library: libraryDto);
    }
}