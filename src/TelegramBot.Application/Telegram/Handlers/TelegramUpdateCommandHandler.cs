using MediatR;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Interfaces.Repositories;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Application.Telegram.Commands;
using TelegramBot.Application.TelegramUsers.Dtos;

namespace TelegramBot.Application.Telegram.Handlers;

public class TelegramUpdateCommandHandler
    : IRequestHandler<TelegramUpdateCommand>
{
    private readonly IEnumerable<ITelegramUpdateHandler> _handlers;
    private readonly IEfTelegramUserRepository _users;

    public TelegramUpdateCommandHandler(
        IEnumerable<ITelegramUpdateHandler> handlers,
        IEfTelegramUserRepository users)
    {
        _handlers = handlers;
        _users = users;
    }

    public async Task Handle(
        TelegramUpdateCommand request,
        CancellationToken ct)
    {
        var context = TelegramUpdateContext.From(request.Update);

        var user = await _users.GetByTelegramIdAsync(
            context.TelegramUserId,
            new TelegramUserIncludes(),
            ct);

        foreach (var handler in _handlers)
        {
            if (handler.CanHandle(user, context))
            {
                await handler.HandleAsync(user, context, ct);
            }
        }
    }
}
