namespace Application.Interfaces.Services;

public interface IAuditTableService
{
    Task LogChangesAsync();
    /*
     *  - Solution 1: you just put the IAuditTable interface in any Domain and then serializing all properties in that
     *  entity.
     *      + Problem 1: Due to the fact that you put this interface for your domain, it will serializing data by Domain
     *      model, which accrues hardware size and reduces performance for reading.
     *      + Problem 2: If you use lazyloading, it also has a LazyLoader property in the json, which increases hardware
     *      size and reduces performance for reading
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
    //Task LogChangesForSolution1Async();

    /*  - Solution 2: each entity want to audit, we will create entity audit on top of that entity. Please check
     *  LogChangesForSolution2Async to see example.
     *       + Problem 1: You need to make sure that your domain model should not be sealed class.
     *       + Problem 2: With this solution, we prevent pollution the Domain Model by not putting IAuditTable to
     *       Domain Model and can manage what data that we want to persist for audit, but causes introducing a lot of
     *       audit classes which we want to persist
     *  This solution makes sense whether if client want a feature to audit changes or not.
     *  Please check mockup file for Solution 1 to see the json that persists (AuditTable.json)
     */
    //Task LogChangesForSolution2Async();

    /*
    * Solution 2.1:
    *  - This is an advanced solution 2, which we dont need to duplicate all properties from Domain Model, and pass 
    *  domain model into Serialize method as parameter instead of constructor
    */
    //Task LogChangesForSolution2_1Async();

    /*  - Solution 3: put Serialize method in entity that you want to audit. This makes sense when client want to have a audit
     *  feature and Domain Model is a part of audit, but it seems like the audit feature should reside in read-side, not write-side.
     *  Hence, in this case if you can put the behaviour of audit data into read-side, it would be better
     *  Please check mockup file for Solution 1 to see the json that persists (AuditTable.json)
     */
    //Task LogChangesForSolution3Async();


    /* if we make changes sth to table 1, and then have an impact on table 2, but in this bounded context 1 don't know
     *  about bounded context 2, and it insert 2 records in AuditTable table. So we don't know whether they are the same
     *  process/request or not. So it makes sense to have a correlationId like traceId to group all the impacts if we
     *  change sth
     */
}