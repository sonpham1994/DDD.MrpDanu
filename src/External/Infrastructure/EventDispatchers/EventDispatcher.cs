using Domain.SharedKernel.Base;
using Domain.SupplyAndProductionManagement.ProductionPlanning.Handlers;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Infrastructure.EventDispatchers;

internal sealed class EventDispatcher
{
    public void Dispatch(IEnumerable<DomainEvent> events)
    {
        foreach (DomainEvent ev in events)
        {
            Dispatch(ev);
        }
    }

    private void Dispatch(DomainEvent ev) 
    { 
        switch (ev)
        {
            case MaterialPriceChangedDomainEvent materialPriceChangedEvent:
                var handler = new MaterialPriceChangedDomainEventHandler();
                handler.Handle(materialPriceChangedEvent);
                break;
            
            default:
                throw new Exception($"Unknown event type: '{ev.GetType()}'");
        }
    }
}