
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Domain;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions opts) : base(opts)
        {
            Database.EnsureCreated();
        }

    public DbSet<Tagg> Taggs { get; set; }

}
