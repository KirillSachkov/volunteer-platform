using Microsoft.AspNetCore.Mvc;
using VolunteerPlatform.Application.Owners;
using VolunteerPlatform.Application.Owners.Requests;

namespace VolunteerPlatform.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnersController : ControllerBase
{
    private readonly OwnersService _ownersService;

    public OwnersController(OwnersService ownersService)
    {
        _ownersService = ownersService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCat(PublishCatRequest request, CancellationToken ct)
    {
        var result = await _ownersService.PublishCat(request, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}