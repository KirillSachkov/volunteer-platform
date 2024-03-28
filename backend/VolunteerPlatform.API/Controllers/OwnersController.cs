using Microsoft.AspNetCore.Mvc;
using VolunteerPlatform.Application.Owners.Commands;
using VolunteerPlatform.Persistence.Queries;

namespace VolunteerPlatform.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> PublishCat(
        PublishCatHandler handler,
        PublishCatCommand command,
        CancellationToken ct = default)
    {
        var result = await handler.Handle(command, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(GetOwnersHandler handler, CancellationToken ct = default)
    {
        var response = await handler.Handle(ct);

        return Ok(response);
    }
}