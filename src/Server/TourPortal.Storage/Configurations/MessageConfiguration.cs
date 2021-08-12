namespace TourPortal.Storage.Configurations
{
    using Infrastructure.Storage.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(g => g.RecipientId)
                .IsRequired();

            builder.Property(g => g.TextMessage)
                .IsRequired();

            builder.HasOne(a => a.Sender)
                .WithMany(m => m.Messages)
                .HasForeignKey(u => u.SenderId);
        }
    }
}