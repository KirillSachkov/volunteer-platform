using Microsoft.AspNetCore.Mvc;
using VolunteerPlatform.Application;
using VolunteerPlatform.Application.Requests;
using VolunteerPlatform.Application.Services;

namespace VolunteerPlatform.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CatController : ControllerBase
{
    private readonly OwnersService _ownersService;

    public CatController(OwnersService ownersService)
    {
        _ownersService = ownersService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCat(CreateCatRequest request, CancellationToken ct)
    {
        var result = await _ownersService.PublishCat(request, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}