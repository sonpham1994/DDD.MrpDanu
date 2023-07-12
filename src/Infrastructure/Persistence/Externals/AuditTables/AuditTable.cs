using System;
using Application.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Externals.AuditTables;

[Table(nameof(AuditTable))]
public class AuditTable
{
    [Key]
    public Guid Id { get; set; }
    
    //json https://www.youtube.com/watch?v=2ZNMlx44gKQ&ab_channel=MilanJovanovi%C4%87
    [Column(TypeName = "nvarchar(max)")]
    public string Content { get; set; }
    
    [Column(TypeName = "varchar(250)")]
    public string ObjectId { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string ObjectName {get; set; }
    
    public DateTime TimeGenerated { get; set; }
    
    public Guid CreatedUser { get; set; }

    [ForeignKey(nameof(StateAuditTable))]
    public byte StateAuditTableId { get; set; }
    public StateAuditTable StateAuditTable { get; set; }

    [Column(TypeName = "varchar(250)")]
    public string CorrelationId { get; set; }

    private AuditTable() { }

    public AuditTable(string content, string objectId, string objectName, StateAuditTable stateAuditTable, Guid createdUserId) 
    { 
        Id = Guid.NewGuid();
        Content = content;
        ObjectId = objectId;
        ObjectName = objectName;
        StateAuditTableId = stateAuditTable.Id;
        StateAuditTable = stateAuditTable;
        TimeGenerated = DateTime.UtcNow;
        CreatedUser = createdUserId;
        CorrelationId = Helper.GetTraceId();
    }
}