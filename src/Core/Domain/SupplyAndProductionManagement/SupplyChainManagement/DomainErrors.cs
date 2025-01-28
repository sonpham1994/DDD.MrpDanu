using SharedKernel;
using SharedKernel.Enumerations;
using SharedKernel.ValueObjects;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.MaterialAggregate;

namespace Domain.SupplyAndProductionManagement.SupplyChainManagement;

public static class DomainErrors
{
    public static class Material
    {
        // This will create only "Material.InvalidMaterialType". Not create "Material" and "Material.InvalidMaterialType" and "InvalidMaterialType"
        public static DomainError InvalidMaterialType => new($"{nameof(Material)}.{nameof(InvalidMaterialType)}", "Material type should be None Regional Market");
        public static DomainError InvalidSubassembliesType => new($"{nameof(Material)}.{nameof(InvalidSubassembliesType)}", "Subassemblies type should not be None Regional Market");
        public static DomainError EmptyId => new($"{nameof(Material)}.{nameof(EmptyId)}", "MaterialId should not be empty.");
        public static DomainError EmptyCode => new($"{nameof(Material)}.{nameof(EmptyCode)}", "Material code should not be empty.");
        public static DomainError ExceedsMaxLengthCode => new($"{nameof(Material)}.{nameof(ExceedsMaxLengthCode)}", "Material code should not larger than 200 characters.");

        public static DomainError NotAllowedSpecialCharacters => new(
           $"{nameof(Material)}.{nameof(NotAllowedSpecialCharacters)}", "Material code is not allowed special characters. The special characters can allow to input is '-', '.', '/'");
        public static DomainError NotAllowedOnlyAllowedSpecialCharacters => new(
           $"{nameof(Material)}.{nameof(NotAllowedOnlyAllowedSpecialCharacters)}", "Material code is not only have allowed special characters.");
        public static DomainError EmptyName => new($"{nameof(Material)}.{nameof(EmptyName)}", "Material name should not be empty.");
        public static DomainError ExceedsMaxLengthName => new($"{nameof(Material)}.{nameof(ExceedsMaxLengthName)}", "Material name should not larger than 500 characters.");
        public static DomainError EmptyVarian => new($"{nameof(Material)}.{nameof(EmptyVarian)}", "Material varian should not be empty.");
        public static DomainError EmptyWidth => new($"{nameof(Material)}.{nameof(EmptyWidth)}", "Material width should not be empty.");
        public static DomainError EmptyUnit => new($"{nameof(Material)}.{nameof(EmptyUnit)}", "Material unit should not be empty.");
        public static DomainError ExceedsMaxNumberOfMaterialCosts => new($"{nameof(Material)}.{nameof(ExceedsMaxNumberOfMaterialCosts)}", "Cannot exceed the max number of MaterialSupplierCost.");

        public static DomainError ExistedCode(MaterialCode code, in MaterialId anotherMaterialId) => new($"{nameof(Material)}.{nameof(ExistedCode)}",
            $"The code '{code.Value}' exists in another material with id '{anotherMaterialId.Value}'");
        public static DomainError NotFoundId(in Guid id) => new($"{nameof(Material)}.{nameof(NotFoundId)}", $"Material id '{id}' is not found");

        public static DomainError NotFoundId(in MaterialId id) => NotFoundId(id.Value);
        
        public static DomainError IdsAreNotTheSame(in MaterialId id, in MaterialId anotherId) => new($"{nameof(Material)}.{nameof(IdsAreNotTheSame)}", $"Material id '{id.Value}' and '{anotherId.Value}' are not the same between Material and MaterialSupplierCost.");
    }
    
    public static class RegionalMarket
    {
        public static DomainError NotFoundId(in byte id) => new($"{nameof(RegionalMarket)}.{nameof(NotFoundId)}", $"Regional market id '{id}' is not found");
    }
    
    public static class MaterialType
    {
        public static DomainError NotFoundId(in byte id) => new($"{nameof(MaterialType)}.{nameof(NotFoundId)}", $"Material type id '{id}' is not found");
    }

