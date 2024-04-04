using Microsoft.AspNetCore.Mvc;
using VolunteerPlatform.Application.Abstractions.Messaging;
using VolunteerPlatform.Application.Owners.Commands;
using VolunteerPlatform.Application.Services;
using VolunteerPlatform.Infrastructure.Queries;

namespace VolunteerPlatform.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnersController : ControllerBase
{
    private readonly ILogger<OwnersController> _logger;
    private readonly ICacheService _cacheService;
    private readonly IDispatcher _dispatcher;

    public OwnersController(ILogger<OwnersController> logger, ICacheService cacheService, IDispatcher dispatcher)
    {
        _logger = logger;
        _cacheService = cacheService;
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> RegisterOwner(
        RegisterOwnerCommand command,
        CancellationToken ct = default)
    {
        var result = await _dispatcher.Dispatch(command, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
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

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(GetOwnersHandler handler, CancellationToken ct = default)
    {
        var response = await _cacheService.GetOrCreate(
            "AllOwners",
            async () => await handler.Handle(),
            ct);

        return Ok(response);
    }
}