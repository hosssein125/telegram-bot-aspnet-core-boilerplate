using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramBot.Domain.Entities;

namespace TelegramBot.Infrastructure.Persistence.EntityConfigurations
{
    internal class TicketEntityConfigurations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn().IsRequired();

            builder.HasMany(u => u.Messages)
                .WithOne(o => o.Ticket)
                .HasForeignKey(o => o.TicketId);
        }
    }
}