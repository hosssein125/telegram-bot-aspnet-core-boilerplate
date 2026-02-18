using MediatR;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Application.Telegram.Commands;
using TelegramBot.Application.TelegramUsers.Dtos;

namespace TelegramBot.Application.Telegram.Handlers;

public class TelegramUpdateCommandHandler
    : IRequestHandler<TelegramUpdateCommand>
{
    private readonly IEnumerable<ITelegramUpdateHandler> _handlers;
    private readonly IDbUnitOfWork _dbUnitOfWork;

    public TelegramUpdateCommandHandler(
        IEnumerable<ITelegramUpdateHandler> handlers,
        IDbUnitOfWork dbUnitOfWork)
    {
        _handlers = handlers;
        _dbUnitOfWork = dbUnitOfWork;
    }

    public async Task Handle(
        TelegramUpdateCommand request,
        CancellationToken ct)
    {
        var context = TelegramUpdateContext.From(request.Update);

        var user = await _dbUnitOfWork.TelegramUsers.GetByTelegramIdAsync(
            context.TelegramUserId,
            new TelegramUserIncludes() { Orders = true },
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