    public static class MaterialSupplierCost
    {
        public static DomainError InvalidSurcharge => new($"{nameof(MaterialSupplierCost)}.{nameof(InvalidSurcharge)}", "Surcharge should not be less than or equal to 0");
        public static DomainError InvalidMinQuantity => new($"{nameof(MaterialSupplierCost)}.{nameof(InvalidMinQuantity)}", "Min quantity should not be less than or equal to 0");
        public static DomainError InvalidPrice => new($"{nameof(MaterialSupplierCost)}.{nameof(InvalidPrice)}", "Price should not be less than or equal to 0");
        public static DomainError EmptySupplierId = new($"{nameof(MaterialSupplierCost)}.{nameof(EmptySupplierId)}", "Supplier id should not be empty.");
        public static DomainError NullMaterialCost => new($"{nameof(MaterialSupplierCost)}.{nameof(NullMaterialCost)}", "Material cost object should not be null.");
        
        public static DomainError DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(CurrencyType currencyPrice, CurrencyType currencySurcharge) => new($"{nameof(MaterialSupplierCost)}.{nameof(DifferentCurrencyBetweenSupplierAndPriceWithSurcharge)}", $"Price and Surcharge currency ('{currencyPrice.Name}' and '{currencySurcharge.Name}') are different.");
        public static DomainError DuplicationSupplierIdFromRequest(in SupplierId id) => new($"{nameof(MaterialSupplierCost)}.{nameof(DuplicationSupplierIdFromRequest)}", $"Duplicate Supplier id {id.Value} from request.");
        public static DomainError DuplicationSupplierIdFromDataStorage(in SupplierId id) => new($"{nameof(MaterialSupplierCost)}.{nameof(DuplicationSupplierIdFromDataStorage)}", $"Duplicate Supplier id {id.Value} from data storage.");
        public static DomainError NotFoundSupplierId(in SupplierId supplierId) => new($"{nameof(MaterialSupplierCost)}.{nameof(NotFoundSupplierId)}", $"Supplier id '{supplierId.Value}' is not found.");
    }

    public static class TransactionalPartner
    {
        public static DomainError EmptyName => new($"{nameof(TransactionalPartner)}.{nameof(EmptyName)}", "Name should not be empty");
        public static DomainError TheLengthOfNameExceedsMaxLength => new($"{nameof(TransactionalPartner)}.{nameof(TheLengthOfNameExceedsMaxLength)}", "Name should not exceed 300 characters.");
        public static DomainError EmptyTaxNo => new($"{nameof(TransactionalPartner)}.{nameof(EmptyTaxNo)}", "Tax numbers should not be empty");
        public static DomainError NullAddress => new($"{nameof(TransactionalPartner)}.{nameof(NullAddress)}", "Address should not be null.");
        public static DomainError EmptyAddressStreet => new($"{nameof(TransactionalPartner)}.{nameof(EmptyAddressStreet)}", "Address street should not be empty.");
        public static DomainError EmptyAddressCity => new($"{nameof(TransactionalPartner)}.{nameof(EmptyAddressCity)}", "Address city should not be empty.");
        public static DomainError EmptyAddressDistrict => new($"{nameof(TransactionalPartner)}.{nameof(EmptyAddressDistrict)}", "Address district should not be empty.");
        public static DomainError EmptyAddressWard => new($"{nameof(TransactionalPartner)}.{nameof(EmptyAddressWard)}", "Address ward should not be empty.");
        public static DomainError EmptyAddressZipCode => new($"{nameof(TransactionalPartner)}.{nameof(EmptyAddressZipCode)}", "Address zipcode should not be empty.");
        public static DomainError InvalidAddressZipCode => new($"{nameof(TransactionalPartner)}.{nameof(InvalidAddressZipCode)}", "Address zipcode is invalid.");
        public static DomainError InvalidCountryAndLocationType => new($"{nameof(TransactionalPartner)}.{nameof(InvalidCountryAndLocationType)}", "Your country should be a domestic location.");
        public static DomainError InvalidCurrencyType => new($"{nameof(TransactionalPartner)}.{nameof(InvalidCurrencyType)}", "Your country should use VND currency.");
        public static DomainError InvalidLengthTaxNo => new($"{nameof(TransactionalPartner)}.{nameof(InvalidLengthTaxNo)}", "The length of tax no in local country should be 10.");
        public static DomainError InvalidTaxNo => new($"{nameof(TransactionalPartner)}.{nameof(InvalidTaxNo)}", "Tax no in local country should be numbers.");
        public static DomainError NotFound => new($"{nameof(TransactionalPartner)}.{nameof(NotFound)}", "Transactional partner is not found");

