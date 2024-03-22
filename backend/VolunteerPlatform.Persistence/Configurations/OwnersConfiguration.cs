using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerPlatform.Domain.Entities;

namespace VolunteerPlatform.Persistence.Configurations;

public class OwnersConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");
        builder.HasKey(o => o.Id);
        builder.ComplexProperty(c => c.PhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Number).HasColumnName("PhoneNumber");
        });
        builder.ComplexProperty(c => c.Credentials, b =>
        {
            b.IsRequired();
            b.Property(c => c.Login).HasColumnName("Login");
            b.Property(c => c.PasswordHash).HasColumnName("PasswordHash");
        });
        builder.HasMany(o => o.Cats).WithOne().HasForeignKey("OwnerId");
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.Description).IsRequired(false);
        builder.Property(o => o.ProfilePhoto).IsRequired(false);
    }
}