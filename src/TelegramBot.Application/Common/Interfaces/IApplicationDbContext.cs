using System.Threading;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Domain.Entities;

namespace TelegramBot.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TelegramUser> TelegramUsers { get; }
        DbSet<Server> Servers { get; }
        DbSet<PlanCategory> PlanCategories { get; }
        DbSet<Plan> Plans { get; }
        DbSet<Order> Orders { get; }
        DbSet<Discount> Discounts { get; }
        DbSet<DiscountUsage> DiscountUsages { get; }
        DbSet<PaymentHistory> PaymentHistories { get; }
        DbSet<Ticket> Tickets { get; }
        DbSet<TicketMessage> TicketMessages { get; }
        DbSet<AppSetting> AppSettings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
