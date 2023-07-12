using Domain.SharedKernel.Base;

namespace Domain.Errors;

public static partial class DomainErrors
{
    public static class Material
    {
        public static readonly DomainError InvalidMaterialType = new("Material.InvalidMaterialType", "Material type should not be None Regional Market");
        public static readonly DomainError InvalidSubassembliesType = new("Material.InvalidSubassembliesType", "Subassemblies type should not be None Regional Market");
        public static readonly DomainError EmptyId = new("Material.EmptyId", "MaterialId should not be empty.");
        public static readonly DomainError EmptyCode = new("Material.EmptyCode", "Material code should not be empty.");
        public static readonly DomainError EmptyName = new("Material.EmptyName", "Material name should not be empty.");
        public static readonly DomainError EmptyVarian = new("Material.EmptyVarian", "Material varian should not be empty.");
        public static readonly DomainError EmptyWidth = new("Material.EmptyWidth", "Material width should not be empty.");
        public static readonly DomainError EmptyUnit = new("Material.EmptyUnit", "Material unit should not be empty.");

        public static DomainError MaterialIdNotFound(Guid id) => new("Material.NotFoundId", $"Material id '{id}' is not found");
    }
    
    public static class RegionalMarket
    {
        public static DomainError NotFoundId(byte id) => new("RegionalMarket.NotFoundId", $"Regional market id '{id}' is not found");
    }
    
    public static class MaterialType
    {
        public static DomainError NotFoundId(byte id) => new("MaterialType.NotFoundId", $"Material type id '{id}' is not found");
    }

    public static class MaterialCostManagement
    {
        public static readonly DomainError InvalidSurcharge = new("MaterialCostManagement.InvalidSurcharge",  "Surcharge should not be less than or equal to 0");
        public static readonly DomainError InvalidMinQuantity = new("MaterialCostManagement.InvalidMinQuantity",  "Min quantity should not be less than or equal to 0");
        public static readonly DomainError InvalidPrice = new("MaterialCostManagement.InvalidPrice",  "Price should not be less than or equal to 0");
        public static readonly DomainError EmptySupplierId = new("MaterialCostManagement.EmptySupplierId", "Supplier id should not be empty.");
        public static readonly DomainError NullMaterialCost = new("MaterialCostManagement.NullMaterialCost", "Material cost object should not be null.");
        public static readonly DomainError NullSupplier = new("MaterialCostManagement.NullSupplier", "Supplier should not null");
        
        public static DomainError DifferentCurrencyBetweenSupplierAndPriceWithSurcharge(string currencyPrice, string currencySurcharge, string currencySupplier) => new("MaterialCostManagement.DifferentCurrencyBetweenSupplierAndPriceWithSurcharge", $"Price and Surcharge currency ({currencyPrice}, {currencySurcharge}) are different from supplier ({currencySupplier})");
        public static DomainError DuplicationSupplierId(Guid id) => new("MaterialCostManagement.DuplicationSupplierId", $"Duplicate Supplier id {id}.");
        public static DomainError NotSupplier(Guid id) => new("MaterialCostManagement.NotSupplier", $"Transactional partner id '{id}' is not a supplier");
        public static DomainError NotExistSupplier(Guid supplierId, Guid materialId) => new("MaterialCostManagement.NotExistSupplier", $"Supplier id '{supplierId}' does not exist in material id '{materialId}'");
    }

