using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Entities;
public static class AuditableShadowProperties
{
    public static readonly Func<object, DateTime?> EFPropertyCreatedOn =
            entity => EF.Property<DateTime?>(entity, CreatedOn);

    public static readonly string CreatedOn = nameof(CreatedOn);

    public static readonly Func<object, DateTime?> EFPropertyModifiedOn =
        entity => EF.Property<DateTime?>(entity, ModifiedOn);

    public static readonly string ModifiedOn = nameof(ModifiedOn);

    public static void SetAuditableEntityPropertyValues(this ChangeTracker changeTracker)
    {
        if (changeTracker == null)
        {
            throw new ArgumentNullException(nameof(changeTracker));
        }

        var auditedEntity = changeTracker.Entries<BaseAuditedEntity>();
        if (auditedEntity.Any())
        {
            var addedEntries = auditedEntity.Where(x => x.State == EntityState.Added).ToList();
            foreach (var item in addedEntries)
            {
                item.SetAddedPropeties();
            }

            var modifiedEntries = auditedEntity.Where(x => x.State == EntityState.Modified).ToList();
            foreach (var item in modifiedEntries)
            {
                item.SetModifiedPropeties();
            }
        }
    }

    public static void SetAddedPropeties(this EntityEntry<BaseAuditedEntity> entityEntry)
    {
        entityEntry.Property(CreatedOn).CurrentValue = DateTime.Now;
    }

    public static void SetModifiedPropeties(this EntityEntry<BaseAuditedEntity> entityEntry)
    {
        entityEntry.Property(ModifiedOn).CurrentValue = DateTime.Now;
    }
}
