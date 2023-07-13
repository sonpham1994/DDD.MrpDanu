using Domain.Exceptions;

namespace Domain.SharedKernel.Base;

//Please check Solutions in Infrastructure
/*
*  - Solution 1: you just put the IAuditTable interface in any Domain and then serializing all properties in that
*  entity.
*      + Problem 1: Due to the fact that you put this interface for your domain, it will serializing data by Domain
*      model, which accrues hardware size and reduces performance for reading.
*      + Problem 2: If you use lazyloading, it also has a LazyLoader property in the json, which increases hardware
*      size and reduces performance for reading
*      + Problem 3: if we use domain model to persist audit data. One day if we refactor domain model, we also need to
 *     deal with the new version of domain model for audit data if we need to retrieve them to display UI 
*     
* It should persist these changes as a pure entity, not domain model. If this audit data just is for dev to
*  check, maybe it makes sense even though having some performance issue. This solution also make changes to Domain
*  Model by putting the IAuditTable interface, which Domain Models know about which one persist data as Audit data,
*  but maybe it's just a little violation of separation of concerns. 
* You are not able to use IAuditTable interface by adding a collection with entities you want to audit.
* Please check mockup file for Solution 1 to see the json that persists with Domain Model (AuditTableForSolution1.json)
* So we can manual create which properties we want to persist. Please check CreateForSolution1 in MaterialAuditFactory
* And mockup file will be in AuditTable.json
*/
public interface IAuditTableForSolution1
{
}

/*  - Solution 2: each entity want to audit, we will create entity audit on top of that entity. Please check
*  LogChangesForSolution2Async to see example.
*       + Problem 1: You need to make sure that your domain model should not be sealed class.
*       + Problem 2: With this solution, we prevent pollution the Domain Model by not putting IAuditTable to
*       Domain Model and can manage what data that we want to persist for audit, but causes introducing a lot of
*       audit classes which we want to persist
*  This solution makes sense whether if client want a feature to audit changes or not.
*  Please check mockup file for Solution 1 to see the json that persists (AuditTable.json)
*/
public interface IAuditTableForSolution2
{
    (string, string) Serialize();
}

/*
 * Solution 2.1:
 *  - This is advanced for solution 2, which we dont need to duplicate all properties from Domain Model, and pass 
 *  domain model into Serialize method as parameter instead of constructor
 */
public abstract class AuditTableForSolution2_1
{
    public string Id { get; protected set; }
    public string Content { get; protected set; }
    public string ObjectName { get; protected set; } 
    public abstract Result<AuditTableForSolution2_1> Serialize<T>(T data); // we can use EntityEntry as parameter and put this abstract into Infrastructure instead

    // if we can put this factory design pattern here and put this abstract outside of domain model, it means this will reside in Infrastructure 
    //public static AuditTableForSolution2_1 Create(Type type)
    //{
    //    var typeName = auditTableType.Name;
    //    var result = typeName switch
    //    {
    //        nameof(MaterialAuditForSolution2_1) => new MaterialAuditForSolution2_1(),
    //        _ => throw new DomainException(DomainErrors.AuditData.NotSupportEntityAuditTypeYet(entityEntry.Entity.GetUnproxiedType().Name))
    //    };
    //
    //    return result;
    //}
}

/*  - Solution 3: put Serialize method in entity that you want to audit. This makes sense when client want to have a audit
*  feature and Domain Model is a part of audit, but it seems like the audit feature should reside in read-side, not write-side.
*  Hence, in this case if you can put the behaviour of audit data into read-side, it would be better
*  Please check mockup file for Solution 1 to see the json that persists (AuditTable.json)
*/
public interface IAuditTableForSolution3
{
    (string, string) Serialize();
}

/*
 * From all solutions, I prever solution 2.1 over others. Due to the fact that it helps logic audit separates from domain model
 */

/* if we make changes sth to table 1, and then have an impact on table 2, but in this bounded context 1 don't know
 *  about bounded context 2, and it insert 2 records in AuditTable table. So we don't know whether they are the same
 *  process/request or not. So it makes sense to have a correlationId like traceId to group all the impacts if we
 *  change sth
 */