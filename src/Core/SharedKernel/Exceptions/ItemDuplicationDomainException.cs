namespace SharedKernel.Exceptions;

public class ItemDuplicationDomainException(Type type) 
    : DomainException(DomainErrors.ItemDuplication.NotSupportType(type.Name));