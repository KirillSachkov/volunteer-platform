using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using VolunteerPlatform.Application.Abstractions.Messaging;

namespace VolunteerPlatform.Application.Utils;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Result> Dispatch(ICommand command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

        dynamic? handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for command type {typeof(ICommand)}");
        }

        return await handler.Handle((dynamic)command, cancellationToken);
    }
}