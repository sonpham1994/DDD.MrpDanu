using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement;

public sealed class DomainErrors
{
    public static class Material
    {
        public static DomainError InvalidMaterialType => new("Material.InvalidMaterialType", "Material type should be None Regional Market");
        public static DomainError InvalidSubassembliesType => new("Material.InvalidSubassembliesType", "Subassemblies type should not be None Regional Market");
        public static DomainError EmptyId => new("Material.EmptyId", "MaterialId should not be empty.");
        public static DomainError EmptyCode => new("Material.EmptyCode", "Material code should not be empty.");
        public static DomainError EmptyName => new("Material.EmptyName", "Material name should not be empty.");
        public static DomainError EmptyVarian => new("Material.EmptyVarian", "Material varian should not be empty.");
        public static DomainError EmptyWidth => new("Material.EmptyWidth", "Material width should not be empty.");
        public static DomainError EmptyUnit => new("Material.EmptyUnit", "Material unit should not be empty.");
        public static DomainError ExceedsMaxNumberOfMaterialCosts => new("Material.ExceedsMaxNumberOfQuantity", "Cannot exceed the max number of Material cost.");

        public static DomainError MaterialIdNotFound(in Guid id) => new("Material.NotFoundId", $"Material id '{id}' is not found");
    }
    
    public static class RegionalMarket
    {
        public static DomainError NotFoundId(in byte id) => new("RegionalMarket.NotFoundId", $"Regional market id '{id}' is not found");
    }
    
    public static class MaterialType
    {
        public static DomainError NotFoundId(in byte id) => new("MaterialType.NotFoundId", $"Material type id '{id}' is not found");
    }

    public static class MaterialCostManagement
    {
        public static DomainError InvalidSurcharge => new("MaterialCostManagement.InvalidSurcharge", "Surcharge should not be less than or equal to 0");
        public static DomainError InvalidMinQuantity => new("MaterialCostManagement.InvalidMinQuantity", "Min quantity should not be less than or equal to 0");
        public static DomainError InvalidPrice => new("MaterialCostManagement.InvalidPrice", "Price should not be less than or equal to 0");
        public static DomainError EmptySupplierId = new("MaterialCostManagement.EmptySupplierId", "Supplier id should not be empty.");
        public static DomainError NullMaterialCost => new("MaterialCostManagement.NullMaterialCost", "Material cost object should not be null.");
        public static DomainError NullSupplier => new("MaterialCostManagement.NullSupplier", "Supplier should not null");
        
        public static DomainError DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(string currencyPrice, string currencySurcharge, string currencySupplier) => new("MaterialCostManagement.DifferentCurrencyBetweenSupplierAndPriceWithSurcharge", $"Price and Surcharge currency ({currencyPrice}, {currencySurcharge}) are different from supplier ({currencySupplier})");
        public static DomainError DuplicationSupplierId(in Guid id) => new("MaterialCostManagement.DuplicationSupplierId", $"Duplicate Supplier id {id}.");
        public static DomainError NotSupplier(in Guid id) => new("MaterialCostManagement.NotSupplier", $"Transactional partner id '{id}' is not a supplier");
        public static DomainError NotExistSupplier(in Guid supplierId, in Guid materialId) => new("MaterialCostManagement.NotExistSupplier", $"Supplier id '{supplierId}' does not exist in material id '{materialId}'");
    }

    public static class TransactionalPartner
    {
        public static DomainError EmptyName => new("TransactionalPartner.EmptyName", "Name should not be empty");
        public static DomainError TheLengthOfNameExceedsMaxLength => new("TransactionalPartner.TheLengthOfNameExceedsMaxLength", "Name should not exceed 300 characters.");
        public static DomainError EmptyTaxNo => new("TransactionalPartner.EmptyTaxNo", "Tax numbers should not be empty");
        public static DomainError NullAddress => new("TransactionalPartner.NullAddress", "Address should not be null.");
        public static DomainError EmptyAddressStreet => new("TransactionalPartner.EmptyAddressStreet", "Address street should not be empty.");
        public static DomainError EmptyAddressCity => new("TransactionalPartner.EmptyAddressCity", "Address city should not be empty.");
        public static DomainError EmptyAddressDistrict => new("TransactionalPartner.EmptyAddressDistrict", "Address district should not be empty.");
        public static DomainError EmptyAddressWard => new("TransactionalPartner.EmptyAddressWard", "Address ward should not be empty.");
        public static DomainError EmptyAddressZipCode => new("TransactionalPartner.EmptyAddressZipCode", "Address zipcode should not be empty.");
        public static DomainError InvalidAddressZipCode => new("TransactionalPartner.InvalidAddressZipCode", "Address zipcode is invalid.");
        public static DomainError InvalidCountryAndLocationType => new("TransactionalPartner.InvalidCountryAndLocationType", "Your country should be a domestic location.");
        public static DomainError InvalidCurrencyType => new("TransactionalPartner.InvalidCurrencyType", "Your country should use VND currency.");
        public static DomainError InvalidLengthTaxNo => new("TransactionalPartner.InvalidLengthTaxNo", "The length of tax no in local country should be 10.");
        public static DomainError InvalidTaxNo => new("TransactionalPartner.InvalidTaxNo", "Tax no in local country should be numbers.");
        public static DomainError NotFound => new("TransactionalPartner.NotFound", "Transactional partner is not found");
        
        public static DomainError InvalidWebsite(string website) => new("TransactionalPartner.InvalidWebsite", $"The website '{website}' is invalid.");
        public static DomainError ExceedsMaxLengthWebsite(in byte maxLength) => new("TransactionalPartner.ExceedsMaxLengthWebsite", $"The website characters cannot exceeds '{maxLength}'.");
        public static DomainError NotFoundId(in Guid id) => new("TransactionalPartner.NotFoundId", $"Transactional partner id '{id}' is not found");
    }

    public static class ContactPersonInformation
    {
        public static DomainError EmptyContact => new("ContactPersonInformation.EmptyContact", "Should provide TelNo or Email");
        public static DomainError TelNoIsNotNumbers => new("ContactPersonInformation.NotNumbers", "TelNo should be a list of numbers");
        public static DomainError EmailExceedsMaxLength => new("ContactPersonInformation.EmailExceedsMaxLength", "Email should not exceed 200 characters.");
        public static DomainError InvalidEmail => new("ContactPersonInformation.InvalidEmail", "Email is invalid");
        public static DomainError EmptyName => new("ContactPersonInformation.EmptyName", "Contact person name should not be empty.");
        public static DomainError TheLengthOfNameExceedsMaxLength => new("ContactPersonInformation.TheLengthOfNameExceedsMaxLength", "Contact person name should not exceed 200 characters");
        public static DomainError TelNoExceedsMaxLength => new("ContactPersonInformation.TelNoExceedsMaxLength", "TelNo should not exceed 20 characters.");
        public static DomainError TelNoOrEmailIsTaken => new("ContactPersonInformation.TelNoOrEmailIsTaken", "TelNo or Email is taken.");
        
    }

    public static class TransactionalPartnerType
    {
        public static DomainError NotExist => new("TransactionalPartnerType.NotExist", "Transactional partner type does not exits.");
        public static DomainError NotFoundId(in byte id) => new("TransactionalPartnerType.NotExist", $"Transactional partner type id '{id}' does not exist.");
    }

    public static class LocationType
    {
        public static DomainError NotFoundId(in byte id) => new("LocationType.NotFoundId", $"Location type id '{id}' does not exist.");
    }

    public static class Country
    {
        public static DomainError NotFoundId(in byte id) => new("Country.NotFoundId", $"Country id '{id}' is not found");
    }
}