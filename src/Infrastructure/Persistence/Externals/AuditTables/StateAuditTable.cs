using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Externals.AuditTables;

[Table(nameof(StateAuditTable))]
internal sealed class StateAuditTable : Enumeration<StateAuditTable>
{
    //Due to EF needs to define properties if using DataAnnotation
    [Key]
    [Column(TypeName = "tinyint")]
    public new byte Id { get; private set; }

    [Column(TypeName = "varchar(50)")]
    public new string Name { get; private set; }
    private StateAuditTable(byte id, string name) : base(id, name)
    {
        Id = id;
        Name = name;
    }
    //end Due to EF need to define if using DataAnnotation

    public static readonly StateAuditTable None = new(0, nameof(None));
    public static readonly StateAuditTable Added = new(1, nameof(Added));
    public static readonly StateAuditTable Modified = new(2, nameof(Modified));
    public static readonly StateAuditTable Deleted = new(3, nameof(Deleted));

    private StateAuditTable() { }

    public static Result<StateAuditTable> FromEntityState(EntityEntry entity)
    {
        var entityState = entity.State;
        var state = entityState switch
        {
            EntityState.Added => Added,
            EntityState.Modified => Modified,
            EntityState.Deleted => Deleted,
            _ => None
        };

        if (state == None)
        {
            if (entity.IsInternalEntityModified())
            {
                state = Modified;
            }
            else
            {
                return InfrastructureDomainErrors.AuditData.InvalidStateAuditData(entityState.ToEnumString());
            }
        }

        return state;
    }
}