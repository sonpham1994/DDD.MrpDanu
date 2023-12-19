using Domain.SharedKernel.Base;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class TransactionalPartner : AggregateRootGuidStronglyTypedId<TransactionalPartnerId>
{
    public CompanyName Name { get; private set; }
    
    //TaxNo and Address may should be exist in the same ValueObject. Because whenever we modify state, it may result
    //in modifying Country, which makes the TaxNo incorrect. So the problem is that, can we put the value object in 
    //another value object?
    public TaxNo TaxNo { get; private set; }
    
    //if a company change address, it means the company move to another location. If two companies have the same
    //address, it means these two companies locate on the same building but they are different floors, for example
    public Address Address { get; private set; }
    
    //should website is a ValueObject? Website is not required so if we introduce ValueObject for Website, it means WebsiteValueObject
    //can be empty? -> we can treat ValueObject as null and Value Conversion skill skip factory method if null data
    public Website? Website { get; private set; }

    public virtual ContactPersonInformation ContactPersonInformation { get; private set; }
    public virtual TransactionalPartnerType TransactionalPartnerType { get; private set; }
    public virtual CurrencyType CurrencyType { get; private set; }
    public virtual LocationType LocationType { get; private set; }

    //required EF Proxies
    protected TransactionalPartner() { }

    private TransactionalPartner(CompanyName name, 
        TaxNo taxNo, 
        Website? website,
        ContactPersonInformation contactPersonInfo,
        Address address, 
        TransactionalPartnerType type, 
        CurrencyType currency,
        LocationType location) : this()
    {
        Name = name;
        TaxNo = taxNo;
        Website = website;
        ContactPersonInformation = contactPersonInfo;
        Address = address;
        TransactionalPartnerType = type;
        CurrencyType = currency;
        LocationType = location;
    }

    public static Result<TransactionalPartner> Create(CompanyName name, 
        TaxNo taxNo, 
        Website? website,
        PersonName personName, 
        ContactInformation contactInfo,
        Address address, 
        TransactionalPartnerType type, 
        CurrencyType currency,
        LocationType location)
    {
        var result = CanExecuteCreateOrUpdate(address, currency, location);
        if (result.IsFailure)
            return result.Error;
        
        var contactPersonInfo = new ContactPersonInformation(personName, contactInfo);
        return new TransactionalPartner(name, taxNo, website, contactPersonInfo, address, type,
            currency, location);
    }

    public Result Update(CompanyName name,
        TaxNo taxNo,
        Website? website,
        Address address,
        TransactionalPartnerType type,
        CurrencyType currency,
        LocationType location)
    {
        var result = CanExecuteCreateOrUpdate(address, currency, location);
        if (result.IsFailure)
            return result;

        Name = name;
        TaxNo = taxNo;
        Website = website;
        Address = address;
        TransactionalPartnerType = type;
        CurrencyType = currency;
        LocationType = location;

        return Result.Success();
    }

    public void UpdateContactPersonInfo(PersonName personName, ContactInformation contactInfo)
    {
        ContactPersonInformation.SetContactPersonInfo(personName, contactInfo);
    }

    public Result IsSupplier()
    {
        var isSupplier = TransactionalPartnerType.IsSupplierType(TransactionalPartnerType);

        if (!isSupplier)
            return DomainErrors.MaterialCostManagement.NotSupplier(new SupplierId(Id.Value));

        return Result.Success();
    }
    
    public Result<>

    private static Result CanExecuteCreateOrUpdate(Address address, 
        CurrencyType currency, 
        LocationType location)
    {
        var locationTypeResult = IsValidLocationType(location, address);
        if (locationTypeResult.IsFailure)
            return locationTypeResult;
        
        var currencyResult = IsValidCurrencyType(currency, address);
        if (currencyResult.IsFailure)
            return currencyResult;

        return Result.Success();
    }
    
    private static Result IsValidCurrencyType(CurrencyType currency, Address address)
    {
        /* we cannot create a CurrencyCountry value object where currencyType and country reside in here. It will make
         *  sense when two parties can discuss about the currency which can pay for goods or materials. For example,
         *  a company in Vietnam want to purchase materials from another company in China, the company in Vietnam can
         *  use USD currency to pay for materials, as long as two parties accept this currency. But if make sense if
         *  our country is vietnam and the client or supplier is Vietnam company
         */
        if (address.Country == Country.VietNam 
            && currency != CurrencyType.VND)
            return DomainErrors.TransactionalPartner.InvalidCurrencyType;

        return Result.Success();
    }

    private static Result IsValidLocationType(LocationType location, Address address)
    {
        /*we cannot put locationType inside address. It makes sense if the client reside in Vietnam, or any country
         *  and if address is country, locationType should be domestic, not oversea, and they seems like going along
         *  with each other, However, In terms of comparison, if we have two address value objects, all properties are
         *  the same, but locationType. So they treat as not unequal, which cannot make sense.
         */
        if ((address.Country != Country.VietNam && location == LocationType.Domestic)
            || (address.Country == Country.VietNam && location == LocationType.Oversea))
            return DomainErrors.TransactionalPartner.InvalidCountryAndLocationType;

        return Result.Success();
    }
}