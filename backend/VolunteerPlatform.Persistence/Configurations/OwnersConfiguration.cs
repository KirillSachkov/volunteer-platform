using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerPlatform.Domain.Entities;

namespace VolunteerPlatform.Persistence.Configurations;

public class OwnersConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasKey(o => o.Id);
        builder.ComplexProperty(c => c.PhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Number);
        });
        builder.ComplexProperty(c => c.Credentials, b =>
        {
            b.IsRequired();
            b.Property(c => c.Login);
            b.Property(c => c.PasswordHash);
        });
        builder.HasMany(o => o.Cats).WithOne();
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.Description).IsRequired(false);
        builder.Property(o => o.ProfilePhoto).IsRequired(false);
    }
}