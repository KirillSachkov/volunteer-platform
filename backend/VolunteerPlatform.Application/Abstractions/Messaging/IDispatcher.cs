using CSharpFunctionalExtensions;

namespace VolunteerPlatform.Application.Abstractions.Messaging;

public interface IDispatcher
{
    Task<Result> Dispatch(ICommand command, CancellationToken cancellationToken = default);
}