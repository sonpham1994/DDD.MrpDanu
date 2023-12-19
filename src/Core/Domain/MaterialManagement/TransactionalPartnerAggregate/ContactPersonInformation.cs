using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class ContactPersonInformation : EntityGuidStronglyTypedId<ContactPersonInformationId>
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
