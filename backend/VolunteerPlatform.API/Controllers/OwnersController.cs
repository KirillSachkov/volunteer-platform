using Microsoft.AspNetCore.Mvc;
using VolunteerPlatform.Application.Owners.Commands;
using VolunteerPlatform.Infrastructure.Queries;

namespace VolunteerPlatform.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnersController : ControllerBase
{
    private readonly ILogger<OwnersController> _logger;

    public OwnersController(ILogger<OwnersController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> RegisterOwner(
        RegisterOwnerHandler handler,
        RegisterOwnerCommand command,
        CancellationToken ct = default)
    {
        var result = await handler.Handle(command, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("cat")]
    public async Task<ActionResult> PublishCat(
        PublishCatHandler handler,
        [FromForm] PublishCatCommand command,
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
        var response = await handler.Handle();

        return Ok(response);
    }
}