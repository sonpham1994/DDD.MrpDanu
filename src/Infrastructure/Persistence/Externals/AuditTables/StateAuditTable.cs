using Domain.SharedKernel.Base;
using Infrastructure.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public static Result<StateAuditTable> FromEntityState(EntityState entityState)
    {
        var state = entityState switch
        {
            EntityState.Added => Added,
            EntityState.Modified => Modified,
            EntityState.Deleted => Deleted,
            EntityState.Unchanged => Modified, //mark Unchanged as Modified due to behavior of EF Core when working with owned entity type
            _ => None
        };

        if (state == None)
            return InfrastructureDomainErrors.AuditData.InvalidStateAuditData(entityState.ToString());

        return state;
    }
}