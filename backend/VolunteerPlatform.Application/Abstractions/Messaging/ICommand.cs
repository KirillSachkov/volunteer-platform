using CSharpFunctionalExtensions;

namespace VolunteerPlatform.Application.Abstractions.Messaging;

public interface ICommand;

public interface ILoggingCommand : ICommand;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}