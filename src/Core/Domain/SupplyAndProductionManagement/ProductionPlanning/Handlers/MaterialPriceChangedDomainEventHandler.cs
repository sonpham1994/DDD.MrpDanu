using Domain.SupplyChainManagement.MaterialAggregate;

namespace Domain.ProductionPlanning.Handlers;

public class MaterialPriceChangedDomainEventHandler
{
    public void Handle(MaterialPriceChangedDomainEvent domainEvent)
    {
        //update price for the latest revision BoMRevision
    }
}