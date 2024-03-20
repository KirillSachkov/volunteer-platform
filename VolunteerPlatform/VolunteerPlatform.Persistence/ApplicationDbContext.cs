using Microsoft.EntityFrameworkCore;
using VolunteerPlatform.Domain.Entities;

namespace VolunteerPlatform.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Owner> Owners => Set<Owner>();
}