using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Domain.SupplyAndProductionManagement.ProductionPlanning.DomainEventHandlers;

public class MaterialPriceChangedDomainEventHandler
{
    public void Handle(MaterialPriceChangedDomainEvent domainEvent)
    {
        //update price for the latest revision BoMRevision
    }
}