        public static DomainError EmptyWebsiteValue =>  new($"{nameof(TransactionalPartner)}.{nameof(EmptyWebsiteValue)}",  "Website's value should not be null or empty.");
        public static DomainError InvalidWebsite(string website) => new($"{nameof(TransactionalPartner)}.{nameof(InvalidWebsite)}", $"The website '{website}' is invalid.");
        public static DomainError ExceedsMaxLengthWebsite(in byte maxLength) => new($"{nameof(TransactionalPartner)}.{nameof(ExceedsMaxLengthWebsite)}", $"The website characters cannot exceeds '{maxLength}'.");
        public static DomainError NotFoundId(in TransactionalPartnerId id) => new($"{nameof(TransactionalPartner)}.{nameof(NotFoundId)}", $"Transactional partner id '{id.Value}' is not found");
        public static DomainError NotFoundId(in Guid id) => new($"{nameof(TransactionalPartner)}.{nameof(NotFoundId)}", $"Transactional partner id '{id}' is not found");
    }

    public static class ContactPersonInformation
    {
        public static DomainError EmptyContact => new($"{nameof(ContactPersonInformation)}.{nameof(EmptyContact)}", "Should provide TelNo or Email");
        public static DomainError TelNoIsNotNumbers => new($"{nameof(ContactPersonInformation)}.{nameof(TelNoIsNotNumbers)}", "TelNo should be a list of numbers");
        public static DomainError EmailExceedsMaxLength => new($"{nameof(ContactPersonInformation)}.{nameof(EmailExceedsMaxLength)}", "Email should not exceed 200 characters.");
        public static DomainError InvalidEmail => new($"{nameof(ContactPersonInformation)}.{nameof(InvalidEmail)}", "Email is invalid");
        public static DomainError EmptyName => new($"{nameof(ContactPersonInformation)}.{nameof(EmptyName)}", "Contact person name should not be empty.");
        public static DomainError TheLengthOfNameExceedsMaxLength => new($"{nameof(ContactPersonInformation)}.{nameof(TheLengthOfNameExceedsMaxLength)}", "Contact person name should not exceed 200 characters");
        public static DomainError TelNoExceedsMaxLength => new($"{nameof(ContactPersonInformation)}.{nameof(TelNoExceedsMaxLength)}", "TelNo should not exceed 20 characters.");
        public static DomainError TelNoOrEmailIsTaken => new($"{nameof(ContactPersonInformation)}.{nameof(TelNoOrEmailIsTaken)}", "TelNo or Email is taken.");
        
    }

    public static class TransactionalPartnerType
    {
        public static DomainError NotFoundId(in byte id) => new($"{nameof(TransactionalPartnerType)}.{nameof(NotFoundId)}", $"Transactional partner type id '{id}' does not exist.");
    }

    public static class LocationType
    {
        public static DomainError NotFoundId(in byte id) => new($"{nameof(LocationType)}.{nameof(NotFoundId)}", $"Location type id '{id}' does not exist.");
    }

    public static class Country
    {
        public static DomainError NotFoundId(in byte id) => new($"{nameof(Country)}.{nameof(NotFoundId)}", $"Country id '{id}' is not found");
    }
}