using Microsoft.EntityFrameworkCore;
using TelegramBot.Application.TelegramUsers.Dtos;
using TelegramBot.Domain.Entities;

namespace TelegramBot.Infrastructure.Persistence.Extensions
{
    public static class TelegramUserExtensions
    {
        public static IQueryable<TelegramUser> ApplyIncludes(
            this IQueryable<TelegramUser> query,
            TelegramUserIncludes includes)
        {
            if (includes.Payments)
                query = query.Include(x => x.Payments);

            if (includes.Orders)
                query = query.Include(x => x.Orders);

            if (includes.DiscountUsages)
                query = query.Include(x => x.DiscountUsages);

            if (includes.Tickets)
                query = query.Include(x => x.Tickets);

            if (includes.InvitedByUser)
                query = query.Include(x => x.InvitedByUser);

            return query;
        }

        public static IQueryable<TelegramUser> ApplyFilter(this IQueryable<TelegramUser> query,
            TelegramUserFilter filter)
        {

            // Filter by FirstName
            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(x => x.FirstName != null &&
                    EF.Functions.ILike(x.FirstName, $"%{filter.FirstName}%"));

            // Filter by LastName
            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(x => x.LastName != null &&
                                         EF.Functions.ILike(x.LastName, $"%{filter.LastName}%"));

            // Filter by FullName
            if (!string.IsNullOrEmpty(filter.FullName))
                query = query.Where(x => (x.FirstName != null &&
                                         EF.Functions.ILike(x.FirstName, $"%{filter.FirstName}%"))
                                        || (x.LastName != null &&
                                         EF.Functions.ILike(x.LastName, $"%{filter.LastName}%")));

            // Filter By Balance From
            if (filter.WalletBalanceFrom.HasValue)
                query = query.Where(x => x.WalletBalance >= filter.WalletBalanceFrom);

            // Filter By Balance To
            if (filter.WalletBalanceTo.HasValue)
                query = query.Where(x => x.WalletBalance <= filter.WalletBalanceTo);

            // Filter By InvitedByUserId
            if (filter.InvitedByUserId.HasValue)
                query = query.Where(x => x.InvitedByUserId == filter.InvitedByUserId);

            // Filter by ReferralCode
            if (!string.IsNullOrEmpty(filter.ReferralCode))
                query = query.Where(x => x.ReferralCode == filter.ReferralCode);

            // Filter by TelegramUsername
            if (!string.IsNullOrEmpty(filter.TelegramUsername))
                query = query.Where(x => x.TelegramUsername == filter.TelegramUsername);

            // Filter By State
            if (filter.State.HasValue)
                query = query.Where(x => x.State == filter.State);

            // Filter By Role
            if (filter.Role.HasValue)
                query = query.Where(x => x.Role == filter.Role);

            // Filter By IsActive
            if (filter.IsActive.HasValue)
                query = query.Where(x => x.IsActive == filter.IsActive);

            // Filter By TelegramId
            if (filter.TelegramId.HasValue)
                query = query.Where(x => x.TelegramId == filter.TelegramId);

            // Filter By Server Switch No From
            if (filter.ServerSwitchNoFrom.HasValue)
                query = query.Where(x => x.ServerSwitchNo >= filter.ServerSwitchNoFrom);

            // Filter By Server Switch No To
            if (filter.ServerSwitchNoTo.HasValue)
                query = query.Where(x => x.ServerSwitchNo <= filter.ServerSwitchNoTo);

            // Filter By Created At From
            if (filter.CreatedAtFrom.HasValue)
                query = query.Where(x => x.CreatedAt >= filter.CreatedAtFrom);

            // Filter By Created At To
            if (filter.CreatedAtTo.HasValue)
                query = query.Where(x => x.CreatedAt <= filter.CreatedAtTo);

            return query;
        }

        public static IQueryable<TelegramUser> ApplySort(this IQueryable<TelegramUser> query,
            TelegramUserSortBy? sortBy, bool? isDesc)
        {

            isDesc ??= true;

            var sortedItems = sortBy switch
            {
                TelegramUserSortBy.Id =>
                    isDesc.Value ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id),

                _ => query.OrderByDescending(x => x.Id)
            };

            return sortedItems;
        }
    }
}
