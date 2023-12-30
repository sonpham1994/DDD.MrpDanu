using Domain.SharedKernel.Base;
using Domain.SharedKernel.ValueObjects;

namespace Domain.SupplyChainManagement.TransactionalPartnerAggregate;

public class ContactPersonInformation : EntityGuidStronglyTypedId<TransactionalPartnerId>
{
    public PersonName Name { get; private set; }
    public ContactInformation ContactInformation { get; private set; }

    protected ContactPersonInformation() {}
    
    internal ContactPersonInformation(PersonName name, ContactInformation contactInformation) : this()
    {
        Name = name;
        ContactInformation = contactInformation;
    }

    internal void SetContactPersonInfo(PersonName name, ContactInformation contactInformation)
    {
        Name = name;
        ContactInformation = contactInformation;
    }
}
