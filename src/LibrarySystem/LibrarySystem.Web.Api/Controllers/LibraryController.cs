using LibrarySystem.Domain.Interfaces.Services;
using LibrarySystem.Domain.Models;
using LibrarySystem.Web.Dto;
using LibrarySystem.Web.Dto.Converters;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/libraries")]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public LibraryController(ILibraryService libraryService)
    {
        _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(LibraryPagedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLibrariesPagedAsync([FromQuery] int page,
        [FromQuery] int size,
        [FromQuery] string city)
    {
        var libraryRequest = new LibraryRequest
        {
            Page = page,
            Size = size,
            City = city
        };
        
        var librariesPaged = await _libraryService.GetLibrariesPagedAsync(libraryRequest);
        
        return Ok(librariesPaged.ToDto());
    }
}