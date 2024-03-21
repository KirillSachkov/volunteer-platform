﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolunteerPlatform.Domain.Entities;

namespace VolunteerPlatform.Persistence.Configurations;

public class CatConfiguration : IEntityTypeConfiguration<Cat>
{
    public void Configure(EntityTypeBuilder<Cat> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasMany(c => c.Tags).WithMany().UsingEntity("CatTags");
        builder.ComplexProperty(c => c.PhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Number).HasColumnName("PhoneNumber");
        });
        builder.ComplexProperty(c => c.Gender, b =>
        {
            b.IsRequired();
            b.Property(g => g.Value).HasColumnName("Gender");
        });
        builder.ComplexProperty(c => c.Age, b =>
        {
            b.IsRequired();
            b.Property(a => a.Years).HasColumnName("Years");
            b.Property(a => a.Months).HasColumnName("Months");
        });
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.AnimalAttitude).IsRequired(false);
        builder.Property(c => c.PeopleAttitude).IsRequired(false);
        builder.Property(c => c.Vaccine).IsRequired(false);
        builder.Property(c => c.Color).IsRequired(false);
        builder.Property(c => c.Place).IsRequired(false);
        builder.Property(c => c.Health).IsRequired(false);
    }
}