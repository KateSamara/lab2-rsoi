using System.Text;
using System.Text.Json;
using GatewayService.Configuration;
using GatewayService.Web.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GatewayService.Controllers;

[ApiController]
[Route("/api/v1/libraries")]
public class LibraryController : ControllerBase
{
    private readonly LibrarySystemConfiguration _librarySystemConfiguration;

    public LibraryController(IOptions<LibrarySystemConfiguration> librarySystemConfiguration)
    {
        _librarySystemConfiguration = librarySystemConfiguration.Value;
    }

    [HttpGet]
    [ProducesResponseType(typeof(LibraryPagedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLibrariesPagedAsync([FromQuery] int page,
        [FromQuery] int size,
        [FromQuery] string city)
    {
        using var client = new HttpClient();
        
        var request = $"{_librarySystemConfiguration.IpAddress}/{_librarySystemConfiguration.BaseUrl}?page={page}&size={size}&city={city}";
        var response = await client.GetAsync(request);
        
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        
        var libraryPaged = JsonSerializer.Deserialize<LibraryPagedDto>(json);
        return Ok(libraryPaged);
    }
}