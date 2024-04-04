using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using VolunteerPlatform.Application.Abstractions.Messaging;

namespace VolunteerPlatform.Application.Decorators;

public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _handler;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;

    public LoggingCommandHandlerDecorator(
        ICommandHandler<TCommand> handler,
        ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
    {
        _handler = handler;
        _logger = logger;
    }

    public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Executing command handler for command type {@CommandName}",
            typeof(TCommand).Name);

        var result = await _handler.Handle(command, cancellationToken);

        _logger.LogInformation(
            "Command handler for command type {@CommandName} executed",
            typeof(TCommand).Name);

        return result;
    }
}