    public static class TransactionalPartner
    {
        public static readonly DomainError EmptyName = new("TransactionalPartner.EmptyName", "Name should not be empty");
        public static readonly DomainError TheLengthOfNameExceedsMaxLength = new("TransactionalPartner.TheLengthOfNameExceedsMaxLength", "Name should not exceed 300 characters.");
        public static readonly DomainError EmptyTaxNo = new("TransactionalPartner.EmptyTaxNo", "Tax numbers should not be empty");
        public static readonly DomainError NullAddress = new("TransactionalPartner.NullAddress", "Address should not be null.");
        public static readonly DomainError EmptyAddressStreet = new("TransactionalPartner.EmptyAddressStreet", "Address street should not be empty.");
        public static readonly DomainError EmptyAddressCity = new("TransactionalPartner.EmptyAddressCity", "Address city should not be empty.");
        public static readonly DomainError EmptyAddressDistrict = new("TransactionalPartner.EmptyAddressDistrict", "Address district should not be empty.");
        public static readonly DomainError EmptyAddressWard = new("TransactionalPartner.EmptyAddressWard", "Address ward should not be empty.");
        public static readonly DomainError EmptyAddressZipCode = new("TransactionalPartner.EmptyAddressZipCode", "Address zipcode should not be empty.");
        public static readonly DomainError InvalidAddressZipCode = new("TransactionalPartner.InvalidAddressZipCode", "Address zipcode is invalid.");
        public static readonly DomainError InvalidCountryAndLocationType = new("TransactionalPartner.InvalidCountryAndLocationType", "Your country should be a domestic location.");
        public static readonly DomainError InvalidCurrencyType = new("TransactionalPartner.InvalidCurrencyType", "Your country should use VND currency.");
        public static readonly DomainError InvalidLengthTaxNo = new("TransactionalPartner.InvalidLengthTaxNo", "The length of tax no in local country should be 10.");
        public static readonly DomainError InvalidTaxNo = new("TransactionalPartner.InvalidTaxNo", "Tax no in local country should be numbers.");
        public static readonly DomainError NotFound = new("TransactionalPartner.NotFound", "Transactional partner is not found");
        
        public static DomainError InvalidWebsite(string website) => new("TransactionalPartner.InvalidWebsite", $"The website '{website}' is invalid.");
        public static DomainError NotFoundId(Guid id) => new("TransactionalPartner.NotFoundId", $"Transactional partner id '{id}' is not found");
    }

    public static class ContactPersonInformation
    {
        public static readonly DomainError EmptyContact = new("ContactPersonInformation.EmptyContact", "Should provide TelNo or Email");
        public static readonly DomainError TelNoIsNotNumbers = new("ContactPersonInformation.NotNumbers", "TelNo should be a list of numbers");
        public static readonly DomainError EmailExceedsMaxLength = new("ContactPersonInformation.EmailExceedsMaxLength", "Email should not exceed 200 characters.");
        public static readonly DomainError InvalidEmail = new("ContactPersonInformation.InvalidEmail", "Email is invalid");
        public static readonly DomainError EmptyName = new("ContactPersonInformation.EmptyName", "Contact person name should not be empty.");
        public static readonly DomainError TheLengthOfNameExceedsMaxLength = new("ContactPersonInformation.TheLengthOfNameExceedsMaxLength", "Contact person name should not exceed 200 characters");
        public static readonly DomainError TelNoExceedsMaxLength = new("ContactPersonInformation.TelNoExceedsMaxLength", "TelNo should not exceed 20 characters.");
        public static readonly DomainError TelNoOrEmailIsTaken = new("ContactPersonInformation.TelNoOrEmailIsTaken", "TelNo or Email is taken.");
        
    }

    public static class TransactionalPartnerType
    {
        public static readonly DomainError NotExist = new("TransactionalPartnerType.NotExist", "Transactional partner type does not exits.");
        public static DomainError NotFoundId(byte id) => new("TransactionalPartnerType.NotExist", $"Transactional partner type id '{id}' does not exist.");
    }

    public static class LocationType
    {
        public static DomainError NotFoundId(byte id) => new("LocationType.NotFoundId", $"Location type id '{id}' does not exist.");
    }

    public static class Country
    {
        public static DomainError NotFoundId(byte id) => new("Country.NotFoundId", $"Country id '{id}' is not found");
    }

    public static class CurrencyType
    {
        public static DomainError NotFoundId(byte id) => new("CurrencyType.NotFoundId", $"Currency type id '{id}' does not exist.");
    }
}