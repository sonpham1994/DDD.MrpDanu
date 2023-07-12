using System;
using System.Collections.Generic;
using Domain.SharedKernel.Base;

namespace Infrastructure.EventDispatchers;

internal sealed class EventDispatcher
{
    public void Dispatch(IEnumerable<IDomainEvent> events)
    {
        foreach (IDomainEvent ev in events)
        {
            Dispatch(ev);
        }
    }

    private void Dispatch(IDomainEvent ev) 
    { 
        switch (ev)
        {
            // new domain events go here

            default:
                throw new Exception($"Unknown event type: '{ev.GetType()}'");
        }
    }
}