
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Entities;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Domain.Context;

public class DataContext : IdentityDbContext<IdentityUser>
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        { }

    public DbSet<Tagg> Taggs { get; set; } = null!;
    public DbSet<Instance> Instances { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken tok)
    {
        this.ChangeTracker.DetectChanges();

        var newEntities = this.ChangeTracker.Entries()
                                            .Where(t => t.State == EntityState.Added)
                                            .Select(t => t.Entity as BaseEntity);
        foreach(var entity in newEntities)
        {
            // Generate UUID at this point
            if(entity is BaseEntity baseEntity)
            {
                baseEntity.Id = Guid.NewGuid();
            }

            // Set created date for any new dated entities
            if(entity is DatedEntity datedEntity)
            {
                datedEntity.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            }
        }

        var difEntities = this.ChangeTracker.Entries()
                                            .Where(t => t.State == EntityState.Modified)
                                            .Select(t => t.Entity as BaseEntity);
        foreach(var entity in difEntities)
        {
            if(entity is MutableDatedEntity datedEntity)
            {
                datedEntity.ModifiedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            }
        }

        var delEntries = this.ChangeTracker.Entries()
                                           .Where(t => t.State ==EntityState.Deleted);
        foreach(var entry in delEntries)
        {
            if(entry.Entity is MutableDatedEntity datedEntity)
            {
                // Deleted Entities to be soft deleted
                entry.State = EntityState.Modified;
                datedEntity.DeletedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            }
        }

        return base.SaveChangesAsync(tok);
    }

}
