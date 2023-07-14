using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Customer).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAudits(ChangeTracker);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAudits(ChangeTracker changeTracker)
    {
        changeTracker.SetAuditableEntityPropertyValues();
    }

    public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
    {
        Entry(entity).State = EntityState.Detached;
        Update(entity);
    }
}

